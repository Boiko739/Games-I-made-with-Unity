using UnityEngine;

public class PipeSpawnerScript : MonoBehaviour
{
    public GameObject Pipe, 
                      PipeContainer;
    private LogicScript _logic;
    private FunctionTimer _timer;
    private readonly float _pipeSpawnDelay = 2f,
                           _pipeSpawnOffset = 4f;

    void Start()
    {
        _logic = GameObject.FindWithTag("Logic").GetComponent<LogicScript>();
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        Pipe = GameObject.FindGameObjectWithTag("Pipes");
        PipeContainer = GameObject.FindGameObjectWithTag("Containers")
                         .transform.Find("PipesContainer").gameObject;
    }

    void Update()
    {
        FunctionTimer.StartAndUpdateTimer(ref _timer, _pipeSpawnDelay, SpawnPipes);
    }

    public void SpawnPipes()
    {
        float lowestPoint;
        float highestPoint;

        float[] range = _logic.DefineSpawnRange(transform.position.y, _pipeSpawnOffset, pipeIsCalling: true);
        lowestPoint = range[0];
        highestPoint = range[1];

        Vector3 pos = new(transform.position.x, Random.Range(lowestPoint, highestPoint), 0);

        var pipeClone = Instantiate(Pipe, pos, transform.rotation);
        pipeClone.SetActive(true);
        pipeClone.transform.parent = PipeContainer.transform;
    }
}
