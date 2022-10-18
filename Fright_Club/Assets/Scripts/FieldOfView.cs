using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Serialization;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;
    
    [FormerlySerializedAs("visibleTargets")] [HideInInspector]
    public List<Transform> visibleMonsters = new List<Transform>();
    
    public float meshResolution;
    Mesh _viewMesh;
    List<Vector3> viewPoints = new List<Vector3>();
    
    public MeshFilter viewMeshFilter;
    
    //public Monster monsterScript;

    // Start is called before the first frame update
    void Start()
    {
        _viewMesh = new Mesh();
        _viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = _viewMesh;
        
        //StartCoroutine(nameof(FindTargetsWithDelay), .2f);
       // FindVisibleTargets();
    }

    private void Update()
    {
        //mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //from
        Vector3 fromPoint = transform.position;
       
        //to 
        Vector3 toPoint = mousePos;
        
       Debug.DrawLine(fromPoint,toPoint,Color.green);
       var angleOfMouseToPlayer = AngleBetweenTwoPoints(fromPoint,toPoint);
       //Debug.Log("Angle of mouse to player: " + angleOfMouseToPlayer);
        
       
       StartCoroutine(nameof(FindTargetsWithDelay), .2f);
     
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    GameObject[] FindGameObjectsInLayer(int layer)
    {
        var goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        var goList = new System.Collections.Generic.List<GameObject>();
        for (int i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer == layer)
            {
                goList.Add(goArray[i]);
            }
        }
        if (goList.Count == 0)
        {
            return null;
        }
        return goList.ToArray();
    }
    
    void FindVisibleTargets()
    {
        visibleMonsters.Clear();
        //Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        
        //get all objects from the targetMask layer
        //TODO: FIX This. This gets the parent monster too which we don't want
        GameObject[] monsterObjects = FindGameObjectsInLayer(7);

        //for each monster check in the beam of light is colliding
        
        //TODO: Test this more
        foreach (GameObject monsterObject in monsterObjects)
        {
            Vector3 dirToTarget = (monsterObject.transform.position - transform.position).normalized;

            var lightAngleStart = AngleBetweenPlayerToMouse() - (viewAngle / 2);
            var lightAngleEnd = AngleBetweenPlayerToMouse() + (viewAngle / 2);

            Vector3 lightAngleStartVector = DirectionFromAngle(lightAngleStart, false);
            Vector3 lightAngleEndVector = DirectionFromAngle(lightAngleEnd, false);

            Debug.DrawLine(transform.position, transform.position + lightAngleStartVector * viewRadius, Color.red);
            Debug.DrawLine(transform.position, transform.position + lightAngleEndVector * viewRadius, Color.red);

            // Vector3 viewAngleA = DirectionFromAngle(-viewAngle / 2, false);
            // Vector3 viewAngleB = DirectionFromAngle(viewAngle / 2, false);
            //
            // Debug.DrawLine(transform.position, transform.position + viewAngleA * viewRadius,Color.red);
            // Debug.DrawLine(transform.position, transform.position + viewAngleB * viewRadius,Color.red);
            Debug.DrawLine(transform.position, monsterObject.transform.position, Color.magenta);


            //monster Is Within Range Of FlashLight Radius
            if (Vector3.Distance(transform.position, monsterObject.transform.position) < viewRadius)
            {
                Monster monsterScript = monsterObject.GetComponent<Monster>();
                if (monsterScript == null) continue;
                // monster.GetComponentsInChildren<SpriteRenderer>()[0].material.color = Color.white;
                // Debug.Log("Monster is within radius of flashlight");
                //TODO: For performance also check if the monster is within the angle of the flashlight?

                //TODO: This should be a raycast check not a in mask check (Walls)
                if (ContainsPoint(viewPoints, monsterObject.transform.position))
                {
                    Debug.Log("Monster is within the flashlight.");
                    //change color of the monster
                    // monsterObject.GetComponentsInChildren<SpriteRenderer>()[0].material.color = Color.red;
                    monsterObject.GetComponent<Monster>().isIlluminated = true;
                    visibleMonsters.Add(monsterObject.transform);
                }
                else
                {
                    // monsterObject.GetComponentsInChildren<SpriteRenderer>()[0].material.color = Color.white;
                    monsterObject.GetComponent<Monster>().isIlluminated = false;
                }
            }
            //monster Is outside of range Of FlashLight Radius
            else
            {
                monsterObject.GetComponent<Monster>().isIlluminated = false;
                //monsterObject.GetComponentsInChildren<SpriteRenderer>()[0].material.color = Color.white;
            }
        }



        // for (int i = 0; i < targetsInViewRadius.Length; i++)
        // {
        //     Transform target = targetsInViewRadius[i].transform;
        //     Vector3 dirToTarget = (target.position - transform.position).normalized;
        //     if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
        //     {
        //         float dstToTarget = Vector3.Distance(transform.position, target.position);
        //
        //         //If the light does not collide with an obstacle
        //         if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
        //         {
        //             visibleTargets.Add(target);
        //             //change color of target
        //             target.GetComponent<Renderer>().material.color = Color.red;
        //             Debug.Log("Target in sight");
        //         }
        //     }
        // }
    }
    


    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float angleSegmentSize = viewAngle / stepCount;
       // List<Vector3> viewPoints = new List<Vector3>();
       viewPoints.Clear();
        
        for (int i = 0; i <= stepCount; i++)
        {
            float angleOfLightSegment = transform.eulerAngles.y - viewAngle / 2 + angleSegmentSize * i;
            
            var angleBetweenPlayerToMouse = AngleBetweenPlayerToMouse();

            //make the flash light look at direction of the mouse
            //make sure the mouse is in the middle of the beam of light
            angleOfLightSegment = angleBetweenPlayerToMouse - (viewAngle / 2) + (angleSegmentSize * i);
            
            //Debug.Log("Angle of mouse to player: " + charToMouseDegrees);
            Debug.DrawLine(transform.position,transform.position + DirectionFromAngle(angleBetweenPlayerToMouse, true) * viewRadius,Color.yellow);

           //Debug.Log("Angle of light segment: " + angleOfLightSegment);
           //Debug.DrawLine(transform.position,transform.position + DirectionFromAngle(angleOfLightSegment, true) * viewRadius,Color.magenta);
            
            ViewCastInfo newViewCast = ViewCast(angleOfLightSegment);
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
        _viewMesh.Clear();
        _viewMesh.vertices = vertices;
        _viewMesh.triangles = triangles;
        _viewMesh.RecalculateNormals();
    }

    private float AngleBetweenPlayerToMouse()
    {
        //mouse position relative to a point
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        return AngleBetweenTwoPoints(transform.position, mousePos);

        // //get the vector representing the mouse's position relative to the point...
        // Vector2 relativeMousePos = mousePos - transform.position;
        // //use atan2 to get the angle; Atan2 returns radians
        // var charToMouseAngleRadians = Mathf.Atan2(relativeMousePos.y, relativeMousePos.x);
        // //convert to degrees
        // var charToMouseDegrees = charToMouseAngleRadians * Mathf.Rad2Deg;
        //
        // //It's offset by 90 degrees, so subtract 90
        // charToMouseDegrees -= 90;
        // //keep angles within the 0 to 360 range
        // if (charToMouseDegrees < 0)
        //     charToMouseDegrees += 360;
        //
        // //change from clockwise to counter clockwise
        // charToMouseDegrees = -charToMouseDegrees;
        // return charToMouseDegrees;
    }
    
    //Returns the angle between two points in degrees
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        
        Vector2 relativePos = b - a;
        //use atan2 to get the angle; Atan2 returns radians
        var angleRadians = Mathf.Atan2(relativePos.y, relativePos.x);
        //convert to degrees
        var angleDegrees = angleRadians * Mathf.Rad2Deg;

        //It's offset by 90 degrees, so subtract 90
        angleDegrees -= 90;
        //keep angles within the 0 to 360 range
        if (angleDegrees < 0)
            angleDegrees += 360;

        //change from clockwise to counter clockwise
        angleDegrees = -angleDegrees;
        return angleDegrees;
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

        return new Vector3(Mathf.Sin(angleDeg * Mathf.Deg2Rad), Mathf.Cos(angleDeg * Mathf.Deg2Rad)); //, Mathf.Cos(angleDeg * Mathf.Deg2Rad));
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
    
    float AddAngles(float a, float b)
    {
        float result = a + b;
        if (result > 360)
        {
            result -= 360;
        }
        else if (result < 0)
        {
            result += 360;
        }
        return result;
    }
    
    
    private static bool ContainsPoint(List<Vector3> polyPoints, Vector2 p)
    {
        // var j = polyPoints.Length - 1;
        var j = polyPoints.Count - 1;
        var inside = false;
        for (int i = 0; i < polyPoints.Count; j = i++)
        // for (int i = 0; i < polyPoints.Length; j = i++)
        {
            var pi = polyPoints[i];
            var pj = polyPoints[j];
            if (((pi.y <= p.y && p.y < pj.y) || (pj.y <= p.y && p.y < pi.y)) &&
                (p.x < (pj.x - pi.x) * (p.y - pi.y) / (pj.y - pi.y) + pi.x))
                inside = !inside;
        }
        return inside;
    }
}
