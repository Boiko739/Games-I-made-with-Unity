using UnityEngine;

public class PipeMiddleScript : MonoBehaviour
{
    public LogicScrypt logic;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScrypt>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
<<<<<<<< Updated upstream:Assets/Scrypts/PipeMiddleScrypt.cs
            logic.AddScore(1);
========
            _logic.scoreHandler.GetComponent<ScoreHandlerScript>().AddScore();
>>>>>>>> Stashed changes:Assets/Scrypts/PipeMiddleScript.cs
    }
}
