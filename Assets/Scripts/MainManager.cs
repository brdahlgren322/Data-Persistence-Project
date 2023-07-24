using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public enum MyGameScenes { Menu = 0, Game = 1}
public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public string currentPlayerName;
    public string highScorePlayerName;
    public int highScore;
    public int currentScore;
    public bool isAlive;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        isAlive = true;
        DontDestroyOnLoad(gameObject);
        LoadCurrentPlayer();
        LoadHighScore();
    }

    [System.Serializable]
    public class CurrentPlayerData
    {
        public string currentPlayerName;
        public int currentScore;
    }

    [System.Serializable]
    public class HighScoreData
    {
        public string highScorePlayerName;
        public int highScore;
    }

    public void ExitGame()
    {
        SaveCurrentPlayer();
        SaveHighScore();
        isAlive = false;
    }

    public void SaveCurrentPlayer()
    {
        SaveCurrentPlayer(currentPlayerName, currentScore);
    }

    public void SaveCurrentPlayer(string name, int score)
    {
        CurrentPlayerData data = new CurrentPlayerData();
        data.currentPlayerName = name;
        data.currentScore = score;
        
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/cp_savefile.json", json );
    }

    public void SaveHighScore()
    {
        SaveHighScore(highScorePlayerName, highScore);
    }
    public void SaveHighScore(string name, int score)
    {
        if(name != highScorePlayerName || score != highScore)
        {
            highScorePlayerName = name;
            highScore = score;
        }
        HighScoreData data = new HighScoreData();
        data.highScorePlayerName = highScorePlayerName;
        data.highScore = highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/hs_savefile.json", json);
    }

    public void LoadCurrentPlayer()
    {
        string path = Application.persistentDataPath + "/cp_savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            CurrentPlayerData data = JsonUtility.FromJson<CurrentPlayerData>(json);

            currentPlayerName = data.currentPlayerName;
            currentScore = data.currentScore;
        }
        Debug.Log("Loaded Current Player Data");
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/hs_savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScoreData data = JsonUtility.FromJson<HighScoreData>(json);

            highScorePlayerName = data.highScorePlayerName;
            highScore = data.highScore;
        }
        Debug.Log("Loaded HighScore Player Data");
    }
}
