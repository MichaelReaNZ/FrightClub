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
    public TextMeshProUGUI CollectablesLeftText;
    public static int CollectableCount = 3;
    public GameObject ShowCollectableCount;

    // Start is called before the first frame update
    void Start()
    {
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
        if ( objectColliding.gameObject.CompareTag("Player") )
        {
            CollectableCount -= 1;
            CollectablesLeftText.text = CollectableCount.ToString() + " left";
            collectablePickUp.Play();
            Destroy(this.gameObject);
        }
    }
}
