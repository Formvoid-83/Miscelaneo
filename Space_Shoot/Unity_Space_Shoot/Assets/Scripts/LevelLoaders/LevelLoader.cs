using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class LevelLoader: MonoBehaviour
{
    
    public void LoadGame(){
        SceneManager.LoadScene("Game_Shoot");
    }
    public void Quit(){
        Application.Quit();
    }
}
