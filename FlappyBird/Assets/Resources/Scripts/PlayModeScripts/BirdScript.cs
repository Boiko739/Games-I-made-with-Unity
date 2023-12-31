using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class BirdScript : View
{
    public Sprite[] Sprites;
    private float _flapStrength = 12;
    private int _timeForSkinChanging = 200;
    private bool _birdIsAlive = true;
    private LogicScript _logic;
    private SpriteRenderer _spriteRenderer;

    private enum BirdSkins
    {
        UsualBird,
        FlappingBird,
        DeadBird
    }

    private void Awake()
    {
        gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("volume");
    }

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Sprites[(short)BirdSkins.UsualBird];

        _logic = GameObject.FindWithTag("Logic").GetComponent<LogicScript>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!_logic.PlayerStartedPlaying && transform.position.y <= 0)
            Flap(2);
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown((short)MouseButton.Left)) &&
            _birdIsAlive && !_logic.GameIsPaused)
            Flap();
        if (gameObject.transform.position.y <= -100)
            Destroy(gameObject);
    }

    private async void Flap(short divider = 1)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * _flapStrength / divider;
        await ChangeSkin();
    }

    private async Task ChangeSkin(BirdSkins skin = BirdSkins.FlappingBird)
    {
        if (_spriteRenderer == null)
            return;
        _spriteRenderer.sprite = Sprites[((short)skin)];

        if (skin == BirdSkins.FlappingBird)
        {
            await Task.Delay(_timeForSkinChanging);

            if (_birdIsAlive)
                await ChangeSkin(BirdSkins.UsualBird);
        }
    }

    private void OnBecameInvisible()
    {
        if (_logic != null)
            OnCollisionEnter2D(new Collision2D());
    }

    private async void OnCollisionEnter2D(Collision2D collision)
    {
        if (_birdIsAlive)
            gameObject.GetComponent<AudioSource>().Play();
        await ChangeSkin(BirdSkins.DeadBird);
        ChangeHatCondition();
        _birdIsAlive = false;
        _logic.GameOver();

    }

    private void ChangeHatCondition()
    {
        var hat = transform.GetChild(0);
        hat.GetComponent<FixedJoint2D>().enabled = false;
        hat.GetComponent<FixedJoint2D>().connectedBody = null;
        hat.GetComponent<Rigidbody2D>().mass = 2f;
        hat.GetComponent<Rigidbody2D>().gravityScale = 2f;
    }

    public override void SetSkin(string path)
    {
        for (int i = 0; i < Sprites.Length; i++)
            Sprites[i] = (Sprite)Resources.Load($"Assets/BirdSkins/{path}/{Sprites[i]}");
    }
}
