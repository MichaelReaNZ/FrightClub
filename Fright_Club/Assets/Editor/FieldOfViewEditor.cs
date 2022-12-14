using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LanturnLightFieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
       LanturnLightFieldOfView fov = (LanturnLightFieldOfView)target;
         Handles.color = Color.white;
         //Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.up, 360, fov.viewRadius);
            Vector3 viewAngleA = fov.DirectionFromAngle(-fov.viewAngle / 2, false);
            Vector3 viewAngleB = fov.DirectionFromAngle(fov.viewAngle / 2, false);
            
            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRadius);
            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRadius);
            
            Handles.color = Color.red;
            foreach (Transform visibleTarget in fov.monstersWithinFlashlight)
            {
                Handles.DrawLine(fov.transform.position, visibleTarget.position);
                
                //draw line visible in game
                Debug.DrawLine(fov.transform.position, visibleTarget.position, Color.red);
            }
    }

}
