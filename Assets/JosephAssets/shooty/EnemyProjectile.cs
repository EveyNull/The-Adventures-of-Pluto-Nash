using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public GameObject PROJ;
    public float timer = 0;
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
       

        timer += Time.deltaTime; 
        if(timer >= 5)
        {
            Instantiate(PROJ, transform.position, transform.rotation);
            timer = 0;
        }
    }
}
