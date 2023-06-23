using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class BirdScrypt : MonoBehaviour
{
    public Rigidbody2D myRigidBody2D;
    public Sprite[] sprites;
    public float flapStrength;
    public bool birdIsAlive = true;
    private LogicScrypt logic;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[0];
        logic = GameObject.FindWithTag("Logic").GetComponent<LogicScrypt>();
        spriteRenderer = GameObject.FindWithTag("Bird").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive)
        {
            myRigidBody2D.velocity = Vector2.up * flapStrength;
            spriteRenderer.sprite = sprites[1];
            for (float i = 0; i < 0.5; i++)//timer 0.5 is a time (in seconds) before bird change its skin backward
                i += Time.deltaTime;
            spriteRenderer.sprite = sprites[0];

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        birdIsAlive = false;
        logic.GameOver();
    }
}
