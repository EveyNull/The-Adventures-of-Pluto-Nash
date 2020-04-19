using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockUnlock : MonoBehaviour
{
    private Animator anim;
    public GameObject key;
    private static readonly int Unlock = Animator.StringToHash("unlock");

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    /**
     * if the player walks into the trigger radius and the key
     * related has been collected(removed from scene) then
     * unlock and destroy the object
     */
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (key) return;
        anim.SetTrigger(Unlock);
        Destroy(gameObject, 1f);
    }

}
