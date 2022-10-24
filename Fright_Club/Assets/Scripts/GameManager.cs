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
    public bool GameIsActive;
    public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI VictoryText;
    public Button RestartBtn;

    private AudioSource backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        VictoryText.gameObject.SetActive(false);
        GameOverText.gameObject.SetActive(false);
        RestartBtn.gameObject.SetActive(false);
        GameIsActive = true;
        backgroundMusic = GetComponent<AudioSource>();
        //Screen.SetResolution(1820, 1080, FullScreenMode.ExclusiveFullScreen, 60);
    }

    // Update is called once per frame
    void Update()
    {
        if(backgroundMusic != null && !backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }

        CollectableCount = GameObject.FindGameObjectsWithTag("Collectable").Length;

        PlayerHealth = GameObject.Find("Player").GetComponent<PlayerMovement>().PlayerHealth; //Will need to be changed if we add another script for player

        if (  CollectableCount <= 0 && GameObject.Find("Player").GetComponent<PlayerMovement>().VictoryLocation ) // Victory Condition
        {
            SceneManager.LoadScene("GameComplete");
            /*
            GameIsActive = false;
            VictoryText.gameObject.SetActive(true);
            RestartBtn.gameObject.SetActive(true);
            */
        }
        else 
        {
            
        }

        if (PlayerHealth <= 0) // Defeat Condition
        {
            SceneManager.LoadScene("GameOver");
            /*
            GameIsActive = false;
            GameOverText.gameObject.SetActive(true);
            RestartBtn.gameObject.SetActive(true);
            */
        }
        else
        {
            
        }
    }

    void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
