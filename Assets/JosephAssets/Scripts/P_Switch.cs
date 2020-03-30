using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Switch : MonoBehaviour
{
    Plat_SWITCH getplatS;

    Renderer render;
    public Material on;
    public Material off;
    // Start is called before the first frame update
    void Start()
    {
        render = gameObject.GetComponent<Renderer>();
        getplatS =  GameObject.FindGameObjectWithTag("Player").GetComponent<Plat_SWITCH>();  
    }

    // Update is called once per frame
    void Update()
    {
        if(getplatS.ON == false)
        {
            render.material = off;
        }
        if(getplatS.ON == true)
        {
            render.material = on;
        }
        
    }
}
