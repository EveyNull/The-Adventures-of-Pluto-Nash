using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushButtonToDamageOrb : MonoBehaviour
{
    public GameObject orb;
    public GameObject explosion;
    private Renderer buttonRenderer;
    [SerializeField] private bool pressed = false;

    private void Start()
    {
        buttonRenderer = gameObject.GetComponent<Renderer>();
        buttonRenderer.material.SetColor("_Color", Color.red);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("LeftFist")) return;
        if (pressed) return;
        pressed = true;
        buttonRenderer.material.SetColor("_Color", Color.green);
        Instantiate(explosion, orb.transform.position, orb.transform.rotation);
        orb.GetComponent<BossDeath>().ApplyDamage();
    }
}
