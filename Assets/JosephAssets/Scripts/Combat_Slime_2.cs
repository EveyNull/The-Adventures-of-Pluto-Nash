using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_Slime_2 : MonoBehaviour
{
    public int health = 2;
    public GameObject stage3;
    Attacking getattack;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        die();
        if(health == 0)
        {
            getattack.slime2 = false;
        }
    }

    void die()
    {
        if (health == 0)
        {
         
            Destroy(gameObject);
            for (int i = 0; i < 2; i++)
            {
                Instantiate(stage3, transform.position, transform.rotation);
                
            }
           
        }
    }
}
