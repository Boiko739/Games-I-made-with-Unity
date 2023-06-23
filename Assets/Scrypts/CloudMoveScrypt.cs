using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMoveScrypt : MonoBehaviour
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
        transform.position += (Vector3.left * logic.cloudMoveSpeed) * Time.deltaTime;
        if (transform.position.x < logic.deadZone)
            Destroy(gameObject);
    }
}
