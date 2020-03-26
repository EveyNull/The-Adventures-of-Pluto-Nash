using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameramovespheres: MonoBehaviour
{
    public Transform camPivot;
    float heading = 0;
    public Transform camera;

    public Rigidbody sphere1;
    public CharacterController sphere2;

    Vector2 input;

    int UnitsPerSec = 5;

    void Update()
    {
        heading += Input.GetAxis("Mouse X") * Time.deltaTime * 180;
        camPivot.rotation = Quaternion.Euler(0, heading, 0);

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        input = Vector2.ClampMagnitude(input, 1);

        Vector3 camF = camera.forward;
        Vector3 camR = camera.right;

        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;

        //make object's x position increase by the Horizontal input option over time
        //purely transform oriented movement
        transform.position += (camF * input.y + camR * input.x) * Time.deltaTime * UnitsPerSec;

        //rigidbody based movement - walls dont affect this
        sphere1.position += (camF * input.y + camR * input.x) * Time.deltaTime * UnitsPerSec;

        //charactercontroller based movement - cannot move through walls
        sphere2.Move((camF * input.y + camR * input.x) * Time.deltaTime * UnitsPerSec);
    }
}
