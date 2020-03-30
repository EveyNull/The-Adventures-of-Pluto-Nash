using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Combat_Slime : MonoBehaviour
{
   public int health = 3;
    public int health2 = 2;
    public GameObject stage2;
    public int stage;
  public Animator anim;
    public ParticleSystem Dead;
   public NavMeshAgent NavMesh;
    GameObject player;
    public HealthBar helathb;
    Rigidbody rb;

   // public GameObject player;

    Renderer render;
    public Material hit;
    public Material norm;

    // Start is called before the first frame update
    void Start()
    {
        anim.enabled = false;
       rb = gameObject.GetComponent<Rigidbody>();
        render = gameObject.GetComponent<Renderer>();
       anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        NavMesh = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(int damage)
    {

        health -= damage;
        helathb.health(health);
        
       StartCoroutine(hitback());
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(deathdelay());
        die();
    }
    
IEnumerator deathdelay()
    {
        anim.enabled = true;
        anim.Play("death");
        NavMesh.isStopped = true;
        yield return new WaitForSeconds(1f);
        anim.enabled = false;
        NavMesh.isStopped = false;
        Instantiate(Dead, transform.position, Quaternion.identity);
        stage = +1;
        gameObject.SetActive(false);
      
        for (int i = 0; i < 2; i++)
        {

            Instantiate(stage2, transform.position, transform.rotation);
        }
    }



    void die()
    {
     
        if(health == 0)
        {
           // anim.Play("death");
          //  NavMesh.isStopped = true;
            StartCoroutine(deathdelay());
           // Instantiate(Dead, transform.position, Quaternion.identity);
           // stage =+1;
           //gameObject.SetActive(false);
            
           // for (int i = 0; i < 2; i++)
           // {

           //         Instantiate(stage2, transform.position, transform.rotation);                      
           // }       
        }
    }

    IEnumerator colour()
    {
        render.material = hit;
        yield return new WaitForSeconds(2f);
        render.material = norm;
    }
    IEnumerator hitback()
    {
        rb.AddForce(player.transform.forward * 600f);
        rb.detectCollisions = false;
   
        yield return new WaitForSeconds(0.25f);

      rb.detectCollisions = true;
        rb.velocity = Vector3.zero;
    }

}
