using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float walkSpeed = 10;
	public float runSpeed = 20;
	public float jumpHeight = 5;

    public int attackInt = 0;
    public int comboMax = 3;
    public float comboTimer = 0.5f;
    public Collider[] hitboxes;

    private float turnSmoothTime = 0.1f;
    float turnSmoothVel;

    private float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;
    
    public Animator animator;

    private Transform playerCam;
	private Rigidbody rb;
	private bool canJump = true;
    private int numOfJumps = 0;
    private int maxJumps = 1;
    private bool canBoop = false;

    public float powerUpTime = 30.0f;
    public bool inSpeedBoost = false;
    public bool canDoubleJump = false;

	// Use this for initialization
	void Start () {
		playerCam = Camera.main.transform;
		rb = this.gameObject.GetComponent<Rigidbody>();
	}



    // Update is called once per frame
    void Update()
    {

       
        if(inSpeedBoost)
        {
            walkSpeed = 17;
            runSpeed = 17;
        }
        else
        {
            walkSpeed = 6;
            runSpeed = 10;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;



        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + playerCam.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVel, turnSmoothTime);
        }

        bool running = Input.GetButton("Run");

        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
        
        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
        
        animator.SetFloat("SpeedForward", currentSpeed);

        Jump();

        if (Input.GetButtonDown("Attack"))
        {
            comboTimer = 1.5f;
            StartCoroutine(Attack());
            
        }

        comboTimer -= Time.deltaTime;
        if (comboTimer < 0)
        {
            attackInt = 0;
        }

        animator.SetInteger("Attack", attackInt);
        for (int i = 0; i < hitboxes.Length; i++)
        {
            hitboxes[i].enabled = false;
        }


        hitboxes[attackInt-1].enabled = true;


        
     

    }

    void Jump()
    {
        if(numOfJumps == maxJumps)
        {
            canJump = false;
        }
        else
        {
            canJump = true;
        }
        if (rb.velocity.y <= 0.1 && rb.velocity.y >= -0.1)
        {
            numOfJumps = 0;
            if (canDoubleJump)
            {
                maxJumps = 2;
            }
            else
            {
                maxJumps = 1;
            }
            
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
        }
      
        if (rb.velocity.y >= 0.1)
        {
           
                
                animator.SetBool("Falling", false);
                animator.SetBool("Jumping", true);
    
        }
        if (rb.velocity.y <= -0.1)
        {
           

                animator.SetBool("Falling", true);
                animator.SetBool("Jumping", false);

            
        }
        


        if (Input.GetButtonDown("Jump") && canJump)
        { 
            rb.velocity = Vector3.up * jumpHeight;
            numOfJumps++;
            Debug.Log(numOfJumps);
            
        }
    }

    IEnumerator Attack()
    {
        attackInt++;

        if (attackInt == 3)
        {
            yield return new WaitForSeconds(0.5f);
            attackInt = 0;
        }

       

        

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("DoubleJump"))
        {
            canDoubleJump = true;
            
            StartCoroutine(DoubleJump());
            Destroy(other.gameObject);
        }
        else if(other.gameObject.CompareTag("SuperSpeed"))
        {
            inSpeedBoost = true;
            StartCoroutine(SpeedBoost());
            Destroy(other.gameObject);
        }
        else if(other.gameObject.CompareTag("Door"))
        {
            
            canBoop = true;
            Debug.Log(canBoop);
            if (Input.GetButtonDown("Interact"))
            {
                 other.gameObject.GetComponent<Door>().Interact();
               // animator.SetTrigger("Interact");
            }
        }

        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            canBoop = false;
        }
    }

    IEnumerator DoubleJump()
    {
        yield return new WaitForSeconds(powerUpTime);
        canDoubleJump = false;
    }
    IEnumerator SpeedBoost()
    {
        yield return new WaitForSeconds(powerUpTime);
        inSpeedBoost = false;
    }

}
