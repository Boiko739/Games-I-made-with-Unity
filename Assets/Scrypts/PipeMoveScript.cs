using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    public float pipeMoveSpeed = 10f;
    private LogicScrypt _logic;
    // Start is called before the first frame update
    void Start()
    {
        _logic = GameObject.FindWithTag("Logic").GetComponent<LogicScrypt>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3.left * pipeMoveSpeed) * Time.deltaTime;
        if (transform.position.x < _logic.DeadZone)
            Destroy(gameObject);
    }
}