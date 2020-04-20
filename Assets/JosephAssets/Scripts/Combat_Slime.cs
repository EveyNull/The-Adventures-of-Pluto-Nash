using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Combat_Slime : MonoBehaviour
{
   public int health = 3;
    public int maxhealth = 3;
    public GameObject stage2;

    public ParticleSystem Dead;
   public NavMeshAgent NavMesh;
    GameObject player;
    public HealthBar helathb;
    Rigidbody rb;
    public GameObject squirm;
    public Animator anim;
    public int FormsLeft = 2;
    Renderer render;
    public Material hit;
    public Material norm;
    private enemyController controller;
    public GameObject particle;
    public int Damage = 1;
    Attacking getat;
    // Start is called before the first frame update
    void Start()
    {
    
       rb = gameObject.GetComponent<Rigidbody>();
        render = gameObject.GetComponent<Renderer>();
        anim = gameObject.GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        NavMesh = GetComponent<NavMeshAgent>();
        controller = GetComponent<enemyController>();
        getat = GameObject.FindGameObjectWithTag("Player").GetComponent<Attacking>();
    }

    public void TakeDamage(int damage)
    {

        health -= damage;
        
        StartCoroutine(hitback());
    }

    // Update is called once per frame
    void Update()
    {

        die();
        helathb.health(health);
    }
    
IEnumerator deathdelay()
    {


        // NavMesh.isStopped = true;
        // yield return new WaitForSeconds(1f);

        //NavMesh.isStopped = false;
        //Instantiate(Dead, transform.position, Quaternion.identity);

        // gameObject.SetActive(false);

        // for (int i = 0; i < 2; i++)
        // {

        // Instantiate(stage2, transform.position, transform.rotation);
        // }
        anim.SetTrigger("death");
        yield return new WaitForSeconds(1.16f);

        Vector3 pos = transform.position;

        if (FormsLeft != 0)
        {
            SpawnAndScale(new Vector3(pos.x, pos.y, pos.z + 1));
            SpawnAndScale(new Vector3(pos.x, pos.y, pos.z - 1));
        }
        Destroy(gameObject);
    }


    private void SpawnAndScale(Vector3 pos)
    {
        Debug.Log("spawning enemy");
        GameObject spawn1 = Instantiate(squirm, pos, transform.rotation);

        spawn1.transform.localScale -= spawn1.transform.localScale / 2;
        Combat_Slime script = spawn1.GetComponent<Combat_Slime>();
        spawn1.GetComponent<enemyController>().hitBack = 0;
        script.FormsLeft -= 1;

        if (script.FormsLeft == 1)
        {

             script.health = maxhealth- 1;
          
        }
        else if (script.FormsLeft == 0)
        {
           
            script.health = maxhealth - 2;
         
        }
        Debug.Log(script.health);
    }



    void die()
    {
     
        if(health == 0)
        {
        
            StartCoroutine(deathdelay());
             
        }
    }

 
    IEnumerator hitback()
    {
        rb.AddForce(player.transform.forward * 600);
        rb.detectCollisions = false;
   
        yield return new WaitForSeconds(0.25f);

      rb.detectCollisions = true;
        rb.velocity = Vector3.zero;
    }
    private void OnTriggerEnter(Collider other)
    {
        Transform otherTrans = other.transform;
        if (other.CompareTag("LeftFist"))
        {
            Instantiate(particle, otherTrans.position, otherTrans.rotation);
            controller.hitBack = .4f;
         
        }
        else if (other.CompareTag("Explosion"))
        {
            Instantiate(particle, otherTrans.position, otherTrans.rotation);
       
        }
        else if (other.CompareTag("Player"))
        {
            controller.hitBack = .4f;
            other.GetComponent<playerControlLerpSmoothAnim>().Stun();
            other.GetComponent<Attacking>().TakeDamage(Damage);

        }
    }
}
