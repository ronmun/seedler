using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MenuButtons : MonoBehaviour
{
    public int firstLevelIndex;
    public int creditsIndex;
    public int mainMenuIndex;

    public void Play(){ 
        SceneManager.LoadScene(firstLevelIndex);
    }
    public void Credits(){ 
        SceneManager.LoadScene(creditsIndex);
    }
    public void Exit(){ 
        Application.Quit();
    }

    public void MainMenu(){
        SceneManager.LoadScene(mainMenuIndex);
    }
}
