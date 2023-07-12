using UnityEngine;

public class CloudSpawnerScript : MonoBehaviour, ISpawner
{
    public GameObject clouds, cloudContainer;
    private readonly float _cloudSpawnDelay = 1f, _cloudSpawnOffset = 6f;
    private FunctionTimer _timer;
    private LogicScript _logic;

    // Start is called before the first frame update
    void Start()
    {
        _logic = GameObject.FindWithTag("Logic").GetComponent<LogicScript>();
        SpawnCloudsOnStart(transform.position.x);
    }
    // Update is called once per frame
    void Update()
    {
        FunctionTimer.StartAndUpdateTimer(ref _timer, _cloudSpawnDelay, SpawnItself);
    }

    private void SpawnCloudsOnStart(float posXToStop)
    {
        transform.position = new Vector3(_logic.DeadZone, transform.position.y, transform.position.z);
        while (transform.position.x < posXToStop)
        {
            SpawnItself();
            transform.position = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
        }

    }
    public void SpawnItself()
    {
        float lowestPoint;
        float highestPoint;
        float closestPoint;
        float farthestPoint;
        var trPos = transform.position;

        var range = _logic.DefineSpawnRange(trPos.y, _cloudSpawnOffset);
        lowestPoint = range[0];
        highestPoint = range[1];

        range = _logic.DefineSpawnRange(trPos.z, _cloudSpawnOffset * 3);//3 because range by z position can
                                                                        //be wider than the offset by y pos imho
        closestPoint = range[0];
        farthestPoint = range[1];

        Vector3 pos = new Vector3(
            trPos.x,
            Random.Range(lowestPoint, highestPoint),
            Random.Range(closestPoint, farthestPoint));

        var cloudClone = Instantiate(clouds.transform.GetChild
                        (Random.Range(0, clouds.transform.childCount)), pos, transform.rotation);
        cloudClone.transform.parent = cloudContainer.transform;
    }
}
