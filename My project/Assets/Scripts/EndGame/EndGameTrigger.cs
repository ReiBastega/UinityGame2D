using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EndGameTrigger : MonoBehaviour
{
    private bool hasEnded = false;

    public GameObject endGame;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasEnded)
        {
            TimerController.instance.EndTimer();
            hasEnded = true;
            EndGame();
        }
    }

    private void EndGame()
    {
        if (endGame)
        {
            Time.timeScale = 0;
            endGame.SetActive(true);
    }

    }
}
