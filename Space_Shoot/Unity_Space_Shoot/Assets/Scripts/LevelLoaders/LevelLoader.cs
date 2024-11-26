using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class LevelLoader: MonoBehaviour
{
    
    public void LoadGame(){
        SceneManager.LoadScene(1);
    }
    public void Quit(){
        Application.Quit();
    }
}
