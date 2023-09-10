using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Image Image;
    private GameObject _volumeSetter;

    private void Awake()
    {
        _volumeSetter = GameObject.FindGameObjectWithTag("Music");
        _volumeSetter.GetComponent<AudioSoucreScript>().SetUpVolume();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetMenuBeckgroundTo(string background)//in the case if we need it from the settings menu
    {
        Image.sprite = Resources.Load<Sprite>(background);
    }
}
