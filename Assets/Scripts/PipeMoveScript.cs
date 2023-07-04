using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    private LogicScript _logic;

    private const float PIPE_MOVE_SPEED = 10f;
    private float _pipeMoveSpeed = 10f;

    public float PipeMoveSpeed { get => _pipeMoveSpeed; set => _pipeMoveSpeed = value; }

    // Start is called before the first frame update
    private void Start()
    {
        _logic = GameObject.FindWithTag("Logic").GetComponent<LogicScript>();
    }
    // Update is called once per frame
    private void Update()
    {
        transform.position += Vector3.left * PipeMoveSpeed * Time.deltaTime;
        if (transform.position.x < _logic.DeadZone)
            Destroy(gameObject);
    }
    public void IncreaseSpeed(LogicScript ls)
    {
        GetComponent<PipeMoveScript>().PipeMoveSpeed += ls.ScoreToAdd / 10f;
    }
    public void ResetSpeed()
    {
        PipeMoveSpeed = PIPE_MOVE_SPEED;
    }
}