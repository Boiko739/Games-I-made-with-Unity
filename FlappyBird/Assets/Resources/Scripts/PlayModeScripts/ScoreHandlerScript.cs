using UnityEngine;
using UnityEngine.UI;

namespace Score
{
    public class ScoreHandlerScript : MonoBehaviour
    {
        private static int Highscore;
        private Text _scoreText,
                     _highcoreText;
        public int PlayerScore { get; private set; } = 0;
        public const short SCORE_TO_ADD = 1;
        private LogicScript _logic;

        private void Start()
        {
            _logic = GameObject.FindWithTag("Logic").GetComponent<LogicScript>();
            _scoreText = _logic.ScoreText;
            _highcoreText = _logic.HighscoreText;

            Highscore = HighscoreManager.LoadHighscores().Capacity > 0 ?
                HighscoreManager.LoadHighscores()[0].Score : 0;
            ShowScore();
        }

        internal void AddScore()
        {
            PlayerScore += SCORE_TO_ADD;
            ShowScore();
        }
        [ContextMenu("Increase Score 100 times")]
        internal void AddScore100()
        {
            for (int i = 0; i < 100; i++)
                _logic.OnScoreIncreased();
        }

        /// <summary>
        /// Updates the score and the highscore
        /// </summary>
        internal void ShowScore()
        {
            _scoreText.text = PlayerScore.ToString();
            if (PlayerScore > Highscore)
                Highscore = PlayerScore;
            _highcoreText.text = $"{Highscore}";
        }
    }
}