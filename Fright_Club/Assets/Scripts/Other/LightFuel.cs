using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFuel : MonoBehaviour
{
    [SerializeField] private FieldOfView _fieldOfView;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //Destroys this on collision with the player
    private void OnCollisionEnter2D( Collision2D objectColliding )
    {
        if ( objectColliding.gameObject.CompareTag("Player") )
        {
            Destroy(this.gameObject);
            
            FieldOfView fieldOfViewComponent = objectColliding.gameObject.GetComponent<FieldOfView>();
            fieldOfViewComponent.ResetLightAngleAndLength();
        }
    }
}
