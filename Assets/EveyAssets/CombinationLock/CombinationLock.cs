using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationLock : MonoBehaviour
{
    public Light[] doorLights = new Light[4];

    public AudioSource correct;
    public AudioSource incorrect;

    public DoorOpener door;

    public Transform doorLook;
    public Transform lightLook;

    [SerializeField]
    private bool[] locks = new bool[4];
    private List<CombinationLockButton> buttonsPressed;
    public bool mustBeInOrder = false;
    public bool opened = false;

    public CombinationLockButton[] buttonOrder = new CombinationLockButton[4];

    private void Start()
    {

        if (!mustBeInOrder)
        {
            buttonsPressed = new List<CombinationLockButton>();
        }
        if (FindObjectOfType<SceneChangeData>().previousScene == 2)
        {
            foreach(Light light in doorLights)
            {
                light.color = Color.green;
            }
            door.SetDoorOpen();
            opened = true;
            
        }
        else
        {
            for (int i = 0; i < locks.Length; i++)
            {
                locks[i] = false;
            }
        }
    }

    public bool PressedButton(CombinationLockButton buttonPressed)
    {
        for(int i = 0; i < locks.Length; i++)
        {
            bool unlockLock = false;
            if(locks[i])
            {
                continue;
            }
            
            if(!mustBeInOrder && !buttonsPressed.Contains(buttonPressed))
            {
                buttonsPressed.Add(buttonPressed);
                unlockLock = true;
            }

            if(buttonOrder[i] == buttonPressed)
            {
                unlockLock = true;
            }

            if(unlockLock)
            {
                locks[i] = true;
                doorLights[i].color = Color.green;
                correct.Play();
                if (i == locks.Length - 1)
                {
                    door.StartDoorOpen();
                    opened = true;
                    return true;
                }
            }
            else
            {
                incorrect.Play();
                if (mustBeInOrder)
                {
                    ResetAllLocks();
                }
            }
            break;
        }
        return false;
    }

    private void ResetAllLocks()
    {
        for (int i = 0; i < locks.Length; i++)
        {
            locks[i] = false;
            doorLights[i].color = Color.red;
        }
    }
}
