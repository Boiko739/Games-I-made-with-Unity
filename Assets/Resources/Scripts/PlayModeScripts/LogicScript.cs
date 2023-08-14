using Score;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public const float DEAD_ZONE = -30f;

    private GameObject GameOverScreen,
                       ScoreHandler,
                       PauseMenu;

    private const float SPAWN_OFFSET_INCREASER = 1.5f,
                        SCORE_LIMIT_TO_INCREASE_SPAWN_OFFSET = 20f,
                        SCORE_LIMIT_TO_INCREASE_SPEED = 100f,
                        SHOW_HINT_DELAY_AFTER_START = 2.5f,//in seconds
                        FLASH_HINT_DELAY = 1.5f;           //this one too

    private bool _hintIsShowing = false;

    private FunctionTimer _timer;
    private Text _scoreText;
    private TextMeshProUGUI _textClickToPlay;

    public bool GameIsOver { get; private set; } = false;
    public bool GameIsPaused { get; private set; } = false;
    public bool PlayerStartedPlaying { get; private set; } = false;
    public GameObject Pipes { get; private set; }
    public GameObject PipeSpawner { get; private set; }
    public Text ScoreText { get => _scoreText; private set => _scoreText = value; }
    public Text HighscoreText { get; private set; }

    public delegate void SpawnDelegate();

    private void Start()
    {
        var spawners = GameObject.FindGameObjectWithTag("Spawners").transform;
        var canvas = GameObject.FindGameObjectWithTag("Canvas").transform;

        Pipes = Resources.Load<GameObject>("Sprites/Pipes");
        PipeSpawner = spawners.Find("PipeSpawner").gameObject;

        PauseMenu = canvas.Find("PauseMenu").gameObject;

        ScoreHandler = this.transform.GetChild(0).gameObject;
        ScoreText = canvas.Find("PlayerScore").GetComponent<Text>();
        HighscoreText = canvas.Find("Highscore").GetComponent<Text>();

        _textClickToPlay = canvas.Find("HintText").GetComponent<TextMeshProUGUI>();
        GameOverScreen = canvas.Find("GameOverScreen").gameObject;

        var bird = GameObject.FindGameObjectWithTag("Bird");
        bird.GetComponent<BirdScript>().Sprites = ChangeSpriteScript.GameSprites[..3];

        bird.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite =  //Hat
            ChangeSpriteScript.GameSprites[3];

        GameObject.FindWithTag("MainCamera").transform.GetChild(0).GetComponent<SpriteRenderer>().sprite =
            ChangeSpriteScript.GameSprites[4];  //Background

        Pipes.transform.Find("TopPipe").GetComponent<SpriteRenderer>().sprite =
            ChangeSpriteScript.GameSprites[5];
        Pipes.transform.Find("BottomPipe").GetComponent<SpriteRenderer>().sprite =
            ChangeSpriteScript.GameSprites[5];

        Time.timeScale = 1;
    }


    private void Update()
    {
        CheckPauseResume();

        if (!PlayerStartedPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton((short)MouseButton.Left))
            {
                OnPlayerStartedPlaying();
                return;
            }
            if (!_hintIsShowing)
            {
                FunctionTimer.StartAndUpdateTimer
                    (ref _timer, SHOW_HINT_DELAY_AFTER_START, SetHintIsShowingToTrue);
                if (_hintIsShowing)
                    _timer = null;
            }
        }

        if (_hintIsShowing) FlashHint();
        else _textClickToPlay.enabled = false;
    }

    private void OnPlayerStartedPlaying()
    {
        PipeSpawner.SetActive(true);
        PlayerStartedPlaying = true;
        _hintIsShowing = false;
        PipeSpawner.GetComponent<PipeSpawnerScript>().SpawnPipes();
    }

    private void CheckPauseResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseResume();
    }

    public void PauseResume()
    {
        Time.timeScale = GameIsPaused ? 1 : 0;
        GameIsPaused = !GameIsPaused;
        PauseMenu.SetActive(!PauseMenu.activeInHierarchy);
    }

    private void SetHintIsShowingToTrue()
    {
        _hintIsShowing = true;
    }

    private void FlashHint()
    {
        if (!PlayerStartedPlaying && _textClickToPlay != null)
            FunctionTimer.StartAndUpdateTimer(ref _timer, FLASH_HINT_DELAY, SwitchTextCondition);
        else
            _hintIsShowing = false;
    }

    private void SwitchTextCondition()
    {
        _textClickToPlay.enabled = !_textClickToPlay.enabled;
    }

    public void OnScoreIncreased()
    {
        var scoreHandler = ScoreHandler.GetComponent<ScoreHandlerScript>();

        scoreHandler.AddScore();
        if (scoreHandler.PlayerScore <= SCORE_LIMIT_TO_INCREASE_SPEED)
            Pipes.GetComponent<PipeMoveScript>().IncreaseSpeed();
    }

    [ContextMenu("Restart Game")]
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Pipes.GetComponent<PipeMoveScript>().ResetSpeed();
    }

    public void OnMainMenuButtonClick()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    [ContextMenu("Game Over")]
    public void GameOver()
    {
        if (!GameOverScreen.IsUnityNull())
        {
            if (!GameOverScreen.activeInHierarchy)
                HighscoreManager.AddHighscoreEntry(ScoreHandler.GetComponent<ScoreHandlerScript>().PlayerScore);
            GameOverScreen.SetActive(true);
            GameIsOver = true;
        }
    }

    /// <summary>
    /// Returns the lowest, closest and the highest, farthest points for spawning depends on the player csore
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="spawnOffset"></param>
    /// <returns></returns>
    public float[] DefineSpawnRange(float pos, float spawnOffset, bool pipeIsCalling = false)
    {
        float[] range = new float[2];
        if (pipeIsCalling)
        {
            var playerScore = ScoreHandler.GetComponent<ScoreHandlerScript>().PlayerScore;

            spawnOffset = playerScore < SCORE_LIMIT_TO_INCREASE_SPAWN_OFFSET ?
                spawnOffset += SPAWN_OFFSET_INCREASER * (playerScore / SCORE_LIMIT_TO_INCREASE_SPAWN_OFFSET) :
                spawnOffset += SPAWN_OFFSET_INCREASER;
        }
        range[0] = pos - spawnOffset;
        range[1] = pos + spawnOffset;

        return range;
    }
}
