using UnityEngine;

public class PipeMiddleScript : MonoBehaviour
{
    private LogicScript _logic;

    void Start()
    {
        _logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
            _logic.OnScoreIncreased();
    }
}
