using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{

    public Slider healthbar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setmacheallth(int health)
    {
        healthbar.maxValue = health;
        healthbar.value = health;
    }

    public void health(int health)
    {
        healthbar.value = health;
    }
}
