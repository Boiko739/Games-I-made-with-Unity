using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour
{
    private bool _inProcess = false;

    private void Start()
    {
        ChangeLocale(PlayerPrefs.GetInt("LocaleKey", 0));
    }

    private void ChangeLocale(int localeID)
    {
        if (!_inProcess)
            StartCoroutine(SetLocate(localeID));
    }

    public void SetNextLocale()
    {
        int localeID = PlayerPrefs.GetInt("LocaleKey", 0);
        int totalLocales = LocalizationSettings.AvailableLocales.Locales.Count;

        localeID = localeID == totalLocales - 1 ? 0 : localeID + 1;

        ChangeLocale(localeID);
    }

    IEnumerator SetLocate(int localeID)
    {
        _inProcess = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        PlayerPrefs.SetInt("LocaleKey", localeID);
        _inProcess = false;
    }
}
