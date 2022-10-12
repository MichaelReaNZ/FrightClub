using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private FieldOfViewOld _fieldOfView;
    
    // Start is called before the first frame update
    void Start()
    {
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //get the aim direction from the player to the mouse position
        Vector3 mousePos = Input.mousePosition;
        //normalize the mouse position
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        //get the direction from the player to the mouse position
        Vector3 aimDirection = (mousePos - transform.position).normalized;
        
        //offset aim direction by 90 degrees
        Vector3 aimDirectionOffset = Quaternion.Euler(0, 0, 90) * aimDirection;
        
        
        
       _fieldOfView.SetAimDirection(aimDirectionOffset);
        //_fieldOfView.SetOrigin(transform.position);

    }
}
