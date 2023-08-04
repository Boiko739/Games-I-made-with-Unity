using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    private LogicScript _logic;

    private const float DEFAULT_PIPE_MOVE_SPEED = 10f;
    private static float _pipeMoveSpeed = 10f;

    public float PipeMoveSpeed { get => _pipeMoveSpeed; private set => _pipeMoveSpeed = value; }

    private void Start()
    {
        _logic = GameObject.FindWithTag("Logic").GetComponent<LogicScript>();
    }

    private void Update()
    {
        transform.position += PipeMoveSpeed * Time.deltaTime * Vector3.left;
        if (transform.position.x < _logic.DeadZone)
            Destroy(gameObject);
    }

    public void IncreaseSpeed()
    {
        PipeMoveSpeed += .1f;
    }

    public void ResetSpeed()
    {
        PipeMoveSpeed = DEFAULT_PIPE_MOVE_SPEED;
    }
}