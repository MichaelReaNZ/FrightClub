using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using static Collectable;
using static GameStartPrompt;

public class GameManager : MonoBehaviour
{
    public PlayableDirector fadeOut;

    // public int CollectableCount;
    public int _playerCourage;
    public GameObject bearCounter;
    private Vector3 PlayerTransform;
    private AudioSource backgroundMusic;
    private int CollectableCount;

    // Start is called before the first frame update
    void Start()
    {
        fadeOut = this.GetComponent<PlayableDirector>();
        backgroundMusic = GetComponent<AudioSource>();

        checkRemainingBears();
    }

    // Update is called once per frame
    void Update()
    {
        if( backgroundMusic != null && !backgroundMusic.isPlaying )
        {
            backgroundMusic.Play();
        }

        if (GameStartPromptIsActive == true)
        {
            bearCounter.SetActive(false);
        }
        else
        {
            bearCounter.SetActive(true);
        }
    }

    public void checkRemainingBears()
    {
        CollectableCount = GameObject.FindGameObjectsWithTag("Collectable").Length - 1;
    }

    void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
