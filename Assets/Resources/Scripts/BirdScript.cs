using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    private float _flapStrength = 12;
    private int _timeForSkinChanging = 200;
    private bool _birdIsAlive = true;
    private LogicScript _logic;
    private SpriteRenderer _spriteRenderer;
    public Sprite[] sprites;

    private enum BirdSkins
    {
        UsualBird,
        FlappyBird,
        DeadBird
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[(short)BirdSkins.UsualBird];

        _logic = GameObject.FindWithTag("Logic").GetComponent<LogicScript>();
        _spriteRenderer = GameObject.FindWithTag("Bird").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_logic.GameIsOn && transform.position.y <= 0)
            Flap(2);
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown((short)MouseButton.Left) && _birdIsAlive)
            Flap();
    }
    private void OnBecameInvisible()
    {
        if (_logic != null)
            OnCollisionEnter2D(new Collision2D());
    }
    private async void Flap(short divider = 1)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * _flapStrength / divider;
        await ChangeSkin();
    }
    private async Task ChangeSkin(BirdSkins skin = BirdSkins.FlappyBird)
    {
        if (_spriteRenderer == null)
            return;
        _spriteRenderer.sprite = sprites[((short)skin)];

        if (skin == BirdSkins.FlappyBird)
        {
            await Task.Delay(_timeForSkinChanging);

            if (_birdIsAlive)
                await ChangeSkin(BirdSkins.UsualBird);
        }
    }
    private async void OnCollisionEnter2D(Collision2D collision)
    {
        if (_birdIsAlive)
            gameObject.GetComponent<AudioSource>().Play();
        await ChangeSkin(BirdSkins.DeadBird);
        _birdIsAlive = false;
        _logic.GameOver();
    }
}
