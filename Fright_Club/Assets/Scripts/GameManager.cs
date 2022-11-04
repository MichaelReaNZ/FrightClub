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
    private Vector3 PlayerTransform;
    public bool GameIsActive;

    private AudioSource backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        GameIsActive = true;

        backgroundMusic = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if( backgroundMusic != null && !backgroundMusic.isPlaying )
        {
            backgroundMusic.Play();
        }

        CollectableCount = GameObject.FindGameObjectsWithTag("Collectable").Length;

        PlayerHealth = GameObject.Find("Player").GetComponent<PlayerMovement>().PlayerHealth; //Will need to be changed if we add another script for player

        // Victory / Defeat Conditions
        if (  CollectableCount <= 0 ) // Victory Condition
        {
            SceneManager.LoadScene("GameComplete");
        }
        else if (PlayerHealth <= 0) // Defeat Condition
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
