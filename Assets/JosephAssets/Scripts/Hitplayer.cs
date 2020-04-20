using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitplayer : MonoBehaviour
{

    Attacking getat;

    public Material angry;
   // public Material norm;
    public Material hit;
    Rigidbody rb;
   
    Renderer render;

    NEWS_MOVE getmove;

    public HealthBar phealthb;
    // Start is called before the first frame update


    // Start is called before the first frame update
    void Start()
    {
        render = gameObject.GetComponentInParent<Renderer>();
        rb = gameObject.GetComponentInParent<Rigidbody>();
        getmove = gameObject.GetComponent<NEWS_MOVE>();
        getat = GameObject.FindGameObjectWithTag("Player").GetComponent<Attacking>();
    }

    // Update is called once per frame
    void Update()
    {
      
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
       
            Debug.Log("hate");
            getat.Phealth -= 1;
        }
    
   
        
    }
  
}
