using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class LogicScript : MonoBehaviour
{
    public int playerScore = 0;
    public Text scoreText;
    public GameObject gameOverScreen;
    public GameObject menuPanel;
    public GameObject chim;
    public GameObject pipeSpawner;
    public static bool showMenu = true;
    public bool gameIsPlaying = false;

    public GameObject pauseScreen;
    public Sprite iconPause, iconPlay;
    public Button buttonPlayPause;

    public Button buttonHome;

    public Collider2D chimCollider;
    public Rigidbody2D chimBody;
    public ChimCode chimScript;

    public AudioClip deathSound;
    public AudioClip scoreSound;

    public TMP_Text bestScoreText;
    public TMP_Text newBestText;

    IEnumerator Shake()
    {
        Vector3 pos = Camera.main.transform.position;

        for (int i = 0; i < 8; i++)
        {
            Camera.main.transform.position =
                pos + (Vector3)Random.insideUnitCircle * 0.1f;

            yield return new WaitForSeconds(0.05f);
        }

        Camera.main.transform.position = pos;
    }

    private void Start()
    {
        if (showMenu == true)
        {
            gameIsPlaying = false;
            buttonPlayPause.gameObject.SetActive(false);
            chim.SetActive(false);
            pipeSpawner.SetActive(false);
            menuPanel.SetActive(true);
            scoreText.enabled = false;

            int bestScore = PlayerPrefs.GetInt("BestScore", 0);
            bestScoreText.text = "Best: " + bestScore;
            bestScoreText.enabled = true;
        }
        else
        {
            bestScoreText.enabled = false;
            startGame();
        }
    }

    private void pauseGame()
    {
        gameIsPlaying = false;
        pauseScreen.SetActive(true);
        buttonPlayPause.image.sprite = iconPlay;
        buttonHome.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (gameIsPlaying == true)
            {
                pauseGame();
            }
            else if(buttonHome.gameObject.activeSelf == true)
            {
                toMenu();
            }
        }
    }

    public void addScore(int scoreToAdd)
    {
        AudioSource.PlayClipAtPoint(
        scoreSound,
        Camera.main.transform.position
        );
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
    }

    public void restartGame()
    {
        Time.timeScale = 1f;
        showMenu = false;
        gameIsPlaying = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void startGame()
    {
        gameIsPlaying = true;
        buttonPlayPause.gameObject.SetActive(true);
        scoreText.enabled = true;
        menuPanel.SetActive(false);
        chim.SetActive(true);
        pipeSpawner.SetActive(true);
    }

    private IEnumerator NewBestEffect()
    {
        newBestText.gameObject.SetActive(true);

        float duration = 0.4f;
        float elapsed = 0f;
        Vector3 originalPos = newBestText.transform.localPosition;
        Color flashColor = new Color(1f, 0.9f, 0f); // yellow
        Color originalColor = newBestText.color;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            // shake: random offset that calms down over time
            float intensity = (1f - t) * 8f;
            newBestText.transform.localPosition = originalPos + (Vector3)Random.insideUnitCircle * intensity;

            // flash: yellow -> original
            newBestText.color = Color.Lerp(flashColor, originalColor, t);

            elapsed += Time.unscaledDeltaTime; // unscaled in case Time.timeScale is 0
            yield return null;
        }

        // reset to clean state
        newBestText.transform.localPosition = originalPos;
        newBestText.color = originalColor;
    }

    public void gameOver()
    {
        gameIsPlaying = false;
        gameOverScreen.SetActive(true);
        buttonPlayPause.gameObject.SetActive(false);
        chimCollider.enabled = false;
        chimBody.linearVelocity = Vector2.up * 15f;
        chimScript.birdIsAlive = false;

        AudioSource.PlayClipAtPoint(
        deathSound,
        Camera.main.transform.position
        );

        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        if (playerScore > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", playerScore);
            PlayerPrefs.Save();
            StartCoroutine(NewBestEffect());
        }

        StartCoroutine(Shake());
    }

    public void pressPlayPause()
    {
        if(buttonPlayPause.image.sprite == iconPause)
        {
            pauseGame();
        }
        else
        {
            gameIsPlaying = true;
            pauseScreen.SetActive(false);
            buttonPlayPause.image.sprite = iconPause;
            buttonHome.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void toMenu()
    {
        showMenu = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
