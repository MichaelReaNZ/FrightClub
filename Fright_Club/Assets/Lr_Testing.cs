using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lr_Testing : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private lr_LineController line;
    
    void Start()
    {
        line.SetUpLine(points);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
