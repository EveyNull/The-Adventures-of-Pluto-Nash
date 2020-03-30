using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NEWS_MOVE : MonoBehaviour
{
    Attacking getattack;

  public NavMeshAgent NavMesh;

    public float Timer = 3f;

    bool intimer = false;

    public Material angry;

    public Material norm;

    public GameObject Player;

     GameObject PLAYER;

     public float To_Player_Speed = 2;

    public bool jump;

    public int speed = 10;

    Rigidbody Rb;
   // public Animator anim;
     public bool fight;

    Combat_Slime getcom;

    Renderer render;
    // Start is called before the first frame update
    void Start()
    {
       PLAYER = GameObject.FindGameObjectWithTag("Player");
        getattack = GameObject.FindGameObjectWithTag("Player").GetComponent<Attacking>();
        NavMesh = GetComponent<NavMeshAgent>();
        Rb = GetComponent<Rigidbody>();
        render = GetComponent<Renderer>();
        // Rb.velocity = Vector3.zero;
        NavMesh.isStopped = true;
        NavMesh.speed = 8;
       NavMesh.SetDestination(RandoPos());
        //anim = GetComponent<Animator>();
        getcom = GameObject.FindGameObjectWithTag("S").GetComponent<Combat_Slime>();
    }

    // Update is called once per frame
    void Update()
    {
        


        if (!intimer && fight == false)
        {
            render.material = norm;
            StartCoroutine(timer());
        }

        if(fight == true)
        {
           
            render.material = angry;
           NavMesh.SetDestination(PLAYER.transform.position);
           // StartCoroutine(JUMPtoPlayer());
            //attacking();
        }
        if(!intimer)
        {
            // newPath();
            StartCoroutine(timer());
        }

        if(getattack.playHit)
        {
            NavMesh.isStopped = true;
            StartCoroutine(unpause());
        }

       

    }



    IEnumerator unpause()
    {
        yield return new WaitForSeconds(1);
        NavMesh.isStopped = false;
        getattack.playHit = false;
    }

    Vector3 RandoPos()
    {
        NavMesh.isStopped = false;
        float x = Random.Range( 4 , 44);
        float z = Random.Range(65,105);

        Vector3 pos = new Vector3(x, 0, z);
        return pos;
    }

IEnumerator timer()
    {
        intimer = true;
        yield return new WaitForSeconds(Timer);
        newPath();
        intimer = false;
    }

    IEnumerator JUMPtoPlayer()
    {
        yield return new WaitForSeconds(2);
        NavMesh.speed = 500;
        jump = true;
        StartCoroutine(Stopjump());
    }


    IEnumerator Stopjump()
    {
        yield return new WaitForSeconds(0.5f);     
        jump = false;
        NavMesh.speed = 2;
    }

    void newPath()
    {
        NavMesh.SetDestination(RandoPos());
    }


   
 
    void OnTriggerStay(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            fight = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            fight = false;
        }
    }

    void attacking()
    {
        if(jump == true)
        {
            NavMesh.speed = 50 * Time.deltaTime;
       
            StartCoroutine(Stopjump());
            
        }
    }
    IEnumerator hitback()
    {
        Rb.AddForce(-transform.forward * 600f);
        Rb.detectCollisions = false;
        // render.material = hit;
        yield return new WaitForSeconds(0.25f);
        // render.material = norm;
        Rb.detectCollisions = true;
        Rb.velocity = Vector3.zero;
    }

}
