using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plat_SWITCH : MonoBehaviour
{
    private RaycastHit P_hit;
    Platform_M getplat;
    Renderer render;
    public Transform switch_hit_point;
    public float hit_range;
    public LayerMask Switch;
    public bool ON = false;

    //public Material on;
    //public Material off;
    // Start is called before the first frame update
    void Start()
    {
        render = gameObject.GetComponent<Renderer>();
        getplat = GameObject.FindGameObjectWithTag("Platform").GetComponent<Platform_M>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("JosephButtonPress"))
        {
            p_press();
        }

        //if(ON == false)
        //{
        //    render.material = off;
        //}
        //if(ON == true)
        //{
        //    render.material = on;
        //}
    }
    void p_press()
    {
        Collider[] hitSwitch = Physics.OverlapSphere(switch_hit_point.position, hit_range, Switch);

            foreach (Collider Switch in hitSwitch)
            {

            getplat.P_Active = !getplat.P_Active;
            ON = !ON;

            }
           
        
    }
    void OnDrawGizmosSelected()
    {
        if (switch_hit_point == null)
            return;
        Gizmos.DrawWireSphere(switch_hit_point.position, hit_range);
    }
}
