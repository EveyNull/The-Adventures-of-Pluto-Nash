// Created by Jacob Woodman, signed 15/07/2019
// movement of player
//modified for animations 25/1/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEditor.UIElements;
using UnityEngine.Serialization;

public class playerControlLerpSmoothAnim : MonoBehaviour
{
    //objects
    public new Transform camera;
    private CharacterController jones;
    public Animator anim;
    public GameObject oldPosObj;
    private oldPosUpdate oldPosUpdateScript;
    public GameObject RIPTIRE;
    public lefthand hand;
    AudioSource voiceline;
    
    public GameObject bodyTire;
    public GameObject tireSmoke;

    //Camera
    CameraLook camScript;
    Vector3 camF;
    Vector3 camR;

    //User input
    public Vector2 input;

    //Movement Physics
    private Vector3 position;
    private Vector3 intent;
    public Vector3 velocity;
    private Vector3 velocityXZ;

    // player's top speed when moving
    public float topSpeed = 8;

    // rate of acceleration and deceleration when
    // beginning and ending movement is based on this value
    public float accel = 5;

    //rate of rotation
    float turnSpeed = 20;

    //Gravity and jumping
    float grav = 19.62f;
    public bool grounded = false;
    float jump = 10f;
    public int maxJumps = 1;
    int jumpsRemaining = 0;

    //upgrades
    public bool jumpAltered = false;
    public bool speedAltered = false;
    public float jumpTimeRemaining = 0;
    public float speedTimeRemaining = 0;
    public bool punchAltered = false;
    public float punchTimeRemaining = 0;
    private GameObject JumpParticle;
    private GameObject SpeedParticle;
    private GameObject PunchParticle;
    
    public int points = 0;

    //combat
    bool isAttacking = false;

    //2d mode
    [HideInInspector]
    public PathCreator path = null;
    public EndOfPathInstruction eOPinstruction;
    public float pathPosition = 0;
    public bool in2D = false;
    [HideInInspector]
    public bool blocked = false;

    public bool inCutscene;

    void Start()
    {
        //lock cursor
        Cursor.lockState = CursorLockMode.Locked;

        camScript = camera.GetComponent<CameraLook>();
        jones = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        jumpsRemaining = maxJumps;
        JumpParticle = transform.GetChild(3).GetChild(0).gameObject;
        SpeedParticle = transform.GetChild(3).GetChild(1).gameObject;
        PunchParticle = transform.GetChild(3).GetChild(2).gameObject;
        voiceline = GetComponent<AudioSource>();

        oldPosUpdateScript = oldPosObj.GetComponent<oldPosUpdate>();
    }

    void Update()
    {
        position = transform.position;

        Gravity();

        if (!inCutscene)
        {
            GetInput();

            CalculateCamera();

            MovePlane();

            Jump();

            //StartCoroutine(Attack());
        }
        else
        {
            velocity = new Vector3(0, velocity.y, 0);
        }

        checkJumps();

        checkSpeed();
        
        checkPunch();

        checkPoints();

        if (in2D)
        {
            camScript.in2D = true;
            SplineMove2D();
        }
        else
        {
            camScript.in2D = false;
            
        }
        
        jones.Move(velocity * Time.deltaTime);
    }

