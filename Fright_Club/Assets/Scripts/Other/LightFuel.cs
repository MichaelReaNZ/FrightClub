using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFuel : MonoBehaviour
{
    [SerializeField] private LanturnLightFieldOfView lanturnLightFieldOfView;
    private AudioSource lightRefill;
    private AudioClip jeff;

    // Start is called before the first frame update
    void Start()
    {
        lightRefill = GetComponent<AudioSource>();
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
            if (!lightRefill.isPlaying)
                lightRefill.Play();
            StartCoroutine(destroyObject(0.6f));

            LanturnLightFieldOfView lanturnLightFieldOfViewComponent = objectColliding.gameObject.GetComponent<LanturnLightFieldOfView>();
            lanturnLightFieldOfViewComponent.ResetLightAngleAndLength();
        }
    }

    private IEnumerator destroyObject(float _time)
    {
        yield return new WaitForSeconds(_time);
        Destroy(this.gameObject);
    }
}
