using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    //objects
    public Transform camPivot;
    float heading = 0;
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
        heading += Input.GetAxis("Mouse X") * Time.deltaTime * 180;
        camPivot.rotation = Quaternion.Euler(0, heading, 0);

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

        velocity = intent * 5;

        jones.Move(velocity * Time.deltaTime);
    }

}
// using this script set your Horizontal and Vertical Gravity and
// Sensitivity to 3, to allow a slight sliding effect when you stop moving