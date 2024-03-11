using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] Text PlayerNameText;
    [SerializeField] Text HighscoreText;


    private void Start()
    {
        if (ScoreManager.Instance.CurrentHighscore.PlayerScore != 0)
        {
            HighscoreText.text = $"Best Score: {ScoreManager.Instance.CurrentHighscore.PlayerName}: {ScoreManager.Instance.CurrentHighscore.PlayerScore}";
        }
        else
        {
            HighscoreText.text = "Waiting for new high score";
        }
    }

    public void StartNew()
    {
        if (PlayerNameText.text == null || PlayerNameText.text == string.Empty)
        {
            Debug.Log("Warning: No player name provided");
        }
        else
        {
            Debug.Log($"Info: Player name was provided -> {PlayerNameText.text}");
            ScoreManager.Instance.PlayerName = PlayerNameText.text;
        }

        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        // MainManager.Instance.SaveBestScore();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
                Application.Quit(); // original code to quit Unity player
#endif
    }
}
