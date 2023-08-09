using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Score
{
    public class HighscoreTable : MonoBehaviour
    {
        private const short TEMPLATE_HEIGHT = 72;
        internal static List<HighscoreEntry> HighscoreEntryList;
        private Transform _entryContainer,
                          _entryTemplate;
        private List<Transform> _highscoreEntryTransformList;

        private void Awake()
        {
            _entryContainer = transform.Find("HighscoreEntryContainer");
            _entryTemplate = _entryContainer.Find("HighscoreEntryTemplate");

            _entryTemplate.gameObject.SetActive(false);

            HighscoreEntryList = HighscoreManager.LoadHighscores();
            if (HighscoreEntryList == null)
                return;
            SortEntries();

            ShowEntries(HighscoreEntryList);

            if (HighscoreEntryList.Count >= 6)
                HighscoreManager.SaveHighscores(HighscoreEntryList.GetRange(0, 6));
            else HighscoreManager.SaveHighscores(HighscoreEntryList);
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
            for (int i = 0; i < HighscoreEntryList.Count; i++)
            {
                for (int j = i + 1; j < HighscoreEntryList.Count; j++)
                {
                    if (HighscoreEntryList[j].Score > HighscoreEntryList[i].Score) //Swap 
                        (HighscoreEntryList[j], HighscoreEntryList[i]) = (HighscoreEntryList[i], HighscoreEntryList[j]);
                }
            }
        }



        private void CreateHighscoreEntryTransform(HighscoreEntry highScoreEntry,
                                                   Transform container, List<Transform> transformlList)
        {
            Transform entryTransform = Instantiate(_entryTemplate, container);
            entryTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -TEMPLATE_HEIGHT * transformlList.Count);
            entryTransform.gameObject.SetActive(true);

            short rank = (short)(transformlList.Count + 1);
            entryTransform.Find("PositionEntry").GetComponent<Text>().text = rank.ToString();

            int score = highScoreEntry.Score;
            entryTransform.Find("ScoreEntry").GetComponent<Text>().text = score.ToString();

            entryTransform.Find("DateEntry").GetComponent<Text>().text = highScoreEntry.Date;

            transformlList.Add(entryTransform);
        }

        public void ResetScoreboard()
        {
            _entryContainer.gameObject.SetActive(false);
            _highscoreEntryTransformList.Clear();
            HighscoreManager.SaveHighscores(new List<HighscoreEntry>());
            ShowEntries(HighscoreManager.LoadHighscores());
        }
    }
}