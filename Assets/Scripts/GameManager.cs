using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    private int score;
    public Text scoreText;
    public Text highScoreText;
    public GameObject playButton;
    public GameObject gameover;
    public GameObject getready;
    public AudioSource music;
    public AudioSource gameoverSound;

    private void Awake()
    {
        gameover.SetActive(false);
        Application.targetFrameRate = 60;
        Pause();
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();
        highScoreText.enabled = false;
        playButton.SetActive(false);
        gameover.SetActive(false);
        getready.SetActive(false);
        music.Play();
        if (gameoverSound.isPlaying)
        {
            gameoverSound.Stop();
        }
        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();
        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        highScoreText.enabled = true;
        UpdateHighScore();
    }

    public void gameOver()
    {
        gameover.SetActive(true);
        playButton.SetActive(true);
        music.Stop();
        gameoverSound.Play();
        Pause();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    private void UpdateHighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore", 0);
    }

    public int GetScore()
    {
        return score;
    }
}
