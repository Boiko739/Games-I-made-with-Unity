using UnityEngine;

public class AudioSoucreScript : MonoBehaviour
{
    private void Awake()
    {
        SetUpVolume();
    }
    public void SetUpVolume()
    {
        if (!PlayerPrefs.HasKey("volume"))
            PlayerPrefs.SetFloat("volume", 0.7f);
        gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("volume");
    }
}
