using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camTransition : MonoBehaviour
{
    public Transform camPos1;
    public Transform camPos2;
    public bool transitionComplete = false;

    Transform currentPos;

    public float moveSpeed = 3;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector3.Lerp(camPos1.transform.position, camPos2.transform.position, Time.deltaTime * moveSpeed);
        gameObject.transform.rotation = Quaternion.Slerp(camPos1.transform.rotation, camPos2.transform.rotation, Time.deltaTime * moveSpeed);

        if (transform.position == camPos2.transform.position)
        {
            transitionComplete = true;
            Debug.Log("transition Complete");
        }

      //  gameObject.transform.position = currentPos.transform.position;
    }
}
