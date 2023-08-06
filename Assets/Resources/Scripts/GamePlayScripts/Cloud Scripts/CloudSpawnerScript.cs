using UnityEngine;

public class CloudSpawnerScript : MonoBehaviour, ISpawner
{
    public GameObject Clouds, 
                      CloudContainer;
    private readonly float _cloudSpawnDelay = 1f,
                           _cloudSpawnOffset = 6f;
    private FunctionTimer _timer;
    private LogicScript _logic;

    void Start()
    {
        _logic = GameObject.FindWithTag("Logic").GetComponent<LogicScript>();
        SpawnCloudsOnStart(transform.position.x);
    }

    void Update()
    {
        FunctionTimer.StartAndUpdateTimer(ref _timer, _cloudSpawnDelay, SpawnItself);
    }

    private void SpawnCloudsOnStart(float xPositionToStop)
    {
        transform.position = new Vector3(LogicScript.DEAD_ZONE, transform.position.y, transform.position.z);
        while (transform.position.x < xPositionToStop)
        {
            SpawnItself();
            transform.position = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
        }
    }

    public void SpawnItself()
    {
        float lowestPoint, highestPoint,
              closestPoint, farthestPoint;

        float[] range = _logic.DefineSpawnRange(transform.position.y, _cloudSpawnOffset);
        lowestPoint = range[0];
        highestPoint = range[1];

        range = _logic.DefineSpawnRange(transform.position.z, _cloudSpawnOffset * 3);//3 because range by z position can
                                                                        //be wider than the offset by y pos imho
        closestPoint = range[0];
        farthestPoint = range[1];

        Vector3 pos = new(transform.position.x,
            Random.Range(lowestPoint, highestPoint),
            Random.Range(closestPoint, farthestPoint));

        var cloudClone = Instantiate(Clouds.transform.GetChild
                        (Random.Range(0, Clouds.transform.childCount)),
                        pos, transform.rotation);
        cloudClone.transform.parent = CloudContainer.transform;
    }
}
