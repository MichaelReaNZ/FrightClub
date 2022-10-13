using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Camera viewCamera;
    public float moveSpeed = 5f;
    
    public Rigidbody2D rigidbody;

    private Vector2 _movement;
    public int PlayerHealth;
    
   // [SerializeField] private FieldOfViewOld _fieldOfView;
    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth = 3;
        rigidbody = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        //user input
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
    }

   
    private void FixedUpdate()
    {
        // Movement
        rigidbody.MovePosition(rigidbody.position + _movement * (moveSpeed * Time.fixedDeltaTime));
        
      //  _fieldOfView.SetOrigin(transform.position);
    }
}
