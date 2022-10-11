using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float fov = 90f;
    public int rayCount;
    public float viewDistance;
    
    private  Mesh mesh;
    
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        // fov = 90f;
        // viewDistance = 50f;
        // rayCount = 50;
        
        Vector3 origin = Vector3.zero;
        float angle = 0f;
        float angleIncrease = fov / rayCount;
        
        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vetexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex = origin + GetVectorFromAngle(angle) * viewDistance;
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
    }
    
    public static Vector3 GetVectorFromAngle(float angle)
    {
        //angle = 0 to 360
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}
