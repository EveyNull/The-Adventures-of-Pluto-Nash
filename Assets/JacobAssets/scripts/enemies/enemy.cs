using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    public GameObject squirm;
    public GameObject particle;
    private Animator anim;
    public int formsLeft = 2;
    public int health = 3;
    public int maxHealth = 3;
    private enemyController controller;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<enemyController>();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Transform otherTrans = other.transform;
    //    if (other.CompareTag("LeftFist"))
    //    {
    //        Instantiate(particle, otherTrans.position, otherTrans.rotation);
    //        controller.hitBack = .4f;
    //        LoseHealth(1);
    //    }
    //    else if (other.CompareTag("Explosion"))
    //    {
    //        Instantiate(particle, otherTrans.position, otherTrans.rotation);
    //        LoseHealth(3);
    //    }
    //    else if(other.CompareTag("Player"))
    //    {
    //        controller.hitBack = .4f;
    //        other.GetComponent<playerControlLerpSmoothAnim>().Stun();

    //    }
    //}

    void LoseHealth(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            controller.isDead = true;
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        anim.SetTrigger("death");
        yield return new WaitForSeconds(1.16f);
        
        Vector3 pos = transform.position;

        if (formsLeft != 0)
        {
            SpawnAndScale(new Vector3(pos.x, pos.y, pos.z+1));
            SpawnAndScale(new Vector3(pos.x, pos.y, pos.z-1));
        }
        Destroy(gameObject);
    }

    private void SpawnAndScale(Vector3 pos)
    {
        Debug.Log("spawning enemy");
        GameObject spawn1 = Instantiate(squirm, pos, transform.rotation);
        
        spawn1.transform.localScale -= spawn1.transform.localScale / 2;
        enemy script = spawn1.GetComponent<enemy>();
        spawn1.GetComponent<enemyController>().hitBack = 0;
        script.formsLeft -= 1;

        if (script.formsLeft == 1)
        {
            script.health = maxHealth - 1;
        }
        else if (script.formsLeft == 0)
        {
            script.health = maxHealth - 2;
        }
        Debug.Log(script.health);
    }
}
