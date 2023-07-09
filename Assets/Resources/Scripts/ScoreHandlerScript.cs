using UnityEngine;

public class ScoreHandlerScript : MonoBehaviour
{
    private static int HighScore;

    private int _playerScore = 0;
    private short _scoreToAdd = 1;
    private int _maxScoreToIncreaseSpeed = 100;
    private LogicScript _logic;
    public int PlayerScore { get => _playerScore; set => _playerScore = value; }
    public short ScoreToAdd { get => _scoreToAdd; }

    private void Start()
    {
        _logic = GameObject.FindWithTag("Logic").GetComponent<LogicScript>();
        LoadHighScore();
        ShowScore();
    }
    [ContextMenu("Increase Score")]
    internal void AddScore()
    {
        PlayerScore += ScoreToAdd;
        ShowScore();
        if (PlayerScore <= _maxScoreToIncreaseSpeed)
            _logic.pipes.GetComponent<PipeMoveScript>().IncreaseSpeed(this);
    }

    /// <summary>
    /// Updates the score. Also updates the highscore
    /// </summary>
    internal void ShowScore()
    {
        _logic.scoreText.text = PlayerScore.ToString();
        if (PlayerScore > HighScore)
        {
            HighScore = PlayerScore;
            SaveHighScore();
        }
        _logic.highScoreText.text = $"Best score\n{HighScore}";
    }

    internal void LoadHighScore()
    {
        HighScore = PlayerPrefs.GetInt("HighScore");
    }
    internal void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", HighScore);
    }
}