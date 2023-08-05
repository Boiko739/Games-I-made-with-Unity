using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    #region ReferencesRegion

    private GameObject PipeSpawner,
                       GameOverScreen,
                       Pipes,
                       ScoreHandler;

    #endregion ReferencesRegion

    #region VariablesRegion

    private const float SPAWN_OFFSET_INCREASER = 1.5f,
                        MAX_SCORE_TO_INCREASE_SPAWN_OFFSET = 20f,
                        MAX_SCORE_TO_INCREASE_SPEED = 100f,
                        SHOW_HINT_DELAY_AFTER_START = 2.5f,//in seconds
                        FLASH_HINT_DELAY = 1.5f;           //same

    private FunctionTimer _timer;
    private Text _scoreText, _highscoreText;

    #endregion VariablesRegion

    #region PropertiesRegion

    private bool GameIsPaused { get; set; } = false;
    public bool PlayerIsPlaying { get; private set; } = false;
    public float DeadZone { get; } = -30f;
    private bool HintIsShowing { get; set; } = false;
    private TextMeshProUGUI TextClickToPlay { get; set; }
    public Text ScoreText { get => _scoreText; private set => _scoreText = value; }
    public Text HighscoreText { get => _highscoreText; private set => _highscoreText = value; }

    #endregion PropertiesRegion

    public delegate void SpawnDelegate();

    private void Awake()
    {
        var spawners = GameObject.FindGameObjectWithTag("Spawners");
        var canvas = GameObject.FindGameObjectWithTag("Canvas");

        Pipes = GameObject.FindGameObjectWithTag("Pipes");
        PipeSpawner = spawners.transform.Find("PipeSpawner").gameObject;

        ScoreHandler = transform.GetChild(0).gameObject;
        ScoreText = canvas.transform.Find("PlayerScore").GetComponent<Text>();
        HighscoreText = canvas.transform.Find("Highscore").GetComponent<Text>();

        TextClickToPlay = canvas.transform.Find("ClickToPlayText").GetComponent<TextMeshProUGUI>();
        GameOverScreen = canvas.transform.Find("GameOverScreen").gameObject;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Time.timeScale = 1;
                GameIsPaused = false;
            }
            else
            {
                Time.timeScale = 0;
                GameIsPaused = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!PlayerIsPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton((short)MouseButton.Left))
            {
                PipeSpawner.SetActive(true);
                PlayerIsPlaying = true;
                HintIsShowing = false;
                PipeSpawner.GetComponent<PipeSpawnerScript>().SpawnPipes();
                return;
            }
            if (!HintIsShowing)
            {
                FunctionTimer.StartAndUpdateTimer
                    (ref _timer, SHOW_HINT_DELAY_AFTER_START, SetHintIsShowingToTrue);
                if (HintIsShowing)
                    _timer = null;
            }
        }

        if (HintIsShowing) FlashHint();
        else TextClickToPlay.enabled = false;
    }

    private void SetHintIsShowingToTrue()
    {
        HintIsShowing = true;
    }

    private void FlashHint()
    {
        if (!PlayerIsPlaying && TextClickToPlay != null)
            FunctionTimer.StartAndUpdateTimer(ref _timer, FLASH_HINT_DELAY, SwitchTextCondition);
        else
            HintIsShowing = false;
    }

    private void SwitchTextCondition()
    {
        TextClickToPlay.enabled = !TextClickToPlay.enabled;
    }

    public void OnScoreIncreased()
    {
        var sh = ScoreHandler.GetComponent<ScoreHandlerScript>();

        sh.AddScore();
        if (sh.PlayerScore <= MAX_SCORE_TO_INCREASE_SPEED)
            Pipes.GetComponent<PipeMoveScript>().IncreaseSpeed();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Pipes.GetComponent<PipeMoveScript>().ResetSpeed();
    }

    public void OnMainMenuButtonClick()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void GameOver()
    {
        if (!GameOverScreen.IsUnityNull())
            GameOverScreen.SetActive(true);
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

            spawnOffset = playerScore < MAX_SCORE_TO_INCREASE_SPAWN_OFFSET ?
                spawnOffset += SPAWN_OFFSET_INCREASER * (playerScore / MAX_SCORE_TO_INCREASE_SPAWN_OFFSET) :
                spawnOffset += SPAWN_OFFSET_INCREASER;
        }
        range[0] = pos - spawnOffset;
        range[1] = pos + spawnOffset;

        return range;
    }
}
