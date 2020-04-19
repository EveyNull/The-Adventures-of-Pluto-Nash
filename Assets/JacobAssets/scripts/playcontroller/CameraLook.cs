// Created by Jacob Woodman, signed 15/07/2019
// controls rotation of camera

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    // in this case topic is the player
    public Transform topic;
    public Transform target;
    private float heading = 0;
    private float tilt = 15;
    public float minDist = 2;
    public float maxDist = 7;
    private float camDist = 7;
    public float zoomSpeed = 25;
    private float currentDist = 0;
    private float topicHeight = 1;
    public Vector2 input;
    private float countdown = 2f;

    [HideInInspector]
    public bool in2D = false;

    private void Start()
    {
        currentDist = camDist;
       // target = topic.GetChild(0);
    }

    private void Update()
    {
        //Vector3 camDirection = topic.position - this.transform.position;
        //RaycastHit hit;
        
        //if (Physics.Raycast(this.transform.position, camDirection, out hit, 7))
        //{
        //    if (!hit.collider.CompareTag("Player"))
        //    {
        //        Debug.DrawRay(this.transform.position, camDirection * hit.distance, Color.yellow);
        //        Debug.Log("BLOCKED");
        //        currentDist = hit.distance;
        //    }
        //    else
        //    {
        //        currentDist = camDist;
        //    }
        //}
        //else
        //{
        //    currentDist = camDist;
        //}
        
        GetInput();

        if (countdown <= 0)
        {
            ResetCamPos();
        }
        else
        {
            countdown -= Time.deltaTime;
        }
    }

    //take input from both horizontal and vertical, X and Z in the world and make it input.
    //clamp input to maximum value of 1
    void GetInput()
    {
        input = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        input = Vector2.ClampMagnitude(input, 1);
    }

    //resets the camera position by calling rotate function for four different axis
    private void ResetCamPos()
    {
        Rotate(ref heading, 45, 135, 90);
        Rotate(ref heading, 135, 225, 180);
        Rotate(ref heading, 225, 315, 270);
        Rotate(ref heading, 315, 405, 360);
    }
    
    //moves heading toward angle if heading is within bound1 and bound2, but not equal to angle
    private void Rotate(ref float var, int bound1, int bound2, int angle)
    {
        //checks if within bounds of angle
        if (var > angle - .5f && var < angle + .5f) return;
        
        if (var > bound1 && var < angle)
        {
            var += Time.deltaTime * 25;

        }
        else if (var <= bound2 && var > angle)
        {
            var -= Time.deltaTime * 25;
        }
    }

    private void LateUpdate()
    {
        if(!in2D)
        {
            Late3DUpdate();
        }
        else
        {
            Late2DUpdate();
        }

    }
    // using LateUpdate instead of update means camera position 
    // will always change after the player position changes.
    // this avoids potential desync on lower framerates

    private void Late3DUpdate()
    {
        if (input.magnitude > 0)
        {
            countdown = 2;

            heading += Input.GetAxis("Mouse X") * Time.deltaTime * 180;
            if (heading > 405)
                heading = 45;

            if (heading < 45)
                heading = 405;

            tilt += Input.GetAxis("Mouse Y") * Time.deltaTime * 180;

            tilt = Mathf.Clamp(tilt, -5, 60);
        }

        transform.rotation = Quaternion.Euler(tilt, heading, 0);

        transform.position = topic.position - transform.forward * currentDist + Vector3.up * topicHeight;
    }

    private void Late2DUpdate()
    {

            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * 8);

            transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * 8);
    }
}