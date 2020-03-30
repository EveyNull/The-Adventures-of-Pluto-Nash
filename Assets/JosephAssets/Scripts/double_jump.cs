using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class double_jump : MonoBehaviour
{

    public bool D_Jump_active = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DoubleJump(other));
        }
    }

    IEnumerator DoubleJump(Collider player)
    {
        Movement jump = player.GetComponent<Movement>();
        jump.jumpbool = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(3);
        jump.jumpbool = false;
        Destroy(gameObject);
    }

}

