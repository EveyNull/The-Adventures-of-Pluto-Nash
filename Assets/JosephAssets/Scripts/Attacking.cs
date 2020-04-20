using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{

    public int Phealth = 8;

    CharacterController control;
    Animator anim;
    public float Raylength = 1.75f;
    private RaycastHit hitfront;

    public Transform attackpoint;
    public float attackrange = 1;
    public LayerMask enemylayer;

    public HealthBar hpb;

    public GameObject slime;

    public Transform spawn;

   public bool hitted;

    public bool hasAttacked;


    public int Damage = 1;

   public bool playHit;

    public Material hit;
 

    GameObject SLIME;

    public bool slime1 = true;
    public bool slime2;
    // Start is called before the first frame update


    void Start()
    {

      
        control = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        SLIME = GameObject.FindGameObjectWithTag("S");
 


    }
    public void TakeDamage(int damage)
    {

        Phealth -= damage;

       
    }

    IEnumerator hashit()
    {
        yield return new WaitForSeconds(1);
        hasAttacked = false;
    }
     

    // Update is called once per frame
    void Update()
    {
        if(Phealth <= 0)
        {
            StartCoroutine(respawn());
        }

        if (Input.GetButtonDown("Fire1") && hasAttacked == false)
        {
          
            anim.SetTrigger("AttackTrigger");
            Debug.Log("Attacking");
        
        }


        if(Input.GetButtonDown("JosephButtonPress") && hasAttacked == false)
        {
            anim.SetTrigger("Kick");
        }


        hpb.health(Phealth); 


        if (Input.GetButtonDown("Fire1") && hasAttacked == false)
        {

            hasAttacked = true;
         
            StartCoroutine(delay());
     
            StartCoroutine(hastacked());
        }

        if ( Input.GetButtonDown("JosephButtonPress") && hasAttacked == false)
        {
            hasAttacked = true;

            StartCoroutine(delay());
  
            StartCoroutine(hastacked());
        }

        if (Input.GetButtonUp("Fire1") || Input.GetKeyUp(KeyCode.Joystick1Button2))
        {

          // anim.SetInteger("ATTACK", 0);

       }

        IEnumerator respawn()
        {


            transform.position = spawn.transform.position;
           
            yield return new WaitForSeconds(0f);
            Phealth = 8;
        }


        if (hitted == true)
        {
      
            Debug.Log("hitted is true dont lie");
            StartCoroutine(stophit());
        }
        Debug.DrawRay(transform.position + transform.up * 0.25f, transform.forward * Raylength,Color.red);     
    }




    IEnumerator stophit()
    {
        yield return new WaitForSeconds(1f);
        hitted = false;
    }
   







  IEnumerator hastacked()
    {
        yield return new WaitForSeconds(0.75f);
        hasAttacked = false;
    }
 

        IEnumerator delay()
    {
        yield return new WaitForSeconds(0.4f);
  

        Collider[] hitenemy = Physics.OverlapSphere(attackpoint.position, attackrange, enemylayer);


        foreach (Collider enemy in hitenemy)
        {

            Debug.Log("we hit" + enemy.name);
            Debug.Log("attacked");
            enemy.GetComponent<Combat_Slime>().TakeDamage(Damage);
            
        }
    }

  void OnDrawGizmosSelected()
    {
        if (attackpoint == null)
            return;
        Gizmos.DrawWireSphere(attackpoint.position, attackrange);
    }
}
