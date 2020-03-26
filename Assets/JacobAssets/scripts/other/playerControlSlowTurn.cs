using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControlSlowTurn : MonoBehaviour
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

    // player's top speed when moving
    float speed = 5;

    // rate of acceleration and deceleration when
    // beginning and ending movement is based on this value
    float accel = 16;

    //rate of rotation
    float turnSpeed = 5;
    float turnSpeedLow = 5;
    float turnSpeedHigh = 20;

    void Start()
    {
        jones = GetComponent<CharacterController>();
    }

    void Update()
    {
        GetInput();

        CalculateCamera();

        Move();
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

    void Move()
    {
        intent = camF * input.y + camR * input.x;

        // this slows player turning down when moving at higher speeds.
        float tS = velocity.magnitude / speed;
        turnSpeed = Mathf.Lerp(turnSpeedHigh, turnSpeedLow, tS);

        if (input.magnitude > 0)
        {
            Quaternion rot = Quaternion.LookRotation(intent);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }

        velocity = Vector3.Lerp(velocity, transform.forward * input.magnitude * speed, accel * Time.deltaTime);

        jones.Move(velocity * Time.deltaTime);
    }

}
// using this script set your Horizontal and Vertical input Gravity and
// Sensitivity to 999, as Vector3.Lerp handles movement accel and decel.
// Know this is less efficient and not the intended use of Lerp, but
// still acceptable to do.