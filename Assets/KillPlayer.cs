using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
     playerControlLerpSmoothAnim getP;
    Attacking getA;
    public Transform spawn;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        getP = GameObject.FindGameObjectWithTag("Player").GetComponent<playerControlLerpSmoothAnim>();
        getA = GameObject.FindGameObjectWithTag("Player").GetComponent<Attacking>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
          //  getA.Phealth = 0;
           // getP.respawn();
           // player.transform.position = spawn.transform.position;
        }
    }
}
