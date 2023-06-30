using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    #region ReferencesRegion

    public Text scoreText, textClickToPlay, highScoreText;
    public GameObject pipeSpawner, gameOverScreen, pipes, scoreHandler;
    public FunctionTimerScript _timer; 

    #endregion ReferencesRegion

    #region VariablesRegion

    private int _playerScore = 0;
    private short _scoreToAdd = 1, _deadZone = -30;
    private float _showHintDelay = 2.5f;
    private bool _gameIsOn = false;
    private bool _hintIsShowing = false;

    #endregion VariablesRegion

    #region PropsRegion

    public int PlayerScore { get => _playerScore; set => _playerScore = value; }
    public bool GameIsOn { get => _gameIsOn; }
    public float DeadZone { get => _deadZone; }
    public short ScoreToAdd { get => _scoreToAdd; }

    #endregion PropsRegion

    private void Start()
    {
        var sh = scoreHandler.GetComponent<ScoreHandlerScript>();
        sh.LoadHighScore();
        sh.ShowScore();
    }
    private void Update()
    {
        if (!_gameIsOn)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton((short)MouseButton.Left))
            {
                pipeSpawner.SetActive(true);
                _gameIsOn = true;
                return;
            }
            if (!_gameIsOn && !_hintIsShowing)
                _timer.StartAndUpdateTimer(ref _timer, _showHintDelay, ShowHintHowToPlay);
        }
    }
    private void ShowHintHowToPlay()
    {
        _hintIsShowing = true;
        if (textClickToPlay != null)
        {
            if (textClickToPlay.enabled)
                textClickToPlay.enabled = false;
            else
                textClickToPlay.enabled = true;
        }
        if (_gameIsOn)
        {
            _hintIsShowing = false;
            textClickToPlay.enabled = false;
        }
    }
    public void RestartGame()
    {
        pipes.GetComponent<PipeMoveScript>().moveSpeed = 10f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    /// <summary>
    /// Returns the lowest/closest and the highest/farthest points for spawning depends on player csore
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
