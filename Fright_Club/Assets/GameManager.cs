using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int CollectableCount;
    public int PlayerHealth;
    private bool gameIsActive;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI victoryText;
    public Button restartBtn;


    // Start is called before the first frame update
    void Start()
    {
        gameOverText = GameObject.FindGameObjectWithTag("GameOver");
        gameOverText.gameObject.SetActive(false);
        victoryText = GameObject.FindGameObjectWithTag("Victory");
        victoryText.gameObject.SetActive(false);
        restartBtn = GameObject.FindGameObjectWithTag("Restart");
        restartBtn.gameObject.SetActive(false);
        gameIsActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        CollectableCount = GameObject.FindGameObjectsWithTag("Collectable").Length;
        PlayerHealth = GameObject.FindGameObjectsWithTag("Health").Length;

        if (CollectableCount == 0) // Victory Condition
        {
            gameIsActive = false;
            victoryText.gameObject.SetActive(true);
            restartBtn.gameObject.SetActive(true);
        }
        else
        {

        }

        if (PlayerHealth == 0) // Defeat Condition
        {
            gameIsActive = false;
            gameOverText.gameObject.SetActive(true);
            restartBtn.gameObject.SetActive(true);
        }
        else
        {
            
        }
    }

    void GameRestart()
    { 
    }
}
