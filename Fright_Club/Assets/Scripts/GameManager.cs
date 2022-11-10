using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static Collectable;

public class GameManager : MonoBehaviour
{
    public int PlayerHealth;
    private Vector3 PlayerTransform;
    private AudioSource backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if( backgroundMusic != null && !backgroundMusic.isPlaying )
        {
            backgroundMusic.Play();
        }

        PlayerHealth = GameObject.Find("Player").GetComponent<PlayerMovement>().PlayerHealth; //Will need to be changed if we add another script for player

        // Victory / Defeat Conditions
        if (CollectableCount <= 0 ) // Victory Condition
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
