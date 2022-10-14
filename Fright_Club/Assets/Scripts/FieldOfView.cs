using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;
    
    public List<Transform> visibleTargets = new List<Transform>();
    
    public float meshResolution;
    Mesh viewMesh;
    
    public MeshFilter viewMeshFilter;

    // Start is called before the first frame update
    void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
        
        StartCoroutine("FindTargetsWithDelay", .2f);
    }

    private void Update()
    {
        //mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //from
        Vector3 fromPoint = transform.position;
       
        //to 
        Vector3 toPoint = mousePos;
        
        Debug.DrawLine(fromPoint,toPoint,Color.red);
        
        //make the flash light look at direction of the mouse
        //viewMeshFilter.transform.LookAt(toPoint);
     
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                //If the light does not collide with an obstacle
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                    //change color of target
                    target.GetComponent<Renderer>().material.color = Color.red;
                    Debug.Log("Target in sight");
                }
            }
        }
    }
    
    // void DrawLine(Vector2 start, Vector2 end, Color color, float duration = 0.2f)
    // {
    //     GameObject myLine = new GameObject();
    //     myLine.transform.position = start;
    //     myLine.AddComponent<LineRenderer>();
    //     LineRenderer lr = myLine.GetComponent<LineRenderer>();
    //     lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
    //     lr.SetColors(color, color);
    //     lr.SetWidth(1000000.1f, 1000000.1f);
    //     lr.SetPosition(0, start);
    //     lr.SetPosition(1, end);
    //     GameObject.Destroy(myLine, duration);
    // }

    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            //rotate angle by 180
            // angle = angle + 180;
            // if (angle < 0) angle += 360;
            //
            //draw a red line in game to visualize the angle
           // DrawLine(transform.position, transform.position + DirectionFromAngle(angle, true) * viewRadius, Color.red);
           
           Debug.DrawLine(transform.position,transform.position + DirectionFromAngle(angle, true) * viewRadius,Color.yellow);
           
            ViewCastInfo newViewCast = ViewCast(angle);
            viewPoints.Add(newViewCast.point);
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];
        
        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
            
            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }
        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 direction = DirectionFromAngle(globalAngle, true);
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, direction, out hit, viewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + direction * viewRadius, viewRadius, globalAngle);
        }
    }
    
    public Vector3 DirectionFromAngle(float angleDeg, bool angleIsGlobal)
    {
        if (angleIsGlobal)
        {
            angleDeg += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleDeg * Mathf.Deg2Rad),
            Mathf.Cos(angleDeg * Mathf.Deg2Rad)); //, Mathf.Cos(angleDeg * Mathf.Deg2Rad));
    }
    
    //SetOrigin
    public void SetOrigin(Vector3 origin)
    {
        transform.position = origin;
    }

    // Update is called once per frame
    void LateUpdate()
    {
     DrawFieldOfView();   
     
     //Draw a line from left and corner to right hand corner
     Vector2 start = new Vector2(200,-70);
     Vector2 end = new Vector2(100,-50);
     //DrawLine(start,end, Color.yellow);
    }
    
    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }

}
