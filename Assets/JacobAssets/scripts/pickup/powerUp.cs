using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour
{
    public GameObject effect;

    public GameObject pill;

    public GameObject icon;

    public Animator pillAnim;

    // Start is called before the first frame update
    private void Start()
    {
        pill = transform.GetChild(0).gameObject;

        icon = transform.GetChild(1).gameObject;

        pillAnim = pill.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //check if overlap is a pickup object
            if (other.CompareTag("Player"))
            {
                Pickup(other);
                Destroy(GetComponent<Collider>());
            }
            else if(other.CompareTag("enemy"))
            {
            PickupEnemy(other);
            Destroy(GetComponent<Collider>());
            }
    }

    public virtual void PickupEnemy(Collider enemy)
    {
        killRoutine();
    }

    public virtual void Pickup(Collider player)
    {
        //subclasses inherit and override this function for their purpose
        //generic collectables use this script and so their function is written here
        player.GetComponent<playerControlLerpSmoothAnim>().points++;
        killRoutine();
    }

    public void killRoutine()
    {
        Destroy(icon);

        Instantiate(effect, this.transform.position + new Vector3(0, 1, 0), pill.transform.rotation *= Quaternion.Euler(90, 0, 0));

        //play an animation
        pillAnim.SetTrigger("pickup");

        Destroy(gameObject, .5f);
    }
}
