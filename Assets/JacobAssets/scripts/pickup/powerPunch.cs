using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class powerPunch : powerUp
{

    public float change = 16;

    //overrides parent function of same name
    public override void Pickup(Collider player)
    {
        //the script we want to edit is in the parented object
        playerControlLerpSmoothAnim stats = player.GetComponent<playerControlLerpSmoothAnim>();
        stats.punchAltered = true;
        stats.topSpeed = change;

        killRoutine();
    }

    public override void PickupEnemy(Collider other)
    {
        GameObject enemyChild = other.transform.GetChild(0).gameObject;
        GameObject enemyChild2 = other.transform.GetChild(4).gameObject;

        other.GetComponentInParent<enemy>().health += 2;
        enemyChild.GetComponent<Renderer>().material.SetColor("_Color", new Color(1f, 0.47f, 0f, 0.5f));
        enemyChild2.GetComponent<Renderer>().material.SetColor("_Color", new Color(1f, 0.47f, 0f));
        killRoutine();
    }
}
