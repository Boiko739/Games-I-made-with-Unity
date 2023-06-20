using UnityEngine;

public class BirdScrypt : MonoBehaviour
{
    public Rigidbody2D myRigidBody2D;
    public float flapStrength;
    public bool birdIsAlive = true;
    public LogicScrypt logic;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScrypt>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive)
        {
            myRigidBody2D.velocity = Vector2.up * flapStrength;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        birdIsAlive = false;
        logic.GameOver();
    }
}
