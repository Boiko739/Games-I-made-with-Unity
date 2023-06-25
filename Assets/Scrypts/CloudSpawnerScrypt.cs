using UnityEngine;

public class CloudSpawnerScrypt : MonoBehaviour
{
    private float _cloudSpawnDelay = 1f;
    private float _cloudSpawnOffset = 7f;
    private float _timer;
    public GameObject clouds;
    public GameObject cloudContainer;
    private LogicScrypt _logic;
    // Start is called before the first frame update
    void Start()
    {
        float posX = transform.position.x;
        _logic = GameObject.FindWithTag("Logic").GetComponent<LogicScrypt>();
        SpawnCloudsOnStart(posX);
    }


    // Update is called once per frame
    void Update()
    {
        _logic.Timer(ref _timer, _cloudSpawnDelay, SpawnCloud);
    }

    private void SpawnCloudsOnStart(float posXToStop)
    {
        transform.position = new Vector3(_logic.DeadZone, transform.position.y, transform.position.z);
        while (transform.position.x < posXToStop)
        {
            SpawnCloud();
            transform.position = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
        }

    }
    void SpawnCloud()
    {
        var range = _logic.DefineSpawnRange(transform.position.y, _cloudSpawnOffset);
        float lowestPoint = range[0];
        float highestPoint = range[1];

        range = _logic.DefineSpawnRange(transform.position.z, _cloudSpawnOffset * 3);//range by position z can be wider than the standart offset imho
        float closestPoint = range[0];
        float farthestPoint = range[1];

        int childCount = clouds.transform.childCount;

        Vector3 pos = new Vector3(
            transform.position.x,
            Random.Range(lowestPoint, highestPoint),
            Random.Range(closestPoint, farthestPoint));

        var cloudClone = Instantiate(clouds.transform.GetChild(Random.Range(0, childCount)), pos, transform.rotation);
        cloudClone.transform.parent = cloudContainer.transform;
    }
}
