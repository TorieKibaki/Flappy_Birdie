using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI Display")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText; // NEW: High score during gameplay

    [Header("Audio")]
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private MusicPlayer musicPlayer;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;
    [SerializeField] private TextMeshProUGUI gameOverHighScoreText; // NEW: High score on death screen

    private int score = 0;
    private int highScore = 0; // NEW: To track the high score in memory

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // LOAD HIGH SCORE: This pulls the saved value from the device
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreUI();

        if (scoreText != null)
        {
            scoreText.text = "Score: 0";
        }
    }

    public void AddScore(int amount)
    {
        score += amount;

        if (scoreText != null)
        {
            audioSource.PlayOneShot(coinSound);
            scoreText.text = "Score: " + score.ToString();
        }

        // Check if we just broke the record
        if (score > highScore)
        {
            highScore = score;
            UpdateHighScoreUI();

            // Optional: Save immediately so it's not lost if the game crashes
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    private void UpdateHighScoreUI()
    {
        if (highScoreText != null)
            highScoreText.text = "Best: " + highScore.ToString();
    }

    public void GameOver()
    {
        if (musicPlayer != null)
        {
            musicPlayer.StopMusic();
        }

        audioSource.PlayOneShot(gameOverSound);
        Time.timeScale = 0f;

        // Final Save: Ensure the high score is written to disk
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();

        // Update Game Over Panel
        gameOverScoreText.text = "Score: " + score.ToString();

        if (gameOverHighScoreText != null)
            gameOverHighScoreText.text = "Best: " + highScore.ToString();

        gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    // BONUS: Add a way to reset the high score for testing
    [ContextMenu("Reset High Score")]
    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        highScore = 0;
        UpdateHighScoreUI();
    }
}