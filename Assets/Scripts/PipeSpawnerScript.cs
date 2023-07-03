using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PipeSpawnerScript : MonoBehaviour
{
<<<<<<< Updated upstream:Assets/Scripts/PipeSpawnerScript.cs
    private float _pipeSpawnDelay = 2f;
    private float _pipeSpawnOffset = 4f;
    public FunctionTimerScript timer;
    public GameObject pipe;
    public GameObject pipeContainer;
=======
    private float _pipeSpawnDelay = 2f, _pipeSpawnOffset = 4f;
    public GameObject pipe, pipeContainer;
>>>>>>> Stashed changes:Assets/Scripts/PipeSpawnerScrypt.cs
    private LogicScript _logic;
    private FunctionTimer _timer;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        _logic = GameObject.FindWithTag("Logic").GetComponent<LogicScript>();
        SpawnPipes();
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream:Assets/Scripts/PipeSpawnerScript.cs
        timer.StartAndUpdateTimer(ref timer, _pipeSpawnDelay, SpawnPipes);
=======
>>>>>>> Stashed changes:Assets/Scripts/PipeSpawnerScrypt.cs
    }
    
    private void SpawnPipes()
    {
        while (true)
        {
            float lowestPoint;
            float highestPoint;

            var range = _logic.DefineSpawnRange(transform.position.y, _pipeSpawnOffset);
            lowestPoint = range[0];
            highestPoint = range[1];

            Vector3 pos = new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0);

            var pipeClone = Instantiate(pipe, pos, transform.rotation);
            pipeClone.transform.parent = pipeContainer.transform;

            StartCoroutine(CoroutineWaitIt(_pipeSpawnDelay));
        }
    }
    private IEnumerator CoroutineWaitIt(float delay, System.Action action = null)
    {
        yield return new WaitForSeconds(delay);
        if (action != null)
            action();
    }
}
