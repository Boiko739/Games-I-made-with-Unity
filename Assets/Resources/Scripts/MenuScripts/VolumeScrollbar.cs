using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeScrollbar : MonoBehaviour
{
    private void OnMouseUp()
    {
        var value = gameObject.transform.parent.transform.parent.GetComponent<Scrollbar>().value;
        PlayerPrefs.SetFloat("volume", value);
    }
}
