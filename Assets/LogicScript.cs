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
    public static bool gameIsPlaying = false;

    public GameObject pauseScreen;
    public Sprite iconPause, iconPlay;
    public Button buttonPlayPause;

    public Button buttonHome;

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
        }
        else
        {
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

    public void gameOver()
    {
        gameIsPlaying = false;
        gameOverScreen.SetActive(true);
        buttonPlayPause.gameObject.SetActive(false);
        Time.timeScale = 0f;
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
