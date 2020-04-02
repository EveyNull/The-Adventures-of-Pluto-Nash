using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public GameObject playerCam;
    public Animation anim;

	// Use this for initialization
	void Start () {
        playerCam = Camera.main.gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Interact()
    {
          anim.Play("DoorOpen");
    }
}
