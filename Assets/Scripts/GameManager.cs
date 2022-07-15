using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text coinsText;
    public Text gameOverText;
    public Text winText;
    public Button restartButton;

    private int coins;

    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        coins = 0;
        gameOver = false;
    }

    // Add coins score on pickup
    public void PickupCoins(int coinsToAdd)
    {
        coins += coinsToAdd; 
        coinsText.text = "Coins: " + coins;
    }

    // Show game over screen
    public void GameOverScreen()
    {
        gameOver = true;

        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    // Show Win screen
    public void WinScreen()
    {
        gameOver = true;

        winText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    // Restart current scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
