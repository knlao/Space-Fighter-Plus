using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public Text healthText;
    public GameObject pausePanel;
    public GameObject gameOverUI;
    public Text finalScoreText;
    public Text highestScoreText;

    public void UpdateScoreText(int newScore)
    {
        const string zero = "000000000000";
        scoreText.text = zero.Substring(0, zero.Length - newScore.ToString().Length) + newScore;
    }

    public void UpdateSubScoreText(int newScore)
    {
        FindObjectOfType<SubScoreSlider>().UpdateSlider(newScore > 20 ? 20 : newScore);
    }
    
    public void UpdateHealthText(int newHealth)
    {
        healthText.text = "Health x" + Mathf.Clamp(newHealth, 0, 3);
    }

    public void Resume()
    {
        FindObjectOfType<GameManager>().Resume();
    }

    public void Restart()
    {
        FindObjectOfType<GameManager>().Restart();
    }

    public void Quit()
    {
        FindObjectOfType<GameManager>().Quit();
    }

    public void GameOver()
    {
        TogglePanel(false);
        gameOverUI.SetActive(true);
    }

    public void TogglePanel(bool on)
    {
        pausePanel.SetActive(on);
    }
}
