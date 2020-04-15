using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floater : MonoBehaviour
{
    private bool is_rising = false;
    public float level_of_movement = 1f;
    public float rate_of_floating = 1.5f;
    private float timer;

    private Vector3 target_pos_lower;
    private Vector3 target_pos_higher;

    private void Start()
    {
        Vector3 default_pos = transform.position;
        target_pos_lower = new Vector3(default_pos.x, default_pos.y - level_of_movement, default_pos.z);
        target_pos_higher = new Vector3(default_pos.x, default_pos.y + level_of_movement, default_pos.z);
        
        timer = rate_of_floating;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            timer = rate_of_floating;
            is_rising = !is_rising; // flip flop bool
        }
        else timer -= Time.deltaTime;

        if (is_rising)
        {
            transform.transform.position = Vector3.Lerp(transform.position,
                target_pos_higher, Time.deltaTime);
        }
        else //is rising is false
        {
            transform.transform.position = Vector3.Lerp(transform.position,
                target_pos_lower, Time.deltaTime);
        }
        
    }
}
