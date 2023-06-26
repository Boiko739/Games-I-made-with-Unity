using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class BirdScrypt : MonoBehaviour
{
    private float _flapStrength = 12;
    private int _timeToChangeSkin = 200;
    private bool _birdIsAlive = true;
    private LogicScrypt _logic;
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

        _logic = GameObject.FindWithTag("Logic").GetComponent<LogicScrypt>();
        _spriteRenderer = GameObject.FindWithTag("Bird").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_logic.GameIsOn && transform.position.y <= 0)
            Flap(2);
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton((short)MouseButton.Left) && _birdIsAlive)
            Flap();
    }
    private async void Flap(short divider = 1)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * _flapStrength / divider;
        await ChangeSkin();
    }
    private async Task ChangeSkin(BirdSkins skin = BirdSkins.FlappyBird)
    {
        if (_spriteRenderer != null)
            _spriteRenderer.sprite = sprites[((short)skin)];
        else return;

        if (skin != BirdSkins.FlappyBird)
            return;

        await Task.Delay(_timeToChangeSkin);

        if (_birdIsAlive)
            await ChangeSkin(BirdSkins.UsualBird);
    }
    private async void OnCollisionEnter2D(Collision2D collision)
    {
        await ChangeSkin(BirdSkins.DeadBird);
        _birdIsAlive = false;
        _logic.GameOver();
    }
}
