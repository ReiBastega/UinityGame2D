using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseDifficulty : MonoBehaviour
{
    public enum DifficultyLevel
    {
        Easy,
        Normal,
        Hard
    }

    public static DifficultyLevel selectedDifficulty;

    public void SetDifficulty(string difficulty)
    {
        switch (difficulty.ToLower())
        {
            case "easy":
                selectedDifficulty = DifficultyLevel.Easy;
                break;

            case "normal":
                selectedDifficulty = DifficultyLevel.Normal;
                break;

            case "hard":
                selectedDifficulty = DifficultyLevel.Hard;
                break;

            default:
                Debug.LogError("Dificuldade desconhecida: " + difficulty);
                break;
        }
        SceneManager.LoadScene(2);
        PlayerPrefs.SetString("SelectedDifficulty", difficulty);
        PlayerPrefs.Save();
    }
    public static DifficultyLevel GetSavedDifficulty()
    {
        string savedDifficulty = PlayerPrefs.GetString("SelectedDifficulty", "normal").ToLower();

        return savedDifficulty switch
        {
            "easy" => DifficultyLevel.Easy,
            "hard" => DifficultyLevel.Hard,
            _ => DifficultyLevel.Normal, 
        };
    }
}
