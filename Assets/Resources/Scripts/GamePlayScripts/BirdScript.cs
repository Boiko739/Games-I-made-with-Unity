using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class BirdScript : View
{
    public Sprite[] sprites;
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
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[(short)BirdSkins.UsualBird];

        _logic = GameObject.FindWithTag("Logic").GetComponent<LogicScript>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_logic.PlayerIsPlaying && transform.position.y <= 0)
            Flap(2);
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown((short)MouseButton.Left) && _birdIsAlive)
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
        _spriteRenderer.sprite = sprites[((short)skin)];

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
        _birdIsAlive = false;
        _logic.GameOver();
    }

    public override void SetSkin(string path)
    {
        for (int i = 0; i < sprites.Length; i++)
            sprites[i] = (Sprite)Resources.Load($"Assets/BirdSkins/{path}/{sprites[i]}");
    }
}
