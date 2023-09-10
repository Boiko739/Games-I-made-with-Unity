using UnityEngine;

public class CloudMoveScript : MonoBehaviour
{
    private float _cloudMoveSpeed = 5f;
    private LogicScript _logic;

    void Start()
    {
        _logic = GameObject.FindWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update()
    {
        transform.position += (Vector3.left * _cloudMoveSpeed) * Time.deltaTime;
        if (transform.position.x < LogicScript.DEAD_ZONE)
            Destroy(gameObject);
    }
}
