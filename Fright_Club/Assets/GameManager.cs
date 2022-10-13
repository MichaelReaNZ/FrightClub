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
    private bool GameIsActive;
    public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI VictoryText;
    public Button RestartBtn;

    // Start is called before the first frame update
    void Start()
    {
        VictoryText.GameObject.SetActive(false);
        GameOverText.GameObject.SetActive(false);
        RestartBtn.GameObject.SetActive(false);
        GameIsActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        CollectableCount = GameObject.FindGameObjectsWithTag("Collectable").Length;
        PlayerHealth = GameObject.FindGameObjectsWithTag("Health").Length;

        if (CollectableCount == 0) // Victory Condition
        {
            GameIsActive = false;
            VictoryText.GameObject.SetActive(true);
            RestartBtn.GameObject.SetActive(true);
        }
        else 
        {
            
        }

        if (PlayerHealth == 0) // Defeat Condition
        {
            GameIsActive = false;
            GameOverText.GameObject.SetActive(true);
            RestartBtn.GameObject.SetActive(true);
        }
        else
        {
            
        }
    }

    void GameRestart()
    { 
    }
}
