using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PROJmovement : MonoBehaviour
{
    GameObject player;
    public int Speed = 5;
    Vector3 PlayerPos;
    public bool KeepGoing = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPos = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z);

       

        if (KeepGoing == false)
        {
            transform.LookAt(PlayerPos);
            towardsPlayer();
        }
        if(KeepGoing == true)
        {
            Foward();
        }
    }
    void towardsPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, PlayerPos, Speed * Time.deltaTime);
    }
    void Foward()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            KeepGoing = true;
        }
    }
}
