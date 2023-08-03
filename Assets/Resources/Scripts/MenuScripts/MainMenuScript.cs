using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Image Image;
    public GameObject VolumeSetter;

    private void Start()
    {
        //Setting Up The Volume
        if (!PlayerPrefs.HasKey("volume"))
            PlayerPrefs.SetFloat("volume", 0.7f);
        VolumeSetter.GetComponent<AudioSoucreScript>().SetUpVolume();

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
