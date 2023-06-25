using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScrypt : MonoBehaviour
{
    #region ReferencesRegion

    public Text scoreText;
    public Text textClickToPlay;
    public GameObject pipeSpawner;
    public GameObject gameOverScreen;

    #endregion ReferencesRegion

    #region VariablesRegion

    private int _playerScore = 0;
    private short _scoreToAdd = 1;
    private float _timer = 0f;
    private bool _textClickToPlayIsEnabled = false;
    private short _deadZone = -30;
    private bool _gameIsOn = false;

    #endregion VariablesRegion

    public delegate void SpawnDelegate();
    public float DeadZone { get => _deadZone; }
    public bool GameIsOn { get => _gameIsOn; }
    private void Update()
    {
        if (!_gameIsOn)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton((short)MouseButton.Left))
            {
                pipeSpawner.SetActive(true);
                _gameIsOn = true;
            }
            if (!_textClickToPlayIsEnabled)
            {
                Timer(ref _timer, 3f, ShowHintHowToPlay, true);
            }
        }
    }
    private async void ShowHintHowToPlay()
    {
        while (!_gameIsOn)
        {
            if (textClickToPlay.enabled)
                textClickToPlay.enabled = false;
            else
                textClickToPlay.enabled = true;
            await Task.Delay(800);//how fast the text has to flash
        }
        textClickToPlay.enabled = false;
    }
    public void AddScore()
    {
        _playerScore += _scoreToAdd;
        scoreText.text = _playerScore.ToString();
    }
    public void RestartGame()
    {
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
        if (_playerScore < 20)
        {
            range[0] = posY - spawnOffset;
            range[1] = posY + spawnOffset;
        }
        else
        {
            spawnOffset += 1.5f;
            range[0] = posY - spawnOffset;
            range[1] = posY + spawnOffset;
        }
        return range;
    }

    /// <summary>
    /// Made for using inside Update() method
    /// </summary>
    /// <param name="timer"></param>
    /// <param name="delay"></param>
    /// <param name="doSmthReturnNth"></param>
    /// <param name="forLogic"></param>
    public void Timer(ref float timer, float delay, SpawnDelegate doSmthReturnNth, bool forLogic = false)
    {
        if (forLogic)
        {
            if (timer < delay)
                timer += Time.deltaTime;
            else
            {
                doSmthReturnNth();
                _textClickToPlayIsEnabled = true;
            }
            return;
        }
        if (timer < delay)
            timer += Time.deltaTime;
        else
        {
            doSmthReturnNth();
            timer = 0;
        }
    }
}
