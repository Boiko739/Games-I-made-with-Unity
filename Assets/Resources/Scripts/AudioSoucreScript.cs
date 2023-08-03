using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSoucreScript : MonoBehaviour
{
    public void SetUpVolume()
    {
        gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("volume");
    }
}
