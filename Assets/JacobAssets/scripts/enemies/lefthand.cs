using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lefthand : MonoBehaviour
{
    public Vector3 sizeIncrease = new Vector3(1, 1, 1);
    public GameObject player;
    private playerControlLerpSmoothAnim playerScript;

    new SphereCollider collider;
    GameObject upgradeFist;
    bool punching = false;

    public bool punchUpgraded;


    // Start is called before the first frame update
    void Start()
    {
        collider = this.GetComponent<SphereCollider>();
        //playerAnim = player.GetComponent<Animator>();
        playerScript = player.GetComponent<playerControlLerpSmoothAnim>();
        upgradeFist = transform.GetChild(5).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //check if attack is used and aren't currently punching
        if (Input.GetButtonDown("Fire1") && !punching)
        {
                punching = true;
                StartCoroutine(Timer());
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.3f);

        if (punchUpgraded) upgradeFist.SetActive(true);
        else collider.enabled = true;

        gameObject.transform.localScale += sizeIncrease;

        yield return new WaitForSeconds(0.2f);

        if (punching)
        {
            EndPunch();
        }
    }

    void EndPunch()
    {
        gameObject.transform.localScale -= sizeIncrease;
        collider.enabled = false;
        upgradeFist.SetActive(false);
        punching = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(punching)
            if(other.CompareTag("world"))
            {
                playerScript.Stun();
                EndPunch();
            }
    }
}
