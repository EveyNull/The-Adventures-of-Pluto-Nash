using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float Speed = 6f;
    private float runspeed = 10f;
   // private float current_speed = 0f;
   // private float smoothvelocity = 0f;
    private float smoothtime = 0.1f;
     float rotspeed = 80f;
    private float gravity = 10f;
     float play_rot;
    private Rigidbody rb;
    public float j_force = 20;
    public bool jumpbool;
    public bool RunActive = false;
    public int jump_count = 0;
    Vector3 velocity = Vector3.zero;
    float FINALinput_X;
    float FINALinput_Y;
    float CAM_ROT_SPEED = 2;

    public bool Splinehit = false;
    private Transform cameratransform;

    private CharacterController controller;
    private Animator anim;
    public Switch getSWitxh;


    //public Transform[] target;

    //private int current;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        cameratransform = Camera.main.transform;
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
    
        //Jump();
      
            Move();
        
        Animation();
        Spline();
        Jump();

    }
    void Move()
    {

     
            if (getSWitxh.near_switch == false)
            {
                Vector2 moveinput = new Vector2(Input.GetAxis("JosephLeftHori"), Input.GetAxis("JosephLeftVerti"));



                Vector3 forward = cameratransform.forward; //transform.forward;
                Vector3 right = cameratransform.right; //transform.right;

                forward.y = 0;
                right.y = 0;

                forward.Normalize();
                right.Normalize();

                Vector3 DesMoveDir = (forward * -moveinput.y + right * moveinput.x).normalized;
                Vector3 GravVect = Vector3.zero;


                if (!controller.isGrounded)
                {
                    GravVect.y -= gravity;
                }
            controller.Move(GravVect * Time.deltaTime);
            if (Splinehit == false)
            {

                controller.Move(DesMoveDir * Speed * Time.deltaTime);
                





                if (Input.GetKey(KeyCode.Joystick1Button8))
                {
                    RunActive = true;

                }
                else
                {
                    RunActive = false;
                    anim.SetBool("Is_running", false);
                }

                if (RunActive == true)
                {
                    controller.Move(DesMoveDir * runspeed * Time.deltaTime);
                    anim.SetBool("Is_running", true);
                }
            }
            
            if (DesMoveDir != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(DesMoveDir.normalized), 0.1f);

            }


        }
    }
   
    void Jump()
    {
        Vector3 GravVect = Vector3.zero;


        //if (controller.isGrounded)
        //{
            if (Input.GetKeyDown("JosephJump"))//(KeyCode.Joystick1Button0))
            {
                Debug.Log("jump");
                velocity = new Vector3(0, 2, 0);
                velocity *= j_force;
                anim.SetBool("Is_Jumping", true);
                 
              
                StartCoroutine(jump_time());
            } 
        
       // }
        if(jumpbool == true && jump_count < 1 && !controller.isGrounded)
        {
            if(Input.GetKeyDown("JosephJump"))//(KeyCode.Joystick1Button0))
            {
                anim.SetBool("D_jumped", true);
                velocity = new Vector3(0, 1, 0);
                velocity *= j_force;
                jump_count += 1;
                StartCoroutine(jump_time());
            }
        }
        if (!controller.isGrounded)
        {
            GravVect.y -= gravity;
        }
        controller.Move(velocity * Time.deltaTime);
        // velocity.y -= gravity * Time.deltaTime;
       // controller.Move(GravVect * Time.deltaTime);
    }
 
    IEnumerator jump_time()
    {
        yield return new WaitForSeconds(0.2f);
        velocity = new Vector3(0, 0, 0);
        anim.SetBool("Is_Jumping", false);
        anim.SetBool("D_jumped", false);
    }

    void Spline()
    {
        //RaycastHit hit;

        //Vector3 POS = transform.position + controller.center;
        //Debug.DrawRay(transform.position, POS);

        //if(Physics.SphereCast(POS, controller.height/2,transform.forward,out hit, 0.5f) && hit.transform.tag == "SplineDoor")
        //{
        //    Splinehit = true;
        //}

        //Vector3 pos = Vector3.MoveTowards(transform.position, target[current].position, 0.1f * Time.deltaTime);
        //if (Splinehit == true && transform.position != target[current].position)
        //{
        //    if (Input.GetAxis("left_joyhori") == 1)
        //    {
        //        pos = pos.normalized;
        //        //Vector3 pos = Vector3.MoveTowards(transform.position, target[current].position, 0.1f * Time.deltaTime);
        //        controller.Move(pos * 2 * Time.deltaTime);
        //    }
        //}
        //else
        //{
        //    current = (current + 1) % target.Length;
        //}
               
    }







    void Animation()
    {
        if (getSWitxh.near_switch == false)

        {
            Vector2 moveinput = new Vector2(Input.GetAxis("JosephLeftHori"), Input.GetAxis("JosephLeftVerti"));
            //forward
            if (Input.GetAxis("JosephLeftVerti") < 0)
            {
                anim.SetBool("Is_moving", true);
                anim.SetFloat("Is_walking", 1f * moveinput.magnitude, smoothtime, Time.deltaTime);
            }
            else if (Input.GetAxis("JosephLeftVerti") > 0)
            {
                anim.SetBool("Is_moving", true);
                anim.SetFloat("Is_walking", -1f * moveinput.magnitude, smoothtime, Time.deltaTime);
            }
            else
            {
                anim.SetBool("Is_moving", false);
                anim.SetFloat("Is_walking", 0f);
            }
            //backwards


            //right
            if (Input.GetAxis("JosephLeftHori") > 0)
            {
                anim.SetBool("Is_moving", true);
                anim.SetFloat("Is_sideways", 1f * moveinput.magnitude, smoothtime, Time.deltaTime);
            }
            else if (Input.GetAxis("JosephLeftHori") < 0)
            {
                anim.SetBool("Is_moving", true);
                anim.SetFloat("Is_sideways", -1f * moveinput.magnitude, smoothtime, Time.deltaTime);
            }
            else
            {
                anim.SetBool("Is_moving", false);
                anim.SetFloat("Is_sideways", 0f);
            }


            if (controller.isGrounded)
            {

                if (Input.GetKeyDown(KeyCode.Joystick1Button0))
                {
                    //anim.SetBool("Is_Jumping", true);
                    //anim.SetFloat("Jumpfloat", 1f);
                }
                else
                {
                    // anim.SetFloat("Jumpfloat", -1f);
                    // anim.SetBool("Is_Jumping", false);
                }
            }
        }
        if(getSWitxh.near_switch == true)
        {
            anim.SetBool("Is_Moving", false);
            anim.SetFloat("Is_walking", 0f);
            anim.SetFloat("Is_sideways", 0f);
        }
    }
  

 
}
