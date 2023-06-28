using UnityEngine;

public class PipeSpawnerScrypt : MonoBehaviour
{
    private float _pipeSpawnDelay = 2f;
    private float _pipeSpawnOffset = 4f;
    private FunctionTimer _timer;
    public GameObject pipe;
    public GameObject pipeContainer;
    private LogicScript _logic;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        _logic = GameObject.FindWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        FunctionTimer.StartAndUpdateTimer(ref _timer, _pipeSpawnDelay, SpawnPipes);
    }
    
    private void SpawnPipes()
    {
        float lowestPoint;
        float highestPoint;

        var range = _logic.DefineSpawnRange(transform.position.y, _pipeSpawnOffset);
        lowestPoint = range[0];
        highestPoint = range[1];

        Vector3 pos = new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0);

        var pipeClone = Instantiate(pipe, pos, transform.rotation);
        pipeClone.transform.parent = pipeContainer.transform;
    }
}
