using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
   private Transform TargetBPOS;
    private Transform TargetDPOS;
    private Transform Standpos;
    private RaycastHit hitfront;
    private CharacterController controller;
    public float Raylength = 1.75f;
    public bool near_switch = false;
    public bool Switch_Pressed = false;

    public float speed = 5f;
    public float ButtonSpeed = 0.2f;
    public float DoorSpeed = 6f;
    public GameObject ENDB_POS;
    public GameObject button;
    public GameObject ENDD_POS;
    public GameObject Door;
    public GameObject STAND;
    Animator anim;



    public Camera MainCam;
    public Camera ButtonCam;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        TargetBPOS = GameObject.FindGameObjectWithTag("B_ENDPOS").transform;
        TargetDPOS = GameObject.FindGameObjectWithTag("D_ENDPOS").transform;
        Standpos = GameObject.FindGameObjectWithTag("STAND").transform;
        anim = GetComponent<Animator>();
     
    }

    // Update is called once per frame
    void Update()
    {
        
        switchpress();
        //Debug.DrawRay(transform.position + transform.up * 1.5f, transform.forward * Raylength);
       

    }
    void switchpress()
    {
        if (Input.GetKeyDown("JosephButtonPress") && controller.isGrounded)
        {
           // if (Physics.Raycast(transform.position + transform.up * 1.5f, transform.forward, out hitfront, Raylength))
           // {
                if (hitfront.collider.gameObject.tag == "Button")

                {

                   near_switch = true;
                    Switch_Pressed = true;
                   
                    anim.SetInteger("ATTACK", 3);
                    MainCam.enabled = false;
                    ButtonCam.enabled = true;
                    transform.position = new Vector3(STAND.transform.position.x,transform.position.y,STAND.transform.position.z);
                    //anim.SetBool("Has_Switched", true);


                }
           // }
          
        }
       

        if (Switch_Pressed == true)
        {
           

            button.transform.position = Vector3.MoveTowards(button.transform.position, TargetBPOS.position, ButtonSpeed/5 * Time.deltaTime);
            StartCoroutine(DoorOpen());
            StartCoroutine(CAM_Switch());
            StartCoroutine(stopSwitch());
          

        }
        if (Input.GetKeyUp(KeyCode.Joystick1Button2))
        {
            anim.SetInteger("ATTACK", 0);
            
        }


    }
    private IEnumerator DoorOpen()
    {
yield return new WaitForSeconds (1f);
        Door.transform.position = Vector3.MoveTowards(Door.transform.position, TargetDPOS.position, DoorSpeed * Time.deltaTime);
       
       
    }
    private IEnumerator CAM_Switch()
    {
        yield return new WaitForSeconds(2f);
        MainCam.enabled = true;
        ButtonCam.enabled = false;
        near_switch = false;
       
    }
    private IEnumerator stopSwitch()
    {
        yield return new WaitForSeconds(5f);
        //Switch_Pressed = false;
    }

}
