using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POINTCOL : MonoBehaviour
{
    public bool hit = false;

    public Collider player;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        hit = !hit;
        if (other == player)
        {
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
