using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed_Pickup : MonoBehaviour
{
    public float boost = 4.0f;
    public bool Speed_Active = false;
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
        if(other.CompareTag("Player"))
        {
            StartCoroutine(SpeedBoost(other));
        }
    }

    IEnumerator SpeedBoost(Collider player)
    {
        Movement Boosted = player.GetComponent<Movement>();
        Boosted.Speed *= boost;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        Speed_Active = true;
        yield return new WaitForSeconds(3.0f);
        Speed_Active = false;
        Boosted.Speed /= boost;
        Destroy(gameObject);
    }
}
