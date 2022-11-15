using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static GameManager;
using static GameStartPrompt;


public class Collectable : MonoBehaviour
{
    private AudioSource collectablePickUp;
    // public string CollectablesLeftText;
    public int CollectableCount;
    public GameObject ShowCollectableCount;
    public TextMeshProUGUI CollectablesLeftText;
    private bool isCollected;

    // Start is called before the first frame update
    void Start()
    {
        isCollected = false;

        CollectableCount = GameObject.FindGameObjectsWithTag("Collectable").Length;
        collectablePickUp = GetComponent<AudioSource>();
        CollectablesLeftText.text = CollectableCount.ToString() + " left";
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStartPromptIsActive == true)
        {
            ShowCollectableCount.SetActive(false);
        }
       else
        {
            ShowCollectableCount.SetActive(true);
        }
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
                CollectablesLeftText.text = (GameObject.FindGameObjectsWithTag("Collectable").Length - 1).ToString() + " left";
                _player.GetComponent<PlayerMovement>().printSpeech("A bear! " + bearsLeft.ToString() + " to get to safety!");
            }
            else
            {
                _player.GetComponent<PlayerMovement>().printSpeech(_player.GetComponent<PlayerMovement>().endText);
                CollectablesLeftText.text = "Head to your bed";
            }
        }
    }

    private IEnumerator destroyObject( float _time )
    {
        yield return new WaitForSeconds(_time);
        Destroy(this.gameObject);
    }
}
