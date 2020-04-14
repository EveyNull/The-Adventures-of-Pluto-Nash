using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTimer : MonoBehaviour
{
    public GameObject spike;
    public float cooldown_timer_length = 4f;
    public float spike_active_time = 1f;
    private bool spike_active;
    private float timer = 4f;
    // Start is called before the first frame update
    void Start()
    {
        spike.SetActive(false);
        spike_active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            Debug.Log("timer ended");
            if (spike_active)
            {
                spike.SetActive(false);
                spike_active = false;
                timer = cooldown_timer_length;
            }
            else
            {
                spike.SetActive(true);
                spike_active = true;
                timer = spike_active_time;
            }
        }
        else
        {
            timer -= Time.deltaTime;
            Debug.Log("timer decreasing");
        }
    }
}
