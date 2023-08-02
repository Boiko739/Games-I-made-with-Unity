using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");

        if (objs.Length > 1)
            Destroy(gameObject);
        objs[0].GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("volume");
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "PlayScene")
            Destroy(gameObject);
    }
}