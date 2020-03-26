using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerJump : powerUp
{
    public int change = 2;
  
    //overrides parent function of same name
    public override void Pickup(Collider player)
    {
        //get the script we want to edit
        playerControlLerpSmoothAnim stats = player.GetComponent<playerControlLerpSmoothAnim>();
        stats.jumpAltered = true;
        stats.maxJumps = change;

        killRoutine();
    }
}
