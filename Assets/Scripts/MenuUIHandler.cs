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
    public TextMeshProUGUI warningText;
    public TMP_InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        GetHighScore();
        GetLastScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (warningText.gameObject.activeInHierarchy)
        {
            if(!string.IsNullOrEmpty(inputNameText.text) && inputNameText.text.Trim().Length > 1) 
            {
                warningText.gameObject.SetActive(false);
            }
        }
    }

    private void GetHighScore()
    {
        if (!string.IsNullOrEmpty(MainManager.Instance.highScorePlayerName)) 
        {
            highScoreText.SetText("High Score: " +  MainManager.Instance.highScorePlayerName + " - " + MainManager.Instance.highScore);
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
            lastScoreText.SetText(MainManager.Instance.currentPlayerName + "'s Last Score: " + MainManager.Instance.currentScore);

            inputNameTitleText.SetText("Still You?");
            //inputNamePlaceholderText.gameObject.SetActive(false);
            inputField.placeholder.gameObject.SetActive(false);
            inputField.text = MainManager.Instance.currentPlayerName;
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
        string inputName = inputNameText.text;
        bool hasName = string.IsNullOrEmpty(inputName);
        bool hasName2 = string.IsNullOrWhiteSpace(inputName);
        int hasName3 = inputName.Trim().Length;
        if (String.IsNullOrEmpty(inputName) || inputName.Trim().Length <= 1)
        {
            warningText.gameObject.SetActive(true);
        }
        else
        {
            MainManager.Instance.currentPlayerName = inputName;
            SceneManager.LoadScene((int)MyGameScenes.Game);
        }
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
