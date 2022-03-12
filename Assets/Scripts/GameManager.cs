using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float skyboxRotateSpeed = 1.2f;
    public int playerScore;
    public int playerSubScore;
    public int playerHealth = 3;

    public bool isGameOver;
    public bool isPaused;

    private void Update()
    {
        // Rotate skybox
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * skyboxRotateSpeed);

        // Key binds
        if (!isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    Time.timeScale = 1;
                    isPaused = false;
                    FindObjectOfType<UIManager>().TogglePanel(false);
                }
                else
                {
                    Time.timeScale = 0;
                    isPaused = true;
                    FindObjectOfType<UIManager>().TogglePanel(true);
                }
            }
        }

        if (isGameOver || isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void AddScore(int score)
    {
        playerScore += score;
        FindObjectOfType<UIManager>().UpdateScoreText(playerScore);
    }

    public void AddSubScore(int score)
    {
        playerSubScore += score;
        FindObjectOfType<UIManager>().UpdateSubScoreText(playerSubScore / 2000);
    }

    public void UpdateHealth(int newHealth)
    {
        playerHealth = newHealth;
        FindObjectOfType<UIManager>().UpdateHealthText(newHealth);
    }

    public void GameOver()
    {
        print("rest in pieces");
        isGameOver = true;

        FindObjectOfType<CameraController>().ShakeCam(true);
        
        const string zero = "000000000000";
        FindObjectOfType<UIManager>().finalScoreText.text =
            zero.Substring(0, zero.Length - playerScore.ToString().Length) + playerScore;

        if (playerScore >= PlayerPrefs.GetInt("highestScore"))
        {
            PlayerPrefs.SetInt("highestScore", playerScore);
        }
        
        FindObjectOfType<UIManager>().highestScoreText.text =
            zero.Substring(0, zero.Length - PlayerPrefs.GetInt("highestScore").ToString().Length) + PlayerPrefs.GetInt("highestScore");

        PlayerPrefs.SetInt("recentScore", playerScore);
        
        FindObjectOfType<UIManager>().GameOver();
        Time.timeScale = 0.3f;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        isPaused = false;
        FindObjectOfType<UIManager>().TogglePanel(false);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}