using UnityEngine;

public class PipeMiddleScrypt : MonoBehaviour
{
    private LogicScrypt _logic;
    // Start is called before the first frame update
    void Start()
    {
        _logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScrypt>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
            _logic.AddScore();
    }
}
