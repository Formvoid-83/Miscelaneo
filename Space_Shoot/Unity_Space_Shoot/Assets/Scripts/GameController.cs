using UnityEngine;

using TMPro;
using Microsoft.Unity.VisualStudio.Editor; // For TextMeshPro

public class GameController : MonoBehaviour
{
    private GameObject gameOverScreen;
    private bool isGameOver = false;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private GameObject shieldBar;
    private int theScore;
    private bool shieldActive;

    void Start()
    {
        gameOverScreen = GameObject.FindWithTag("GameOver");
        shieldBar.SetActive(false);
        shieldActive = false;
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        finalScoreText.text = "Final Score: "+ theScore;
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
    public void updateScore(int score){
        theScore += score;
        scoreText.text = "Score: " + theScore;
    }
}
