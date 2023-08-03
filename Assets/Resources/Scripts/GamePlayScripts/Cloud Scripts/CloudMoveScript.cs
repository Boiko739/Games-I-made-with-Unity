using UnityEngine;

public class CloudMoveScript : MonoBehaviour
{
    private float _cloudMoveSpeed = 5f;
    private LogicScript _logic;
    // Start is called before the first frame update
    void Start()
    {
        _logic = GameObject.FindWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3.left * _cloudMoveSpeed) * Time.deltaTime;
        if (transform.position.x < _logic.DeadZone)
            Destroy(gameObject);
    }
}
