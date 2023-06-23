using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    private LogicScrypt logic;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindWithTag("Logic").GetComponent<LogicScrypt>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3.left * logic.pipeMoveSpeed) * Time.deltaTime;
        if (transform.position.x < logic.deadZone)
            Destroy(gameObject);
    }
}