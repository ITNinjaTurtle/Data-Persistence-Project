using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public string PlayerName;
    public int PlayerScore;
    public Highscore CurrentHighscore = new Highscore();

    public class Highscore
    {
        public string PlayerName;
        public int PlayerScore = 0;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighscore();
    }

    // This could be adapted to save the top 5 scores
    [Serializable]
    class SaveData
    {
        public string PlayerName;
        public int PlayerScore;
    }

    public void SaveHighscore()
    {
        SaveData data = new SaveData();

        data.PlayerName = PlayerName;
        data.PlayerScore = PlayerScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/highscores.json", json);
    }

    public void LoadHighscore()
    {
        string path = Application.persistentDataPath + "/highscores.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            CurrentHighscore.PlayerName = data.PlayerName;
            CurrentHighscore.PlayerScore = data.PlayerScore;
        }
    }
}
