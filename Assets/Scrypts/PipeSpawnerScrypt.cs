using UnityEngine;
using static LogicScrypt;

public class PipeSpawnerScrypt : MonoBehaviour
{
    public float pipeSpawnDelay = 2f;
    private float _pipeSpawnOffset = 4f;
    private float _timer;
    public GameObject pipe;
    public GameObject pipeContainer;
    private LogicScrypt _logic;

    // Start is called before the first frame update
    void Start()
    {
        _logic = GameObject.FindWithTag("Logic").GetComponent<LogicScrypt>();
        SpawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        _logic.Timer(ref _timer, pipeSpawnDelay, SpawnPipe);
    }
    
    private void SpawnPipe()
    {
        var range = _logic.DefineSpawnRange(transform.position.y, _pipeSpawnOffset);
        float lowestPoint = range[0];
        float highestPoint = range[1];

        Vector3 pos = new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0);

        var pipeClone = Instantiate(pipe, pos, transform.rotation);
        pipeClone.transform.parent = pipeContainer.transform;
    }
}
