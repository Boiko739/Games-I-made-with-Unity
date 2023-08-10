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

        private void Start()
        {
            var logic = GameObject.FindWithTag("Logic").GetComponent<LogicScript>();
            _scoreText = logic.ScoreText;
            _highcoreText = logic.HighscoreText;
            Highscore = HighscoreManager.LoadHighscores()[0].Score;
            ShowScore();
        }

        [ContextMenu("Increase Score")]
        internal void AddScore()
        {
            PlayerScore += SCORE_TO_ADD;
            ShowScore();
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