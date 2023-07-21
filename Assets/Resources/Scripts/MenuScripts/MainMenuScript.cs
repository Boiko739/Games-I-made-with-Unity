using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public UnityEngine.UI.Image image;
    private void Start()
    {
        //SetMenuBeckgroundTo("Sprites/Backgrounds/MenuBackground1");
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("PlayScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void SetMenuBeckgroundTo(string background)//in the case if need it from menu settings
    {
        image.sprite = Resources.Load<Sprite>(background) as Sprite;
    }
}
