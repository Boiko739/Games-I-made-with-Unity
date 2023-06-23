using UnityEngine;

public class PipeSpawnerScrypt : MonoBehaviour
{
    public GameObject pipe;
    public float pipeSpawnDelay = 2f;
    private float timer = 0f;
    private float pipeSpawnOffset = 4f;
    public GameObject pipeContainer;
    private LogicScrypt logic;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindWithTag("Logic").GetComponent<LogicScrypt>();
        SpawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < pipeSpawnDelay)
            timer += Time.deltaTime;
        else
        {
            SpawnPipe();
            timer = 0f;
        }
    }
    void SpawnPipe()
    {
        float lowestPoint = 0f;
        float highestPoint = 0f;
        if (logic.playerScore < 10)
        {
            lowestPoint = transform.position.y - pipeSpawnOffset;
            highestPoint = transform.position.y + pipeSpawnOffset;
        }

        Vector3 pos = new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0);

        var pipeClone = Instantiate(pipe, pos, transform.rotation);
        pipeClone.transform.parent = pipeContainer.transform;
    }
}
