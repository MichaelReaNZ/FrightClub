using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class VictoryCondition : MonoBehaviour
{
    private GameObject _player;
    private PlayableDirector gameVictory;

    void Start()
    {
        _player = GameObject.Find("Player");
        gameVictory = GameObject.Find("GameVictory").GetComponent<PlayableDirector>();
    }

    void OnCollisionEnter2D(Collision2D objectColliding)
    {
        if (objectColliding.gameObject.CompareTag("Player"))
        {
            if (GameObject.FindGameObjectsWithTag("Collectable").Length <= 0)
            {
                _player.GetComponent<PlayerMovement>().printSpeech("Goodnight, my bears...");
                StartCoroutine(showVictoryCutscene(2f));
                StartCoroutine(displayVictory(6f));
            }
        }
    }

    private IEnumerator showVictoryCutscene(float _time)
    {
        yield return new WaitForSeconds(_time);
        gameVictory.Play();
    }

    private IEnumerator displayVictory(float _time)
    {
        yield return new WaitForSeconds(_time);
        SceneManager.LoadScene("GameComplete");
    }
}
