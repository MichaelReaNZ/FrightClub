using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFuel : MonoBehaviour
{
    [SerializeField] private LanturnLightFieldOfView lanturnLightFieldOfView;
    
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
            
            LanturnLightFieldOfView lanturnLightFieldOfViewComponent = objectColliding.gameObject.GetComponent<LanturnLightFieldOfView>();
            lanturnLightFieldOfViewComponent.ResetLightAngleAndLength();
        }
    }
}
