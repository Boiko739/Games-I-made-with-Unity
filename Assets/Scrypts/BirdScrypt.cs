using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class BirdScrypt : MonoBehaviour
{
    public float flapStrength;
    public bool birdIsAlive = true;
    public Sprite[] sprites;
    private Rigidbody2D _myRigidBody2D;
    private LogicScrypt _logic;
    private SpriteRenderer _spriteRenderer;
    private enum BirdSkins
    {
        UsualBird,
        FlappyBird,
        DeadBird
    }

    // Start is called before the first frame update
    void Start()
    {
        _myRigidBody2D = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().sprite = sprites[(short)BirdSkins.UsualBird];
        _logic = GameObject.FindWithTag("Logic").GetComponent<LogicScrypt>();
        _spriteRenderer = GameObject.FindWithTag("Bird").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    async void Update()
    {
        if (!_logic.GameIsOn && transform.position.y <= 0)
        {
            _myRigidBody2D.velocity = Vector2.up * flapStrength / 2f;
            await ChangeSkin();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton((short)MouseButton.Left) && birdIsAlive)
        {
            _myRigidBody2D.velocity = Vector2.up * flapStrength;
            await ChangeSkin();
        }
    }
    private async Task ChangeSkin(BirdSkins skin = BirdSkins.FlappyBird)
    {
        if (skin == BirdSkins.FlappyBird)
            _spriteRenderer.sprite = sprites[((short)skin)];
        else
        {
            _spriteRenderer.sprite = sprites[((short)skin)];
            return;
        }
        await Task.Delay(200);//the time in miliseconds before the bird changes its skin backward

        if (birdIsAlive)
            _spriteRenderer.sprite = sprites[(short)BirdSkins.UsualBird];
    }
    private async void OnCollisionEnter2D(Collision2D collision)
    {
        await ChangeSkin(BirdSkins.DeadBird);
        birdIsAlive = false;
        _logic.GameOver();
    }
}
