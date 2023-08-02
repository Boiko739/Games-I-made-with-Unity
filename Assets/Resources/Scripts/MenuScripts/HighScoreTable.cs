using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour
{
    private Transform _entryContainer, _entryTemplate;
    private List<HighscoreEntry> _highscoreEntryList;
    private List<Transform> _highscoreEntryTransformList;


    private void Awake()
    {
        _entryContainer = transform.Find("HighscoreEntryContainer");
        _entryTemplate = _entryContainer.Find("HighscoreEntryTemplate");

        _entryTemplate.gameObject.SetActive(false);

        _highscoreEntryList = LoadHighscores();
        if (_highscoreEntryList == null)
            return;

        SortEntries();
       
        ShowEntries(_highscoreEntryList);
        
        if (_highscoreEntryList.Count >= 6)
            SaveHighscores(_highscoreEntryList.GetRange(0, 6));
        else SaveHighscores(_highscoreEntryList);
    }

    private void ShowEntries(List<HighscoreEntry> highscoreEntries)
    {
        _highscoreEntryTransformList = new List<Transform>();
        short ind = 0;
        foreach (var highscoreEntry in highscoreEntries)
        {
            if (ind == 6)
                break;
            CreateHighscoreEntryTransform(highscoreEntry, _entryContainer, _highscoreEntryTransformList);
            ind++;
        }
    }

    private void SortEntries()
    {
        for (int i = 0; i < _highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < _highscoreEntryList.Count; j++)
            {
                if (_highscoreEntryList[j].score > _highscoreEntryList[i].score)
                {
                    //Swap 
                    var tmp = _highscoreEntryList[i];
                    _highscoreEntryList[i] = _highscoreEntryList[j];
                    _highscoreEntryList[j] = tmp;
                }
            }
        }
    }

    private void SaveHighscores(List<HighscoreEntry> highscoreEntries)
    {
        _highscoreEntryList = highscoreEntries;
        string json = JsonUtility.ToJson(new Highscores(highscoreEntries));
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private List<HighscoreEntry> LoadHighscores()
    {
        return JsonUtility.FromJson<Highscores>(PlayerPrefs.GetString("highscoreTable")).highscoreEntryList;
    }

    public void AddHighscoreEntry(int score)
    {
        var highscoreEntries = LoadHighscores() ?? new List<HighscoreEntry>();
        var date = DateTime.Now.ToShortDateString()[..6] +
            DateTime.Now.Year.ToString()[2..]; //gives dd.mm.yy
        highscoreEntries.Add(new HighscoreEntry { score = score, Date = date });
        SaveHighscores(highscoreEntries);
    }
    private void CreateHighscoreEntryTransform(HighscoreEntry highScoreEntry, Transform container, List<Transform> transformlList)
    {
        short templateHeight = 72;

        Transform entryTransform = Instantiate(_entryTemplate, container);
        entryTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -templateHeight * transformlList.Count);
        entryTransform.gameObject.SetActive(true);

        short rank = (short)(transformlList.Count + 1);
        entryTransform.Find("PositionEntry").GetComponent<Text>().text = rank.ToString();

        int score = highScoreEntry.score;
        entryTransform.Find("ScoreEntry").GetComponent<Text>().text = score.ToString();

        entryTransform.Find("DateEntry").GetComponent<Text>().text = highScoreEntry.Date;

        transformlList.Add(entryTransform);

    }

    public void ResetScoreboard()
    {
        _entryContainer.gameObject.SetActive(false);
        _highscoreEntryTransformList.Clear();
        SaveHighscores(new List<HighscoreEntry>());
        ShowEntries(LoadHighscores());

    }
    [Serializable]
    private sealed class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
        public Highscores(List<HighscoreEntry> highscores)
        {
            highscoreEntryList = highscores;
        }
    }
    [Serializable]
    private sealed class HighscoreEntry
    {
        public int score = 0;
        public string date;
        public string Date { get => date; set => date = value.Replace('/', '.'); }
    }
}