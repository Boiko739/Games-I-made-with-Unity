using System.Threading.Tasks;
using UnityEngine;

public class BirdScrypt : MonoBehaviour
{
    private Rigidbody2D myRigidBody2D;
    public Sprite[] sprites;
    public float flapStrength;
    public bool birdIsAlive = true;
    private LogicScrypt logic;
    private SpriteRenderer spriteRenderer;
    private enum BirdSkins
    {
        UsualBird,
        FlappyBird,
        DeadBird
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().sprite = sprites[(short)BirdSkins.UsualBird];
        logic = GameObject.FindWithTag("Logic").GetComponent<LogicScrypt>();
        spriteRenderer = GameObject.FindWithTag("Bird").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    async void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive)
        {
            myRigidBody2D.velocity = Vector2.up * flapStrength;
            await ChangeSkin();
        }
    }
    private async Task ChangeSkin(BirdSkins skin = BirdSkins.FlappyBird)
    {
        if (skin == BirdSkins.FlappyBird)
            spriteRenderer.sprite = sprites[((short)skin)];
        else
        {
            spriteRenderer.sprite = sprites[((short)skin)];
            return;
        }
        await Task.Delay(200);//the time in miliseconds before the bird changes its skin backward
        spriteRenderer.sprite = sprites[(short)BirdSkins.UsualBird];
    }
    private async void OnCollisionEnter2D(Collision2D collision)
    {
        await ChangeSkin(BirdSkins.DeadBird);
        birdIsAlive = false;
        logic.GameOver();
    }
}
