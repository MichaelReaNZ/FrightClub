using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    private float fov;
    
    private float viewDistance;
    private float startingAngle;
    private Vector3 origin;
    
    //[SerializeField] private LayerMask layerMask;
    private  Mesh mesh;
    
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        fov = 90f;
        origin = Vector3.zero;
        startingAngle = 0f;
    }

    // Update is called once per frame
    void Update()
    { 
        int rayCount = 100;
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;
        viewDistance = 50;
        
        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vetexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
          // RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance,layerMask);
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance);
            if(raycastHit2D.collider == null)
            {
                //no hit then max distance
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                //hit object, lands on point it hits
                vertex = raycastHit2D.point;
            }
            
            vertices[vetexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vetexIndex - 1;
                triangles[triangleIndex + 2] = vetexIndex;
                
                triangleIndex += 3;
            }
     
            vetexIndex++;
            //unity goes anticlockwise...
            angle -= angleIncrease;
        }
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.bounds = new Bounds(origin, Vector3.one * 1000f);
    }
    
    public static Vector3 GetVectorFromAngle(float angle)
    {
        //angle = 0 to 360
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
    
    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }
    
    //Set from the player position
    public void SetOrigin(Vector3 origin)
    {
        transform.position = origin;
    }
    
    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = GetAngleFromVectorFloat(aimDirection) - fov / 2f;
    }
}
