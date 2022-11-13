using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Abberant : Monster

   
{
    
     

    //The AI for a monster if it is hidden
    protected override void HiddenBehaviour()
    {
        
        gameObject.tag = startingtag;
        gameObject.transform.localScale = Originalscale;

        if ( (Vector2)this.transform.position != StartingPosition )
            ReturnToStart();
    }

    //The AI for a monster if it is in the light of the lantern
    protected override void IlluminatedBehaviour()
    {
        gameObject.tag = newtag;
        gameObject.transform.localScale = newscale;

        if ( DetectPlayer() && !AwayFromStart() )
        {
            Attack();
        }
        else
        { 
            ReturnToStart();
        }
    }
}