    //take input from both horizontal and vertical, X and Z in the world and make it input.
    //clamp input to maximum value of 1
    void GetInput()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        input = Vector2.ClampMagnitude(input, 1);
    }

    //set camera position and movement based on player location
    //will only update when player is moving the camera, or when player doesnt move
    private void CalculateCamera()
    {
        if (camScript.input.magnitude > 0 || input.magnitude == 0 || in2D)
        {
            camF = camera.forward;
            camR = camera.right;

            camF.y = 0;
            camR.y = 0;
            camF = camF.normalized;
            camR = camR.normalized;
        }
    }
    
    //colliding with world makes you grounded
        public void groundStay(Collider other)
    { 
        if (!isAttacking)
        {
            if (other.CompareTag("world") || other.CompareTag("enemy"))
            {
                grounded = true;
                jumpsRemaining = maxJumps;
            }
        }
    }

    //no longer colliding? no longer touching the ground
    public void groundExit(Collider other)
    {
        if (other.CompareTag("world") || other.CompareTag("enemy"))
        {
            anim.ResetTrigger("JumpTrigger");
            grounded = false;
            jumpsRemaining = maxJumps - 1;
        }
    }

    //moves along the X and Z axis, using the camera position as reference.
    void MovePlane()
    {
            intent = camF * input.y + camR * input.x;

            if (input.magnitude > 0)
            {
                anim.SetBool("Moving", true);
                anim.SetFloat("speedMultiplier", input.magnitude * (topSpeed / 8));
                //having a walk animation would be really good here because my player can move at multiple speeds, but the default asset does not have one
                //not removing variable speeds though because I feel it is integral to good platforming
                Quaternion rot = Quaternion.LookRotation(intent);
                transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
            }
            else
            {
                anim.SetBool("Moving", false);
            }
            velocityXZ = velocity;
            velocityXZ.y = 0;
            velocityXZ = Vector3.Lerp(velocityXZ, transform.forward * input.magnitude * topSpeed, accel * Time.deltaTime);

            velocity = new Vector3(velocityXZ.x, velocity.y, velocityXZ.z);
    }

    // if airborne, increase rate at which player is falling.
    void Gravity()
    {
        if (grounded)
        {
            if(anim.GetFloat("airTimer") > 0)
            {
                anim.SetFloat("airTimer", 0);
            }
            if(velocity.y >= -5)
                velocity.y -= 5 * Time.deltaTime;
            else if (velocity.y < -5)
                velocity.y = -5;
        }
        else
        {
            anim.SetFloat("airTimer", anim.GetFloat("airTimer") + (grav * Time.deltaTime));
                velocity.y -= grav * Time.deltaTime;
        }

        velocity.y = Mathf.Clamp(velocity.y, -24, 36);
    }

    void Jump()
    {
        if(grounded || jumpsRemaining > 0)
        {

            if (Input.GetButtonDown("Jump"))
            {
                anim.SetTrigger("JumpTrigger");
                velocity.y = jump;
                jumpsRemaining--;
            }
        }
    }

    void checkJumps()
    {
        if(jumpAltered)
        {
            jumpAltered = false;
            jumpTimeRemaining = 10f;
            JumpParticle.SetActive(true);
            JumpParticle.GetComponent<ParticleSystem>().Play();
        }

        if (IsTimerZero(ref jumpTimeRemaining))
        {

            JumpParticle.SetActive(false);
            maxJumps = 1;
        }
    }

    //return true if timer is zero
    //decrement timer if not
    bool IsTimerZero(ref float timer)
    {
        if (timer <= 0) return true;

        timer -= Time.deltaTime;
        return false;       
    }

    void checkSpeed()
    {
        if (speedAltered)
        {
            speedAltered = false;
            speedTimeRemaining = 10f;
            SpeedParticle.SetActive(true);
            SpeedParticle.GetComponent<ParticleSystem>().Play();
        }

        if (speedTimeRemaining > 0)
        {
            speedTimeRemaining -= Time.deltaTime;
            //thwart chance of speed going over maximum
            if (topSpeed > 16)
            {
                topSpeed = 16;
            }
        }
        else if (IsTimerZero(ref speedTimeRemaining) && !isAttacking)
        {
            SpeedParticle.SetActive(false);
            topSpeed = 10;
        }
    }

    void checkPunch()
    {
        if(punchAltered)
        {
            punchAltered = false;
            punchTimeRemaining = 10f;
            hand.punchUpgraded = true;
            PunchParticle.SetActive(true);
            PunchParticle.GetComponent<ParticleSystem>().Play();
        }
        
        if(IsTimerZero(ref punchTimeRemaining))
        {
            PunchParticle.SetActive(false);
            hand.punchUpgraded = false;
        }
    }

    private void checkPoints()
    {
        if (points >= 3)
        {
            tireSmoke.SetActive(true);
        }
        else tireSmoke.SetActive(false);
    }

    //private IEnumerator Attack()
    //{
    //        if (Input.GetButtonDown("Fire1"))
    //        {
    //            if (!isAttacking)
    //            {
    //                isAttacking = true;
    //                anim.ResetTrigger("Stunned");
    //                anim.SetTrigger("AttackTrigger");
    //                topSpeed = topSpeed / 6;
    //                yield return new WaitForSeconds(.5f);
    //                isAttacking = false;
    //                topSpeed *= 6;
    //        }
    //        }
    //        else if (Input.GetButtonDown("Ultimate") && points >= 3)
    //        {
    //            voiceline.Play();
    //            Vector3 tirePos = transform.position + new Vector3(0, 2, 0);
    //            GameObject TIRE = Instantiate(RIPTIRE, tirePos, transform.rotation);
    //            tireControl control = TIRE.GetComponent<tireControl>();

    //            control.camera = camera;
    //            control.oldPosObj = oldPosObj;
    //            control.player = this.gameObject;
                    
    //            points -= 3;
    //            inCutscene = true;
    //            oldPosUpdateScript.SwapTopic(TIRE);
    //        }
    //}

    void SplineMove2D()
    {
        if (!blocked)
        {
            if (input.magnitude != 0)
            {
                if (input.x != 0)
                {
                    pathPosition += input.x * Time.deltaTime * topSpeed;
                }
            }
        }
        Vector3 splinePos = path.path.GetPointAtDistance(pathPosition, eOPinstruction);
        transform.position = new Vector3(splinePos.x, position.y, splinePos.z);
    }

    public void Stun()
    {
        anim.SetTrigger("Stunned");
        if (!inCutscene)
        {
            inCutscene = true;
            StartCoroutine(stunTimer());
        }
    }

    private IEnumerator stunTimer()
    {
        yield return new WaitForSeconds(.4f);
        inCutscene = false;
    }
}
// using this script set your Horizontal and Vertical input Gravity and
// Sensitivity to 999, as Vector3.Lerp handles movement accel and decel.
// Know this is less efficient and not the intended use of Lerp, but still fine.

//https://answers.unity.com/questions/29777/first-person-controller-doesnt-slide-down-slopes.html