using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager
{
    private const int maxEntries = 5;

    public static void SubmitScore(string level, string name, float time, int cherries)
    {
        List<Entry> entries = LoadScores(level);

        entries.Add(new Entry(name, time, cherries));

        entries.Sort((a, b) =>
        {
            int timeCompare = a.time.CompareTo(b.time);
            if (timeCompare == 0)
                return b.cherries.CompareTo(a.cherries); 
            return timeCompare;
        });

        if (entries.Count > maxEntries)
            entries.RemoveRange(maxEntries, entries.Count - maxEntries);

        for (int i = 0; i < entries.Count; i++)
        {
            PlayerPrefs.SetString($"{level}_Score_{i}_Name", entries[i].name);
            PlayerPrefs.SetFloat($"{level}_Score_{i}_Time", entries[i].time);
            PlayerPrefs.SetInt($"{level}_Score_{i}_Cherry", entries[i].cherries);
        }

        PlayerPrefs.Save();
    }

    public static List<Entry> LoadScores(string level)
    {
        List<Entry> result = new List<Entry>();
        for (int i = 0; i < maxEntries; i++)
        {
            string nameKey = $"{level}_Score_{i}_Name";
            string timeKey = $"{level}_Score_{i}_Time";
            string cherryKey = $"{level}_Score_{i}_Cherries";

            if (PlayerPrefs.HasKey(nameKey) && PlayerPrefs.HasKey(timeKey) && PlayerPrefs.HasKey(cherryKey))
            {
                string name = PlayerPrefs.GetString(nameKey);
                float time = PlayerPrefs.GetFloat(timeKey);
                int cherries = PlayerPrefs.GetInt(cherryKey);
                result.Add(new Entry(name, time, cherries));
            }
        }
        return result;
    }

    public class Entry
    {
        public string name;
        public float time;
        public int cherries;

        public Entry(string name, float time, int cherries)
        {
            this.name = name;
            this.time = time;
            this.cherries = cherries;
        }
    }
}
