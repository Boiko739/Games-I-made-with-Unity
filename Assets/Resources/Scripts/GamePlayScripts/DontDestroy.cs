using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag($"{gameObject.tag}");

        if (objs.Length > 1)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        gameObject.GetComponent<AudioSoucreScript>().SetUpVolume();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "PlayScene")
            Destroy(gameObject);
    }
}