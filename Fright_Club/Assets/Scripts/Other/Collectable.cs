using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static GameManager;


public class Collectable : MonoBehaviour
{
    private AudioSource collectablePickUp;
    public GameObject CollectableCounter;
    private bool isCollected;

    // Start is called before the first frame update
    void Start()
    {
        isCollected = false;
        collectablePickUp = GetComponent<AudioSource>();
    }

    //Destroys this on collision with the player
    private void OnCollisionEnter2D( Collision2D objectColliding )
    {
        if ( objectColliding.gameObject.CompareTag("Player") && !isCollected )
        {
            isCollected = true;
            
            if(!collectablePickUp.isPlaying)
                collectablePickUp.Play();
            
            StartCoroutine(destroyObject(0.6f));

            GameObject _player = GameObject.Find("Player");
            if ( GameObject.FindGameObjectsWithTag("Collectable").Length >= 2 )
            {
                int bearsLeft = GameObject.FindGameObjectsWithTag("Collectable").Length - 1;
                _player.GetComponent<PlayerMovement>().printSpeech("'A bear! I feel so much safer!'");
            }
            else
            {
                _player.GetComponent<PlayerMovement>().printSpeech(_player.GetComponent<PlayerMovement>().endText);
            }
            CollectableCounter.GetComponent<CollectableCounter>().updateCounter();
        }
    }

    private IEnumerator destroyObject( float _time )
    {
        yield return new WaitForSeconds(_time);
        Destroy(this.gameObject);
    }
}
