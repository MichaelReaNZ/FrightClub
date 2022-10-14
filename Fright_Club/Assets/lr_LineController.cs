using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lr_LineController : MonoBehaviour
{
    private LineRenderer lr;
    private Transform[] pointPositions;
    
    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }
    
    public void SetUpLine(Transform[] points)
    {
       lr.positionCount = points.Length;
       pointPositions = points;
    }
    
    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < pointPositions.Length; i++)
        {
            lr.SetPosition(i, pointPositions[i].position);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }


}
