using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandlerScript : MonoBehaviour
{
    private static int HighScore;
    private int _maxScoreToIncreaseSpeed = 50;
    private LogicScript _logic;

    private void Start()
    {
        _logic = GameObject.FindWithTag("Logic").GetComponent<LogicScript>();
    }
    [ContextMenu("Increase Score")]
    internal void AddScore()
    {
        _logic.PlayerScore += _logic.ScoreToAdd;
        ShowScore();
        if (_logic.PlayerScore <= _maxScoreToIncreaseSpeed)
            _logic.pipes.GetComponent<PipeMoveScript>().IncreaseSpeed(_logic);
    }

    /// <summary>
    /// Also updates highscore
    /// </summary>
    internal void ShowScore()
    {
        _logic.scoreText.text = _logic.PlayerScore.ToString();
        if (_logic.PlayerScore > HighScore)
        {
            HighScore = _logic.PlayerScore;
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
