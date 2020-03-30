using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public GameObject PLAYER;
    private Vector3 point;


    //private float rotspeed = 20f;
    //float play_rot = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //point = PLAYER.transform.position;
        //transform.LookAt(point);

        //play_rot = Input.GetAxis("right_joyhori") * rotspeed * Time.deltaTime;
        //transform.eulerAngles = new Vector3(0, play_rot, 0);
    }
}
