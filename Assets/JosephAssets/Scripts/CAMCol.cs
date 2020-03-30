using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAMCol : MonoBehaviour
{
    public float MinDis = 1.0f;
    public float MaxDis = 4.0f;
    public float SMOOTH = 10.0f;

    Vector3 DIR;
    public Vector3 DIRADJ;

    public float distance;
    // Start is called before the first frame update
    void Awake()
    {
        DIR = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
       
        Vector3 DesCamPos = transform.parent.TransformPoint(DIR * MaxDis);
        RaycastHit hit;

        if (Physics.Raycast( transform.parent.position, DesCamPos, out hit))
      
        {
            distance = Mathf.Clamp((hit.distance * 0.9f), MinDis, MaxDis);
            Debug.Log("HELLO");
        }
        else
        {
            distance = MaxDis;
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, DIR * distance, Time.deltaTime * SMOOTH);
    }
}


