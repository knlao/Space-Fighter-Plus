using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Text recentScoreText;
    public Text highestScoreText;

    public void Start()
    {
        const string zero = "000000000000";
        recentScoreText.text = zero.Substring(0, zero.Length - PlayerPrefs.GetInt("recentScore").ToString().Length) +
                               PlayerPrefs.GetInt("recentScore");
        highestScoreText.text = zero.Substring(0, zero.Length - PlayerPrefs.GetInt("highestScore").ToString().Length) +
                               PlayerPrefs.GetInt("highestScore");
    }

    public void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}