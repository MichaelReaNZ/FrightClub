using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class VictoryCondition : MonoBehaviour
{
    private GameObject _player;
    private PlayableDirector fadeToBlack;

    void Start()
    {
        _player = GameObject.Find("Player");
        fadeToBlack = GameObject.Find("GameManager").GetComponent<GameManager>().fadeOut;
    }

    void OnCollisionEnter2D(Collision2D objectColliding)
    {
        if (objectColliding.gameObject.CompareTag("Player"))
        {
            if (GameObject.FindGameObjectsWithTag("Collectable").Length <= 0)
            {
                _player.GetComponent<PlayerMovement>().printSpeech("Goodnight, my bears...");
                fadeToBlack.Play();
                StartCoroutine(displayVictory(2f));
            }
        }
    }

    private IEnumerator displayVictory(float _time)
    {
        yield return new WaitForSeconds(_time);
        SceneManager.LoadScene("GameComplete");
    }
}
