using UnityEngine;
using UnityEngine.UI;

public class VolumeScrollbar : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponent<Scrollbar>().value = PlayerPrefs.GetFloat("volume");
    }

    public void SaveVolumeValue()
    {
        float value = gameObject.GetComponent<Scrollbar>().value;
        PlayerPrefs.SetFloat("volume", value);
    }
}
