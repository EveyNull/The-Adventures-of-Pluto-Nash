using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraCollision : MonoBehaviour
{
    public float minDistance = 1.0f;
    public float maxDistance = 7.0f;


    Vector3 dollyDir;
    public float distance = 7.0f;


    void Awake()
    {
        dollyDir = Vector3.forward;
        distance = maxDistance;
    }


    void Update()
    {
        Vector3 desiredCameraPos = transform.parent.TransformPoint(dollyDir * distance);


        //layers to be ignored for the linecast collision
        const int ignoreLayer1 = 1 << 8;
        int layerMask = ignoreLayer1;
        layerMask = ~layerMask;

        RaycastHit hit;
        if (Physics.Linecast(transform.parent.position, desiredCameraPos, out hit))
        {
            distance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
    }
}
