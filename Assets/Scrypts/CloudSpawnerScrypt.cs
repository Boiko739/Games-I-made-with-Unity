using UnityEngine;

public class CloudSpawnerScrypt : MonoBehaviour
{
    private float timer = 0f;
    public float cloudSpawnDelay = 0.6f;
    private float cloudSpawnOffset = 3f;
    public GameObject clouds;
    public GameObject cloudContainer;
    private LogicScrypt logic;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindWithTag("Logic").GetComponent<LogicScrypt>();
        SpawnCloud();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < cloudSpawnDelay)
            timer += Time.deltaTime;
        else
        {
            SpawnCloud();
            timer = 0f;
        }
    }

    void SpawnCloud()
    {
        float lowestPoint = transform.position.y - cloudSpawnOffset;
        float highestPoint = transform.position.y + cloudSpawnOffset;

        float farthestPoint = transform.position.z - cloudSpawnOffset;
        float closestPoint = transform.position.z + cloudSpawnOffset;

        int childCount = clouds.transform.childCount;

        Vector3 pos = new Vector3(
            transform.position.x,
            Random.Range(lowestPoint, highestPoint),
            Random.Range(farthestPoint, closestPoint));

        var cloudClone = Instantiate(clouds.transform.GetChild(Random.Range(0, childCount)), pos, transform.rotation);
        cloudClone.transform.parent = cloudContainer.transform;
    }
}
