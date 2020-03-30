using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressed : MonoBehaviour
{
    private Transform TargetPOS;
    public float speed = 10;
    public Switch GetSwitch;
    public bool has_PRESSED = true;
    // Start is called before the first frame update
    void Start()
    {
        TargetPOS = GameObject.FindGameObjectWithTag("B_ENDPOS").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
       // Pressed();
    }
    public void Pressed()
    {
        has_PRESSED = true;
       
        if (GetSwitch.Switch_Pressed == true)
        {
             float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, TargetPOS.position,speed * Time.deltaTime);
 
            Debug.Log("HERRRORRORROROORRORORRO");
        }
    }
}
