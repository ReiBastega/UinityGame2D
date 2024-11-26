using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; 
    public int score = 0; 
    public TextMeshProUGUI scoreText; 


    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start() {
        TimerController.instance.BeginTimer();
    }


    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }


    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }
     
}
