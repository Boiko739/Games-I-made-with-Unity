using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    #region ReferencesRegion

    public Text scoreText, textClickToPlay, highScoreText;
    public GameObject pipeSpawner, gameOverScreen, pipes, scoreHandler;

    #endregion ReferencesRegion

    #region VariablesRegionKE

    private int _playerScore = 0;
    private short _scoreToAdd = 1, _deadZone = -30;
    private float _showHintDelayAfterStart = 2.5f;
    private float _flashHintDelay = 1f;
    private bool _hintIsShowing = false;
    private bool _gameIsOn = false;
    private FunctionTimer _timer;

    #endregion VariablesRegion

    #region PropsRegion

    public delegate void SpawnDelegate();
    public int PlayerScore { get => _playerScore; set => _playerScore = value; }
    public bool GameIsOn { get => _gameIsOn; private set => _gameIsOn = value; }
    public float DeadZone { get => _deadZone; }
    public short ScoreToAdd { get => _scoreToAdd; }
    public bool HintIsShowing { get => _hintIsShowing; set => _hintIsShowing = value; }

    #endregion PropsRegion

    private void Start()
    {

    }
    private void Update()
    {
        if (!_gameIsOn)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton((short)MouseButton.Left))
            {
                pipeSpawner.SetActive(true);
                GameIsOn = true;
                HintIsShowing = false;
                pipes.GetComponent<PipeSpawnerScrypt>().SpawnPipes();
                return;
            }
            if (!HintIsShowing)
            {
                FunctionTimer.StartAndUpdateTimer(ref _timer, _showHintDelayAfterStart, SetHintIsShowingToTrue);
                if (HintIsShowing)
                    _timer = null;
            }
        }
        if (HintIsShowing)
            FlashHint();
        else textClickToPlay.enabled = false;

    }
    private void SetHintIsShowingToTrue()
    {
        HintIsShowing = true;
    }
    private void FlashHint()
    {
        if (!GameIsOn && textClickToPlay != null)
        {
            FunctionTimer.StartAndUpdateTimer(ref _timer, _flashHintDelay, SwitchTextCondition);
        }
        else
            HintIsShowing = false;
    }
    private void SwitchTextCondition()
    {
        textClickToPlay.enabled = !textClickToPlay.enabled;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        pipes.GetComponent<PipeMoveScript>().ResetSpeed();
    }
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    /// <summary>
    /// Returns the lowest, closest and the highest, farthest points for spawning depends on player csore
    /// </summary>
    /// <param name="posY"></param>
    /// <param name="spawnOffset"></param>
    /// <returns></returns>
    public float[] DefineSpawnRange(float posY, float spawnOffset)
    {
        float[] range = new float[2];

        if (PlayerScore > 20)
            spawnOffset += 1.5f;

        range[0] = posY - spawnOffset;
        range[1] = posY + spawnOffset;

        return range;
    }
}
