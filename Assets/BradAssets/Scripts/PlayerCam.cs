﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour {

    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 50.0f;

    public Transform target;
    public Transform camTransform;
    public float sensitivityX = 4.0f;
    public float sensitivityY = 1.0f;
    public float dist = 10.0f;

    private float currentX = 0.0f;
    private float currentY = 0.0f;
  

	// Use this for initialization
	private void Start () {
        camTransform = this.transform;
        

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
	}

    private void Update()
    {
        currentX += (Input.GetAxis("Mouse X") * sensitivityX);
        currentY += (Input.GetAxis("Mouse Y") * sensitivityY);

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }
	
	// Update is called once per frame
	private void LateUpdate () {

        Vector3 dir = new Vector3(0, 0, -dist);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = target.position + rotation * dir;
        camTransform.LookAt(target.position);
		
	}
}
