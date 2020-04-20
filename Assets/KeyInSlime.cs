using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInSlime : MonoBehaviour
{
    public GameObject Slime;
    GameObject slime;
    Vector3 slimeman;
    Combat_Slime com;

    // Start is called before the first frame update
    void Start()
    {
        slime = GameObject.FindGameObjectWithTag("enemy");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {



        if (slime)
        {
            slimeman = new Vector3(Slime.transform.position.x, Slime.transform.position.y + 2, Slime.transform.position.z);
            transform.position = slimeman;
        }

    }
}
