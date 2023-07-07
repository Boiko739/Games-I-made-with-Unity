using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MainMenuScript : MonoBehaviour
{
    public UnityEngine.UI.Image image;
    public void PlayGame()
    {
        SceneManager.LoadScene("PlayScene");
    }
    public void GameSettings()
    {

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
