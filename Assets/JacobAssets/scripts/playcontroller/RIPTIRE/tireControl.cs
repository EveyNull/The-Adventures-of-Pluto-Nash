using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class tireControl : MonoBehaviour
{
    //objects
    public new Transform camera;
    private CharacterController jonesTire;
    public GameObject oldPosObj;
    private oldPosUpdate oldPosUpdateScript;
    public GameObject player;
    public GameObject explosion;

    //Camera
    private CameraLook camScript;
    private Vector3 camF;
    private Vector3 camR;

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
    float accel = 5;

    //rate of rotation
    public float turnSpeed = 20;

    //Gravity and jumping
    private float grav = 19.62f;
    public bool grounded = false;
    private float jump = 10f;
    private int jumpsRemaining = 1;

    //combat
    private bool isAttacking = false;

    //2d mode
    [HideInInspector]
    public PathCreator path = null;
    public EndOfPathInstruction eOPinstruction;
    public float pathPosition = 0;
    public bool in2D = false;
    [HideInInspector]
    public bool blocked = false;

    public bool inCutscene;
    private playerControlLerpSmoothAnim playerControlLerpSmoothAnim;

    void Start()
    {
        playerControlLerpSmoothAnim = player.GetComponent<playerControlLerpSmoothAnim>();
        //lock cursor
        Cursor.lockState = CursorLockMode.Locked;

        camScript = camera.GetComponent<CameraLook>();
        jonesTire = GetComponent<CharacterController>();

        oldPosUpdateScript = oldPosObj.GetComponent<oldPosUpdate>();
    }

    void Update()
    {
        position = transform.position;
        
        Gravity();
        
        GetInput();

        CalculateCamera();

        MovePlane();

        Jump();

        Attack();
            
        jonesTire.Move(velocity * Time.deltaTime);
    }

    //take input from both horizontal and vertical, X and Z in the world and make it input.
    //clamp input to maximum value of 1
    void GetInput()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), 1);

        input = Vector2.ClampMagnitude(input, 1);
    }

    //set camera position and movement based on player location
    //will only update when player is moving the camera, or when player doesnt move
    private void CalculateCamera()
    {
        if (camScript.input.magnitude > 0 || Input.GetAxis("Horizontal") > 0 || in2D)
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
    public void GroundStay(Collider other)
    {
        if (other.CompareTag("world") || other.CompareTag("enemy"))
            {
                grounded = true;
                jumpsRemaining = 1;
            }
    }

    //no longer colliding? no longer touching the ground
    public void groundExit(Collider other)
    {
        if (other.CompareTag("world") || other.CompareTag("enemy"))
        {
            grounded = false;
            jumpsRemaining -= 1;
        }
    }

    //moves along the X and Z axis, using the camera position as reference.
    void MovePlane()
    {
            intent = camF * input.y + camR * input.x;
            
            Quaternion rot = Quaternion.LookRotation(intent);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
            
            velocityXZ = velocity;
            velocityXZ.y = 0;
            velocityXZ = Vector3.Lerp(velocityXZ, transform.forward * topSpeed, accel * Time.deltaTime);

            velocity = new Vector3(velocityXZ.x, velocity.y, velocityXZ.z);
    }

    // if airborne, increase rate at which player is falling.
    void Gravity()
    {
        if (!grounded) velocity.y -= grav * Time.deltaTime;
        else if (velocity.y >= -5)
        {
            velocity.y -= 5 * Time.deltaTime;
        }

        velocity.y = Mathf.Clamp(velocity.y, -24, 36);
    }

    void Jump()
    {
        if(grounded || jumpsRemaining > 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = jump;
                jumpsRemaining--;
            }
        }
    }

    private void Attack()
    {
            if (Input.GetButtonDown("Fire1"))
            {
            Debug.Log("exploding");
                GameObject explode = Instantiate(explosion, position, transform.rotation);
                oldPosUpdateScript.SwapTopic(explode);
                Destroy(gameObject);
            }
    }
}
// using this script set your Horizontal and Vertical input Gravity and
// Sensitivity to 999, as Vector3.Lerp handles movement accel and decel.
// Know this is less efficient and not the intended use of Lerp, but still fine.

//https://answers.unity.com/questions/29777/first-person-controller-doesnt-slide-down-slopes.html