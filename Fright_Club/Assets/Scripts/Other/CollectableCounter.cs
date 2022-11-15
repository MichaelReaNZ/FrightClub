using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;


public class CollectableCounter : MonoBehaviour
{
    public TextMeshProUGUI CollectablesLeftText;
    private int BearCount;

    // Start is called before the first frame update
    void Start()
    {
        BearCount = GameObject.FindGameObjectsWithTag("Collectable").Length;
        CollectablesLeftText.text = BearCount.ToString() + " left";
    }

    public void updateCounter()
    {
        BearCount = GameObject.FindGameObjectsWithTag("Collectable").Length - 1;
        if(BearCount > 1 )
            CollectablesLeftText.text = BearCount.ToString() + " left";
        else
        {
            CollectablesLeftText.text = "Head to your bed";
        }
    }
}
