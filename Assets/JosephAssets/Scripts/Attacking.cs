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
    Renderer render;
    Combat_Slime getSlime;

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
       render = GetComponent<Renderer>();


    }

    IEnumerator hashit()
    {
        yield return new WaitForSeconds(1);
        hasAttacked = false;
    }
     

    // Update is called once per frame
    void Update()
    {
        if(Phealth == 0)
        {
            StartCoroutine(respawn());
        }

        if (Input.GetKeyDown("JosephSwordAttack") && hasAttacked == false)
        {
            //hasAttacked = true;
            anim.SetTrigger("SwordAttack");
            Debug.Log("swinging");
            //anim.Play("Attack");
           // StartCoroutine(hastacked());
        }


        if(Input.GetKeyDown("JosephButtonPress") && hasAttacked == false)
        {
            anim.SetTrigger("Kick");
        }


         hpb.health(Phealth); 


        if (Input.GetKeyDown("JosephSwordAttack") && hasAttacked == false)
        {

           // anim.SetInteger("ATTACK", 3);
            hasAttacked = true;
         
            StartCoroutine(delay());
            // Newattack();
            StartCoroutine(hastacked());
        }

        if ( Input.GetKeyDown("JosephButtonPress") && hasAttacked == false)
        {
            hasAttacked = true;

            StartCoroutine(delay());
  
            StartCoroutine(hastacked());
        }

        if (Input.GetKeyUp("JosephSwordAttack") || Input.GetKeyUp(KeyCode.Joystick1Button2))
        {

           anim.SetInteger("ATTACK", 0);

       }

        IEnumerator respawn()
        {
          
               
            transform.position = spawn.transform.position;
           
            yield return new WaitForSeconds(0f);
            Phealth = 8;
        }


        if (hitted == true)
        {
           
           // render.material = hit;
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
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button3) && hasAttacked == false)
        {
            //hasAttacked = true;  
            switch (Random.Range(0, 5))
            {
                case 1:
                    anim.SetInteger("ATTACK", 1);
                    break;
                case 2:
                    anim.SetInteger("ATTACK", 2);
                    break;
                case 3:
                    anim.SetInteger("ATTACK", 3);
                    break;
                case 4:
                    anim.SetInteger("ATTACK", 4);
                    break;
                case 5:
                    anim.SetInteger("ATTACK", 5); 
                    break;
                case 6:
                    anim.SetInteger("ATTACK", 6);
                    break;
            }
            StartCoroutine(hastacked());
            Debug.Log("attacking");
            // choosing a random attack animation when the attack button is pressed.
        }
        
        if (Input.GetKeyUp(KeyCode.Joystick1Button3))
        {
            
            anim.SetInteger("ATTACK", 0);
          
        }
    }



IEnumerator stophitted()
    {
        yield return new WaitForSeconds(0.5f);
        hitted = false;
    }




  IEnumerator hastacked()
    {
        yield return new WaitForSeconds(0.75f);
        hasAttacked = false;
    }
    //private void OnCollisionEnter(Collision collision)
    //{
        
    //        if (collision.transform.CompareTag("S"))
    //        {
    //        Debug.Log("helllooooooooooo im gay");
    //            playHit = true;
    //        }
        
    //}



    //void hitting()
    //{
    //    if (Input.GetKeyDown(KeyCode.Joystick1Button3) && hasAttacked == false )
    //    {
    //        if (Physics.Raycast(transform.position + transform.up * 0.25f, transform.forward, out hitfront, Raylength))
    //        {

    //            if (hitfront.collider.gameObject.tag == "S")
    //            {
    //                hasAttacked = true;
    //                hitted = true;
    //                getSlime = hitfront.transform.gameObject.GetComponent<Combat_Slime>();
    //                getSlime.health -= 1;
    //                Debug.Log("hit");
    //                StartCoroutine(stophitted());
    //                StartCoroutine(hastacked());
    //            }
    //        }
    //    }


    //}

        IEnumerator delay()
    {
        yield return new WaitForSeconds(0.4f);
        //anim.SetInteger("ATTACK", 3);

        Collider[] hitenemy = Physics.OverlapSphere(attackpoint.position, attackrange, enemylayer);


        foreach (Collider enemy in hitenemy)
        {

            Debug.Log("we hit" + enemy.name);
            Debug.Log("attacked");
            enemy.GetComponent<Combat_Slime>().TakeDamage(Damage);
            
        }
    }
    void Newattack()
    {
       // anim.SetInteger("ATTACK", 3);

       //Collider[] hitenemy = Physics.OverlapSphere(attackpoint.position, attackrange, enemylayer);


       // foreach(Collider enemy in hitenemy)
       // {
            
       //    Debug.Log("we hit" + enemy.name);
       //    enemy.GetComponent<Combat_Slime>().TakeDamage(Damage);
       // }


    }
  void OnDrawGizmosSelected()
    {
        if (attackpoint == null)
            return;
        Gizmos.DrawWireSphere(attackpoint.position, attackrange);
    }
}
