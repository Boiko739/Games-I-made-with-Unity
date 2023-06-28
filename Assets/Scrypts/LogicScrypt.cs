using System.Collections;
using System.IO;
using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScrypt : MonoBehaviour
{
    #region ReferencesRegion

    public Text scoreText, textClickToPlay, highScoreText;
    public GameObject pipeSpawner, gameOverScreen, pipes;
    private FunctionTimer _timer;

    #endregion ReferencesRegion

    #region VariablesRegion

    private static int HighScore;
    private int _playerScore = 0, _maxScoreToIncreaseSpeed = 50;
    private short _scoreToAdd = 1, _deadZone = -30;
    private float _showHintDelay = 2.5f;
    private bool _gameIsOn = false;

    #endregion VariablesRegion

    public delegate void SpawnDelegate();
    public int PlayerScore { get => _playerScore; set => _playerScore = value; }
    public bool GameIsOn { get => _gameIsOn; }
    public float DeadZone { get => _deadZone; }
    public short ScoreToAdd { get => _scoreToAdd; }

    private void Start()
    {
        LoadHighScore();
        ShowScore();
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
            FunctionTimer.StartAndUpdateTimer(ref _timer, _showHintDelay, ShowHintHowToPlay);
        }
    }
    private async void ShowHintHowToPlay()
    {
        while (!_gameIsOn)
        {
            if (textClickToPlay != null)
            {
                if (textClickToPlay.enabled)
                    textClickToPlay.enabled = false;
                else
                    textClickToPlay.enabled = true;
            }
            await Task.Delay(900);//how fast the text has to flash
        }
        textClickToPlay.enabled = false;
    }

    [ContextMenu("Increase Score")]
    public void AddScore()
    {
        PlayerScore += ScoreToAdd;
        ShowScore();
        if (PlayerScore <= _maxScoreToIncreaseSpeed)
            pipes.GetComponent<PipeMoveScript>().IncreaseSpeed(this);
    }

    /// <summary>
    /// Also updates highscore
    /// </summary>
    private void ShowScore()
    {
        scoreText.text = PlayerScore.ToString();
        if (PlayerScore > HighScore)
        {
            HighScore = PlayerScore;
            SaveHighScore();
        }
        highScoreText.text = $"Best score\n{HighScore}";
    }
    public void RestartGame()
    {
        pipes.GetComponent<PipeMoveScript>().PipeMoveSpeed = 10f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }
    private void LoadHighScore()
    {
        HighScore = PlayerPrefs.GetInt("HighScore");
    }
    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", HighScore);
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
