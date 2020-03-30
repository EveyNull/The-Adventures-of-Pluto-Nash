using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spline : MonoBehaviour
{
    public Movement getmove;

    public Transform[] target;

    private CharacterController controller;

    private int current;
    private int targetPOINT;

    public Camera MAIN;
    public Camera Splinecam;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPOINT <= 0)
        {
            targetPOINT = 0;
        }
        if (current <= 0)
        {
            current = 0;
        }
        if (targetPOINT >= target.Length)
        {
            targetPOINT = target.Length;
        }
        if (current == target.Length)
        {
            current = target.Length;
        }

        RaycastHit hit;

        Vector3 POS = transform.position + controller.center;
        Debug.DrawRay(transform.position, POS);

        if (Physics.SphereCast(POS, controller.height / 2, transform.forward, out hit, 0.5f) && hit.transform.tag == "SplineDoor")
        {
            getmove.Splinehit = true;

            MAIN.enabled = false;
            Splinecam.enabled = true;

        }


        if (Physics.SphereCast(POS, controller.height / 2, transform.forward, out hit, 0.5f) && hit.transform.tag == "SplineExit")
        {
            getmove.Splinehit = false;

            MAIN.enabled = true;
            Splinecam.enabled = false;

            current = 0;
            targetPOINT = 0;


            Debug.Log("hi");
            for (int i = 0; i<target.Length; i++)
            {
                target[i].GetComponent<POINTCOL>().hit = false;
              
            }
         

      
        }

        if (getmove.Splinehit == true )
 
        {

            if (Input.GetAxis("left_joyhori") == 1)
            {
                if (target[current].GetComponent<POINTCOL>().hit == false)
                {

                    Vector3 pos = Vector3.LerpUnclamped(transform.position, target[current].transform.position, 1000000000000);
                    pos = pos.normalized;

                    controller.Move(pos * 10 * Time.deltaTime);
                }
                else
                {
                    current = (current + 1) % target.Length;
                    targetPOINT++;
                    Debug.Log(current);
                

                }
            }
           
        }
       


        if (getmove.Splinehit == true) 
        {
            if (Input.GetAxis("left_joyhori") == -1)
            {
                if (target[current].GetComponent<POINTCOL>().hit != false)
                {
                    Vector3 pos = Vector3.LerpUnclamped(transform.position, target[targetPOINT].transform.position, 1000000000000);
                    pos = pos.normalized;

                    controller.Move(pos * 10 * Time.deltaTime);
                }
                else
                {
                    current = (current - 1) % target.Length;
                    targetPOINT--;
                    Debug.Log(current);
                 
                }
            }
          
      

        }
        if (targetPOINT <= 0)
        {
            targetPOINT = 0;
        }
        if (current <= 0)
        {
            current = 0;
        }
        if (targetPOINT >= target.Length)
        {
            targetPOINT = target.Length;
        }
        if (current == target.Length)
        {
            current = target.Length;
        }
    }
}
