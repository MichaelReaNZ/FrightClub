using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using static Collectable;

public class GameManager : MonoBehaviour
{
    public PlayableDirector fadeOut;

    // public int CollectableCount;
    public int _playerCourage;
    private Vector3 PlayerTransform;
    private AudioSource backgroundMusic;

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
    }

    public void checkRemainingBears()
    {
        CollectableCount = GameObject.FindGameObjectsWithTag("Collectable").Length;
    }

    void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
