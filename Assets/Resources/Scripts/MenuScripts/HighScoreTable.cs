using System;
using System.Collections.Generic;
using UnityEditor.Build.Pipeline;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    private Transform _entryContainer, _entryTemplate;
    private List<HighscoreEntry> _highscoreEntryList;
    private List<Transform> _highScoreEntryTransformList;


    private void Awake()
    {
        _entryContainer = transform.Find("HighScoreEntryContainer");
        _entryTemplate = _entryContainer.Find("HighScoreEntryTemplate");

        _entryTemplate.gameObject.SetActive(false);

        _highscoreEntryList = new List<HighscoreEntry>()
        {
            new HighscoreEntry{score = 3245, Date = "12/23/2023"},
            new HighscoreEntry{score = 2340, Date = "10/19/2023"},
            new HighscoreEntry{score = 3043, Date = "11/20/2023"},
            new HighscoreEntry{score = 2450, Date = "01/08/2023"},
            new HighscoreEntry{score = 2950, Date = "14/19/2023"},
            new HighscoreEntry{score = 10000, Date = DateTime.Now.ToString()}
        };
        for (var i = 0; i < _highscoreEntryList.Count; i++)
        {
            for (int j = 0; j < _highscoreEntryList.Count; j++)
            {
                if (_highscoreEntryList[i].score > _highscoreEntryList[j].score)
                {
                    //Swap 
                    var tmp = _highscoreEntryList[j];
                    _highscoreEntryList[j] = _highscoreEntryList[i];
                    _highscoreEntryList[i] = tmp;
                }
            }
        }

        var highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in _highscoreEntryList)
            CreateHighScoreEntryTransform(highscoreEntry, _entryContainer, highscoreEntryTransformList);

        PlayerPrefs.SetString("highscoreTable","");
        PlayerPrefs.Save();
        PlayerPrefs.GetString("highscoreTable");
    }
    private void CreateHighScoreEntryTransform(HighscoreEntry highScoreEntry, Transform container, List<Transform> transformlList)
    {
        short templateHeight = 72;

        Transform entryTransform = Instantiate(_entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformlList.Count);
        entryTransform.gameObject.SetActive(true);

        short rank = (short)(transformlList.Count + 1);
        entryTransform.Find("PositionEntry").GetComponent<Text>().text = rank.ToString();

        var score = highScoreEntry.score;
        entryTransform.Find("ScoreEntry").GetComponent<Text>().text = score.ToString();

        entryTransform.Find("DateEntry").GetComponent<Text>().text = highScoreEntry.Date;

        transformlList.Add(entryTransform);
    }
    [Serializable]
    private class HighscoreEntry
    {
        public int score;
        private string date;
        public string Date { get => date; set => date = value.Replace('/','.'); }
    }
}