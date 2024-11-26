using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class EndGame : MonoBehaviour
{
    public string level;
    public TextMeshProUGUI bestScoreText; 
    public TextMeshProUGUI currentScoreText; 
    public TextMeshProUGUI finalTimeText; 
    public TextMeshProUGUI bestTimeText; // Exibe o menor tempo

    private void OnEnable()
    {
        int currentScore = ScoreManager.instance != null ? ScoreManager.instance.score : 0;
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

        // Lógica para o Best Score
        if (currentScore > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", currentScore);
            PlayerPrefs.Save();
        }

        if (bestScoreText != null)
        {
            bestScoreText.text = "Best score: " + PlayerPrefs.GetInt("BestScore").ToString();
        }
        if (currentScoreText != null)
        {
            currentScoreText.text = "Score: " + currentScore.ToString();
        }

        string currentTimeStr = TimerController.instance.timeCounter.text;
        float currentTime = TimerController.instance.elapsedTime; 

        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
        if (currentTime < bestTime)
        {
            PlayerPrefs.SetFloat("BestTime", currentTime);
            PlayerPrefs.Save();
        }

        if (finalTimeText != null)
        {
            finalTimeText.text = "Your Time: " + currentTimeStr;
        }

        if (bestTimeText != null)
        {
            TimeSpan bestTimeSpan = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("BestTime"));
            bestTimeText.text = "Best Time: " + bestTimeSpan.ToString("mm':'ss'.'ff");
        }
    }

public void Restart()
{
    ScoreManager.instance?.ResetScore();

    GameObject player = GameObject.FindWithTag("Player");
    if (player != null)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.ResetLife();
        }
    }

    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}


    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
