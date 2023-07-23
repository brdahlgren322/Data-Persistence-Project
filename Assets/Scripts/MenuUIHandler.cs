using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI lastScoreText;
    public TextMeshProUGUI inputNameTitleText;
    public TextMeshProUGUI inputNamePlaceholderText;
    public TextMeshProUGUI inputNameText;
    // Start is called before the first frame update
    void Start()
    {
        GetHighScore();
        GetLastScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetHighScore()
    {
        MainManager.Instance.LoadHighScore();
        bool nameCheck = !string.IsNullOrEmpty(MainManager.Instance.highScorePlayerName);
        if (!string.IsNullOrEmpty(MainManager.Instance.highScorePlayerName)) 
        {
            highScoreText.SetText("High Score: " +  MainManager.Instance.highScorePlayerName + "-- " + MainManager.Instance.highScore);
        }
        else
        {
            highScoreText.SetText("High Score: None Yet!");
        }
    }

    private void GetLastScore()
    {
        MainManager.Instance.LoadCurrentPlayer();
        bool nameCheck = !string.IsNullOrEmpty(MainManager.Instance.currentPlayerName);
        if (!string.IsNullOrEmpty(MainManager.Instance.currentPlayerName))
        {
            lastScoreText.GetComponentInParent<RectTransform>().sizeDelta = new Vector2(lastScoreText.GetComponentInParent<RectTransform>().rect.width, 100);
            lastScoreText.gameObject.SetActive(true);
            lastScoreText.SetText(MainManager.Instance.currentPlayerName + "'s Score: " + "-- " + MainManager.Instance.currentScore);

            inputNameTitleText.SetText("Still You???");
            inputNameText.SetText(MainManager.Instance.currentPlayerName);
        }
        else
        {
            lastScoreText.GetComponentInParent<RectTransform>().sizeDelta = new Vector2(lastScoreText.GetComponentInParent<RectTransform>().rect.width, 50);
            lastScoreText.gameObject.SetActive(false);

            inputNameTitleText.SetText("Name:");
            inputNamePlaceholderText.SetText("Enter your Name...");
            inputNameText.SetText(string.Empty);

        }

    }

    public void StartNew()
    {
        SceneManager.LoadScene((int)MyGameScenes.Game);
    }

    public void Exit()
    {
        MainManager.Instance.ExitGame();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
