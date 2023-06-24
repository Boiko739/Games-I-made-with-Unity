using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScrypt : MonoBehaviour
{
    public int playerScore = 0;
    private int _scoreToAdd = 1;
    public Text scoreText;
    public GameObject gameOverScreen;
    public delegate void SpawnDelegate();


    private float _deadZone = -30f;
    public float DeadZone { get => _deadZone; set => _deadZone = value; }

    public void Timer(ref float timer, float delay, SpawnDelegate spawnSmth)
    {
        if (timer < delay)
            timer += Time.deltaTime;
        else
        {
            spawnSmth();
            timer = 0;
        }
    }

    public void AddScore()
    {
        playerScore += _scoreToAdd;
        scoreText.text = playerScore.ToString();
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
        if (playerScore < 20)
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
}
