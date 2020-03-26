// Created by Jacob Woodman, signed 15/07/2019
// movement of player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControlLerpSmooth : MonoBehaviour
{
    //objects
    public Transform camera;
    CharacterController jones;

    //Camera
    Vector3 camF;
    Vector3 camR;

    //User input
    Vector2 input;

    //Physics
    Vector3 intent;
    Vector3 velocity;
    Vector3 velocityXZ;

    // player's top speed when moving
    float speed = 5;

    // rate of acceleration and deceleration when
    // beginning and ending movement is based on this value
    float accel = 5;

    //rate of rotation
    float turnSpeed = 20;

    //Gravity
    float grav = 9.81f;
    bool grounded = false;

    void Start()
    {
        jones = GetComponent<CharacterController>();
    }

    void Update()
    {
        GetInput();

        CalculateCamera();

        CalculateGround();

        MovePlane();

        Gravity();

        Jump();

        jones.Move(velocity * Time.deltaTime);
    }


    void GetInput()
    {

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        input = Vector2.ClampMagnitude(input, 1);
    }

    void CalculateCamera()
    {
        camF = camera.forward;
        camR = camera.right;

        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;
    }

    void CalculateGround()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position+Vector3.up*0.1f, -Vector3.up, out hit, 0.2f))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    void MovePlane()
    {
        intent = camF * input.y + camR * input.x;

        if(input.magnitude > 0)
        {
            Quaternion rot = Quaternion.LookRotation(intent);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }
        velocityXZ = velocity;
        velocityXZ.y = 0;
        velocityXZ = Vector3.Lerp(velocityXZ, transform.forward * input.magnitude * speed, accel*Time.deltaTime);
        velocity = new Vector3(velocityXZ.x, velocity.y, velocityXZ.z);
    }

    void Gravity()
    {
        if (grounded)
        {
            velocity.y = -0.5f;
        }
        else
        {
            velocity.y -= grav * Time.deltaTime;
        }

        velocity.y = Mathf.Clamp(velocity.y, -10, 10);
    }

    void Jump()
    {
        if(grounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = 6;
                //14 degrees is highest angle we can still jump on slopes
                //issue with raycast length in CalculateGround
                //doesnt work when descending slopes
            }
        }
    }

}
// using this script set your Horizontal and Vertical input Gravity and
// Sensitivity to 999, as Vector3.Lerp handles movement accel and decel.
// Know this is less efficient and not the intended use of Lerp, but
// still acceptable to do.