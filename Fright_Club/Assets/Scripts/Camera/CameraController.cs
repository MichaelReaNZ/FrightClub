using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform playerTransform;
    public Vector3 minBounds;
    public Vector3 maxBounds;
    
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        minBounds.z = -1;
        maxBounds.z = -1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z - 1 );
        //if ( transform.position.x < minBounds.x ) transform.position.x = minBounds.x;
        //if ( transform.position.y < minBounds.y ) transform.position.x = minBounds.y;
        //if ( transform.position.x > maxBounds.x ) transform.position.x = maxBounds.x;
        //if ( transform.position.y > maxBounds.y ) transform.position.x = maxBounds.y;
    }
}
