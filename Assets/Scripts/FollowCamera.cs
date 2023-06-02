using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    static public GameObject POI;
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;
    [Header("Set Dynamically")]
    public float camZ;

    private void Awake()
    {
        camZ = transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 destination;
        if (POI == null) destination = Vector3.zero;
        else
        {
            destination = POI.transform.position;
            if(POI.tag == "Projectile")
            {
                if(POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    POI = null;
                    return;
                }
            }
        }

        destination.x = Mathf.Max(destination.x, minXY.x);
        destination.y = Mathf.Max(destination.y, minXY.y);
        destination = Vector3.Lerp(transform.position, destination, easing);
        destination.z = camZ;
        transform.position = destination;
        Camera.main.orthographicSize = destination.y + 10;
    }
}
