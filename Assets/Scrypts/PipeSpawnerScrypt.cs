using UnityEngine;

public class PipeSpawnerScrypt : MonoBehaviour
{

    public GameObject pipe;
    public float spawnRate = 2f;
    private float timer = 0f;
    private float spawnOffset = 3f;
    public GameObject PipeContainer;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
            timer += Time.deltaTime;
        else
        {
            SpawnPipe();
            timer = 0f;
        }
    }
    void SpawnPipe()
    {
        float lowestPoint = transform.position.y - spawnOffset;
        float highestPoint = transform.position.y + spawnOffset;

        var pipeClone = Instantiate(pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
        pipeClone.transform.parent = PipeContainer.transform;
    }
}
