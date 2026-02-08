
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Audio")]
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private MusicPlayer musicPlayer;

    [Header("UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;

    private int score = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

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
    }

    public void GameOver()
    {
        if (musicPlayer != null)
        {
            musicPlayer.StopMusic();
        }

        audioSource.PlayOneShot(gameOverSound);

        Time.timeScale = 0f;

        gameOverScoreText.text = "Score: " + score.ToString();

        gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;

        gameOverPanel.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
}
