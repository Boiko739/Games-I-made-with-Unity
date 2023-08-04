using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    #region ReferencesRegion

    public Text ScoreText, TextClickToPlay, HighScoreText;
    public GameObject PipeSpawner, GameOverScreen, Pipes, ScoreHandler, Locale;

    #endregion ReferencesRegion

    #region VariablesRegion

    private const float SPAWN_OFFSET_INCREASER = 1.5f, SCORE_LIMIT_TO_SPEED_UP = 20f;
    private readonly int _maxScoreToIncreaseSpeed = 100;

    private readonly short _deadZone = -30;
    private readonly float _showHintDelayAfterStart = 2.5f,
                           _flashHintDelay = 1.5f;
    private FunctionTimer _timer;

    #endregion VariablesRegion

    #region PropertiesRegion

    private bool GameIsPaused { get; set; } = false;
    public bool PlayerIsPlaying { get; private set; } = false;
    public float DeadZone { get => _deadZone; }
    private bool HintIsShowing { get; set; } = false;

    #endregion PropertiesRegion

    public delegate void SpawnDelegate();

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
                    (ref _timer, _showHintDelayAfterStart, SetHintIsShowingToTrue);
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
            FunctionTimer.StartAndUpdateTimer(ref _timer, _flashHintDelay, SwitchTextCondition);
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
        if (sh.PlayerScore <= _maxScoreToIncreaseSpeed)
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
            //here I increase the spawn offset depend on the limit to speed up
            spawnOffset = playerScore < SCORE_LIMIT_TO_SPEED_UP ?
                spawnOffset += SPAWN_OFFSET_INCREASER * (playerScore / SCORE_LIMIT_TO_SPEED_UP) :
                spawnOffset += SPAWN_OFFSET_INCREASER;
        }
        range[0] = pos - spawnOffset;
        range[1] = pos + spawnOffset;

        return range;
    }
}
