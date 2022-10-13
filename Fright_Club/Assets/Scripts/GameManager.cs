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
        VictoryText.gameObject.SetActive(false);
        GameOverText.gameObject.SetActive(false);
        RestartBtn.gameObject.SetActive(false);
        GameIsActive = true;
        Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen, 60);
    }

    // Update is called once per frame
    void Update()
    {
        CollectableCount = GameObject.FindGameObjectsWithTag("Collectable").Length;
        PlayerHealth = GameObject.Find("Player").GetComponent<PlayerMovement>().PlayerHealth; //Will need to be changed if we add another script for player

        if (CollectableCount == 0) // Victory Condition
        {
            GameIsActive = false;
            VictoryText.gameObject.SetActive(true);
            RestartBtn.gameObject.SetActive(true);
        }
        else 
        {
            
        }

        if (PlayerHealth == 0) // Defeat Condition
        {
            GameIsActive = false;
            GameOverText.gameObject.SetActive(true);
            RestartBtn.gameObject.SetActive(true);
        }
        else
        {
            
        }
    }

    void GameRestart()
    { 

    }
}
