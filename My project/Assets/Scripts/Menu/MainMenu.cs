using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

public void playGame(){
    SceneManager.LoadScene(1);
}
public void exitGame(){
    Application.Quit();
}


}
