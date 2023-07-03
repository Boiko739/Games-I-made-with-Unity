using UnityEngine;

public class PipeMiddleScrypt : MonoBehaviour
{
    private LogicScript _logic;
    // Start is called before the first frame update
    void Start()
    {
        _logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3) { }
        _logic.scoreHandler.GetComponent<ScoreHandlerScript>().AddScore();
    }
}
