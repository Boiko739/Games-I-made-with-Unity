using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    private float _flapStrength = 12, _timeToChangeSkin = 0.2f;
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
            Flap(strengthDivider: 2);
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown((short)MouseButton.Left) && _birdIsAlive)
            Flap();
    }
    private void Flap(short strengthDivider = 1)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * _flapStrength / strengthDivider;
        StartCoroutine(ChangeSkin());
    }
    private IEnumerator ChangeSkin(BirdSkins skin = BirdSkins.FlappyBird)
    {
        if (_spriteRenderer == null)
            yield break;
        _spriteRenderer.sprite = sprites[((short)skin)];

        if (skin == BirdSkins.FlappyBird)
        {
            yield return new WaitForSeconds(_timeToChangeSkin);

            if (_birdIsAlive)
                StartCoroutine(ChangeSkin(BirdSkins.UsualBird));
        }
    }
    private void OnBecameInvisible()
    {
        if (_logic != null)
            OnCollisionEnter2D(new Collision2D());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ChangeSkin(BirdSkins.DeadBird));
        _birdIsAlive = false;
        _logic.GameOver();
    }
}
