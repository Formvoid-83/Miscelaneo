using UnityEngine;

using TMPro; // For TextMeshPro

public class GameController : MonoBehaviour
{
    private GameObject gameOverScreen;
    private bool isGameOver = false;

    void Start()
    {
        gameOverScreen = GameObject.FindWithTag("GameOver");
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
    }

    void Update()
    {

        if (isGameOver && Input.anyKeyDown)
        {
            RestartGame();
        }
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}
