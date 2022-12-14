using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Serialization;

public class LanturnLightFieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)] public float viewAngle;
    private float originalViewAngle;
    private float originalViewRadius;

   // public LayerMask targetMask;
    public LayerMask obstacleMask;

    [FormerlySerializedAs("visibleTargets")] [HideInInspector]
    public List<Transform> monstersWithinFlashlight = new List<Transform>();

    public float meshResolution;
    Mesh _viewMesh;
    private List<Vector3> _viewPoints = new List<Vector3>();
    public MeshFilter viewMeshFilter;
    
    public bool dimViewAngle = false;
    public bool dimViewRadius = false;
    public bool dimBrightness = false;
    private float _brightness = 0f;
    public int dimmingSpeed = 10;
    public bool limitLightMouseMovementByPlayerDirection = true;
    
    private float _currentLightAnglePercentage;
    
    public AudioClip heartBeatSound;
    public AudioSource heartBeatSoundSource;

    // Start is called before the first frame update
    void Start()
    {
        _viewMesh = new Mesh();
        _viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = _viewMesh;
        
        originalViewAngle = viewAngle;
        originalViewRadius = viewRadius;

        StartCoroutine(nameof(FindTargetsWithDelay), .2f);
        StartCoroutine(nameof(ReduceLightAngleAndLength));
        
        heartBeatSoundSource.Play();
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
        var gameObjectArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        var gameObjectList = new List<GameObject>();
        if (gameObjectArray != null)
            foreach (var t in gameObjectArray)
            {
                if (t.layer == layer)
                {
                    gameObjectList.Add(t);
                }
            }

        if (gameObjectList.Count == 0)
        {
            return null;
        }

        return gameObjectList.ToArray();
    }

    void FindVisibleTargets()
    {
        monstersWithinFlashlight.Clear();
        GameObject[] monsterObjects = FindGameObjectsInLayer(7);

        //for each monster check in the beam of light is colliding
        for (var index = 0; index < monsterObjects.Length; index++)
        {
            var monsterObject = monsterObjects[index];
            Monster monsterComponent = monsterObject.GetComponent<Monster>();
            bool illuminateMonster = false;

            var playerPosition = transform.position;
            var monsterPosition = monsterObject.transform.position;

            var lightAngleStart = AngleBetweenPlayerAndMouse() - (viewAngle / 2);
            var lightAngleEnd = AngleBetweenPlayerAndMouse() + (viewAngle / 2);

            #region Debugging lines

            Vector3 lightAngleStartVector = DirectionFromAngle(lightAngleStart, false);
            Vector3 lightAngleEndVector = DirectionFromAngle(lightAngleEnd, false);

            Debug.DrawLine(playerPosition, playerPosition + lightAngleStartVector * viewRadius, Color.red);
            Debug.DrawLine(playerPosition, playerPosition + lightAngleEndVector * viewRadius, Color.red);
            Debug.DrawLine(playerPosition, monsterPosition, Color.magenta);

            #endregion

            //monster Is Within Range Of FlashLight Radius
            if (Vector3.Distance(transform.position, monsterObject.transform.position) < viewRadius)
            {
                // Debug.Log("Monster is within radius of flashlight");
                Monster monsterScript = monsterObject.GetComponent<Monster>();
                if (monsterScript == null) continue;

                float angleBetweenPlayerAndMonster = AngleBetweenTwoPoints(playerPosition, monsterPosition);
                //  Debug.Log("Angle between player and monster: " + angleBetweenPlayerAndMonster);

                //is Monster Within Light
                if (angleBetweenPlayerAndMonster >= lightAngleStart && angleBetweenPlayerAndMonster <= lightAngleEnd)
                {
                    Vector3 dirToTarget = (monsterPosition - playerPosition).normalized;
                    float dstToTarget = Vector3.Distance(playerPosition, monsterPosition);

                    //If the light does not collide with an obstacle then it's hit a monster
                    if (!Physics.Raycast(playerPosition, dirToTarget, dstToTarget, obstacleMask))
                    {
                        // Debug.Log("Monster is within the flashlight.");
                        illuminateMonster = true;
                        monstersWithinFlashlight.Add(monsterObject.transform);
                    }
                }
            }

            // monsterObject.GetComponentsInChildren<SpriteRenderer>()[0].material.color = illuminateMonster ? Color.red : Color.white;
            if (monsterComponent != null) monsterComponent.isIlluminated = illuminateMonster;
        }
    }


    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float angleSegmentSize = viewAngle / stepCount;
        _viewPoints.Clear();

        for (int i = 0; i <= stepCount; i++)
        {
            // float angleOfLightSegment = transform.eulerAngles.y - viewAngle / 2 + angleSegmentSize * i;

            var angleBetweenPlayerToMouse = AngleBetweenPlayerAndMouse();

            //make the flash light look at direction of the mouse
            //make sure the mouse is in the middle of the beam of light
            float angleOfLightSegment = angleBetweenPlayerToMouse - (viewAngle / 2) + (angleSegmentSize * i);

            //Debug.Log("Angle of mouse to player: " + charToMouseDegrees);
            Debug.DrawLine(transform.position, transform.position + DirectionFromAngle(angleBetweenPlayerToMouse, true) * viewRadius, Color.yellow);

            //Debug.Log("Angle of light segment: " + angleOfLightSegment);
            //Debug.DrawLine(transform.position,transform.position + DirectionFromAngle(angleOfLightSegment, true) * viewRadius,Color.magenta);

            ViewCastInfo newViewCast = ViewCast(angleOfLightSegment);
            _viewPoints.Add(newViewCast.point);
        }

        int vertexCount = _viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];
        vertices[0] = Vector3.zero;

        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(_viewPoints[i]);

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

    private float AngleBetweenPlayerAndMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angleBetweenPlayerAndMouse = AngleBetweenTwoPoints(transform.position, mousePos);

        if (limitLightMouseMovementByPlayerDirection)
        {
            //get direction of player
            PlayerMovement.PlayerDirection currentDirection = transform.GetComponent<PlayerMovement>().currentDirection;

            //if facing left
            if (currentDirection == PlayerMovement.PlayerDirection.Left)
            {
                //if mouse is to the right of the player
                if (mousePos.x > transform.position.x)
                {
                    //flip the angle
                    angleBetweenPlayerAndMouse = 360 - angleBetweenPlayerAndMouse;
                }
            }
            //if facing right
            else if (currentDirection == PlayerMovement.PlayerDirection.Right)
            {
                //if mouse is to the left of the player
                if (mousePos.x < transform.position.x)
                {
                    //flip the angle
                    angleBetweenPlayerAndMouse = 360 - angleBetweenPlayerAndMouse;
                }
            }
            //if facing up
            else if (currentDirection == PlayerMovement.PlayerDirection.Up)
            {
                //if mouse is below the player
                if (mousePos.y < transform.position.y)
                {
                    //reverse the angle
                    angleBetweenPlayerAndMouse = 180 - angleBetweenPlayerAndMouse;
                }
            }
            //if facing down
            else if (currentDirection == PlayerMovement.PlayerDirection.Down)
            {
                //if mouse is above the player
                if (mousePos.y > transform.position.y)
                {
                    //reverse the angle
                    angleBetweenPlayerAndMouse = 180 - angleBetweenPlayerAndMouse;
                }
            }
        }

        return angleBetweenPlayerAndMouse;
    }

    //Returns the angle between two points in degrees
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        Vector2 relativePosition = b - a;
        //atan2 to get the angle but Atan2 returns radians
        var angleRadians = Mathf.Atan2(relativePosition.y, relativePosition.x);
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

    public ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 direction = DirectionFromAngle(globalAngle, true);
        var rayCastResult = Physics2D.Raycast(transform.position, direction, viewRadius, obstacleMask);

        bool isProp = false;
        if(rayCastResult.collider != null)
        {
            //get the object that was hit
            var hitObject = rayCastResult.collider.gameObject;
            if(hitObject != null)
            {
                //if the object is a prop then shine the light through it
                var hitLayer = hitObject.layer;
                isProp = hitLayer == 6;
            }
        }

        //if the object is in the prop layer then shine the light through it
        if(isProp)
        {
            return new ViewCastInfo(false, transform.position + direction * viewRadius, viewRadius, globalAngle);
        }
        //if the raycast hit something
        if (rayCastResult.distance > 0)
        {
            return new ViewCastInfo(true, rayCastResult.point, rayCastResult.distance, globalAngle);
        }

        return new ViewCastInfo(false, transform.position + direction * viewRadius, viewRadius, globalAngle);
    }

    public Vector3 DirectionFromAngle(float angleDeg, bool angleIsGlobal)
    {
        if (angleIsGlobal)
        {
            angleDeg += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleDeg * Mathf.Deg2Rad), Mathf.Cos(angleDeg * Mathf.Deg2Rad));
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
    }

    public struct ViewCastInfo
    {
        private bool hit;
        public Vector3 point;
        private float dst;
        private float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }
    
    IEnumerator ReduceLightAngleAndLength()
    {
        while (dimmingSpeed > 0)
        {
            yield return new WaitForSeconds(0.05f);
            if (dimViewAngle && viewAngle > 0)
            {
                //reduce by 1%
                viewAngle -= viewAngle * (dimmingSpeed * 0.0001f);
                viewAngle -= 0.01f;
            }
                
            if (dimViewRadius && viewRadius > 0)
            {
                //reduce by 1%
                viewRadius -= viewRadius * (dimmingSpeed * 0.0001f);
                viewRadius -= 0.01f;
            }

            if (dimBrightness)
            {
                _brightness += dimmingSpeed * 0.00005f;
                var materials = viewMeshFilter.GetComponent<MeshRenderer>().materials;
                var material = materials[1];
                Color currentColor = material.color;
                material.color = new Color(currentColor.r, currentColor.g, currentColor.b, _brightness);
            }
            
            _currentLightAnglePercentage = viewAngle / originalViewAngle;
            
            //set volume of heartbeat sound based on the light angle
            if (heartBeatSoundSource != null)
            {
                //start quiet and get louder as the light angle gets smaller
                heartBeatSoundSource.volume = 1 -_currentLightAnglePercentage;
                
                //speed up the heartbeat as the light angle gets smaller
                heartBeatSoundSource.pitch = ((1 - _currentLightAnglePercentage) * 3) - 0.75f;
            }
        }
    }
    
    public void ResetLightAngleAndLength()
    {
        viewAngle = originalViewAngle;
        viewRadius = originalViewRadius;
        _brightness = 0;
    }
}