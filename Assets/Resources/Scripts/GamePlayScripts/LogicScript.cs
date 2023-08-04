using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    #region ReferencesRegion

    public Text scoreText, textClickToPlay, highScoreText;
    public GameObject pipeSpawner, gameOverScreen, pipes, scoreHandler, locale;

    #endregion ReferencesRegion

    #region VariablesRegion

    private const float SPAWN_OFFSET_INCREASER = 1.5f, SCORE_LIMIT_TO_SPEED_UP = 20f;
    private readonly int _maxScoreToIncreaseSpeed = 100;

    private readonly short _deadZone = -30;
    private readonly float _showHintDelayAfterStart = 2.5f;
    private readonly float _flashHintDelay = 1.5f;
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
                pipeSpawner.SetActive(true);
                PlayerIsPlaying = true;
                HintIsShowing = false;
                pipeSpawner.GetComponent<PipeSpawnerScript>().SpawnPipes();
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
        else textClickToPlay.enabled = false;
    }
    private void SetHintIsShowingToTrue()
    {
        HintIsShowing = true;
    }
    private void FlashHint()
    {
        if (!PlayerIsPlaying && textClickToPlay != null)
            FunctionTimer.StartAndUpdateTimer(ref _timer, _flashHintDelay, SwitchTextCondition);
        else
            HintIsShowing = false;
    }
    private void SwitchTextCondition()
    {
        textClickToPlay.enabled = !textClickToPlay.enabled;
    }
    public void OnScoreIncreased()
    {
        var sh = scoreHandler.GetComponent<ScoreHandlerScript>();

        sh.AddScore();
        if (sh.PlayerScore <= _maxScoreToIncreaseSpeed)
            pipes.GetComponent<PipeMoveScript>().IncreaseSpeed();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        pipes.GetComponent<PipeMoveScript>().ResetSpeed();
    }
    public void OnMainMenuButtonClick()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    public void GameOver()
    {
        if (!gameOverScreen.IsUnityNull())
            gameOverScreen.SetActive(true);
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
            var playerScore = scoreHandler.GetComponent<ScoreHandlerScript>().PlayerScore;
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
