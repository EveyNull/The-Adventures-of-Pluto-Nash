using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
using UnityEngine;

public class toggleOn : MonoBehaviour
{
    public PathFollower[] scripts;
    private Renderer buttonRenderer;

    private void Start()
    {
        buttonRenderer = gameObject.GetComponent<Renderer>();
        buttonRenderer.material.SetColor("_Color", Color.green);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("LeftFist")) return;
        if (scripts[0].moving)
        {
            foreach (PathFollower script in scripts)
            {
                script.moving = false;
            }
            buttonRenderer.material.SetColor("_Color", Color.red);
        }
        else
        {
            foreach (PathFollower script in scripts)
            {
                script.moving = true;
            }
            buttonRenderer.material.SetColor("_Color", Color.green);
        }

        Debug.Log("toggle");
    }
}
