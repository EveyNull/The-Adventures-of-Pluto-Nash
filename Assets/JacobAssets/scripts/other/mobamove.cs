using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobamove : MonoBehaviour
{
    Vector2 input;

    int UnitsPerSec = 5;

	void Update ()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        input = Vector2.ClampMagnitude(input, 1);

        //make object's x position increase by the Horizontal input option over time
        transform.position += new Vector3(input.x, 0, input.y)*Time.deltaTime * UnitsPerSec;
	}
}
