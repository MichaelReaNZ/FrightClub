using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Destroys this on collision with the player
    private void OnCollisionEnter( Collision objectColliding )
    {
        if (objectColliding.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
