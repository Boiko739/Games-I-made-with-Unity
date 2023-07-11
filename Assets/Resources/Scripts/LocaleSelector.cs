using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour
{
    private bool _active = false;
    private void Start()
    {
        ChangeLocale(PlayerPrefs.GetInt("LocaleKey", 0));
    }
    public void ChangeLocale(int localeID)
    {
        if (_active) return;
        StartCoroutine(SetLocate(localeID));
    }
    IEnumerator SetLocate(int localeID)
    {
        _active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        PlayerPrefs.SetInt("LocaleKey", localeID);
        _active = false;
    }
}
