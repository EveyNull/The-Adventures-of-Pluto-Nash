﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveScript : MonoBehaviour
{
    public float cameraRotateSpeed = 10f;
    public Transform targetFollow;

    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = targetFollow.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = targetFollow.position - offset;

        transform.Translate(-Vector3.right * Input.GetAxis("JosephRightHori") * Time.deltaTime * cameraRotateSpeed);

        float vertMove = Input.GetAxis("JosephRightVerti");
        bool allowMove = (transform.position.y - targetFollow.position.y <= 4 || vertMove <= 0) && (transform.position.y - targetFollow.position.y >= -2 || vertMove >= 0);
        if (allowMove)
        {
            transform.Translate(0f, vertMove * Time.deltaTime * cameraRotateSpeed, 0f);
        }
        offset = targetFollow.position - transform.position;

        transform.LookAt(targetFollow);

        RaycastHit hit;

        if(Physics.Raycast(targetFollow.position, -transform.forward, out hit, Vector3.Distance(targetFollow.position, transform.position)))
        {
            transform.position = hit.point;
        }
    }
}
