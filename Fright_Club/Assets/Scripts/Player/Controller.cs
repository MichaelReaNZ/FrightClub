using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    
    Rigidbody2D rigidbody;
    Camera viewCamera;
    Vector2 velocity;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        viewCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
       // Vector3 mousePos = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));//, viewCamera.transform.position.y));
        
        //look in direction of mouse
      //  transform.LookAt(mousePos + Vector3.forward * viewCamera.transform.position.z);
        
        //transform.LookAt(mousePos + Vector3.up * transform.position.z);
        
        //velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * moveSpeed;
        
        
        
        velocity.x = Input.GetAxisRaw("Horizontal");
        velocity.y = Input.GetAxisRaw("Vertical");
        
        velocity = velocity.normalized * moveSpeed;
        
        //rotate on z based on the mouse position
        //transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90);
        
       
           
       
    }
    
    void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
    }
}
