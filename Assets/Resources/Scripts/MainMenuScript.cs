using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public UnityEngine.UI.Image image;
    public void PlayGame()
    {
        SceneManager.LoadScene("PlayScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void SetMenuBeckgroundTo(string background)
    {
        image.sprite = Resources.Load<Sprite>(background) as Sprite;
    }
}
