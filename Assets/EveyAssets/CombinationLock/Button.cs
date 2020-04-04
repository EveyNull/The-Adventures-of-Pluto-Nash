using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Button : MonoBehaviour
{
    public Image interactImage;
    public abstract void HitButton();

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<playerControlLerpSmoothAnim>())
        {
            if (interactImage)
            {
                interactImage.enabled = true;
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<playerControlLerpSmoothAnim>())
        {
            if (interactImage)
            {
                interactImage.enabled = false;
            }
        }
    }
}
