using System;
using System.Collections.Generic;
using UnityEngine;

namespace Score
{
    [Serializable]
    internal class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
        public Highscores(List<HighscoreEntry> highscores)
        {
            highscoreEntryList = highscores;
        }
    }

    [Serializable]
    internal class HighscoreEntry
    {
        [SerializeField]
        private int score = 0;
        [SerializeField]
        private string date;
        public HighscoreEntry()
        {
            //empty constructor fot serialization
        }
        public string Date
        {
            get => date; set => date = (value[..6] +
            DateTime.Now.Year.ToString()[2..]).Replace('/', '.');
        }
        public int Score { get => score; set => score = value; }
    }

    public static class HighscoreManager
    {
        internal static void SaveHighscores(List<HighscoreEntry> highscoreEntries)
        {
            HighscoreTable.HighscoreEntryList = highscoreEntries;
            string json = JsonUtility.ToJson(new Highscores(highscoreEntries));
            PlayerPrefs.SetString("highscoreTable", json);
            PlayerPrefs.Save();
        }

        internal static List<HighscoreEntry> LoadHighscores()
        {
            return JsonUtility.FromJson<Highscores>(PlayerPrefs.GetString("highscoreTable")).highscoreEntryList;
        }

        public static void AddHighscoreEntry(int score)
        {
            var highscoreEntries = LoadHighscores() ?? new List<HighscoreEntry>();
            var date = DateTime.Now.ToShortDateString();
            highscoreEntries.Add(new HighscoreEntry { Score = score, Date = date });
            SaveHighscores(highscoreEntries);
        }
    }
}
