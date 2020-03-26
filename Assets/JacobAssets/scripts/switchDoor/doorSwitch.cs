using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorSwitch : MonoBehaviour
{

    bool pressed = false;

    public bool isClosing = false;
    public GameObject Camera;
    public Transform newCamPos;
    camTransition camTrans;
    CameraLook camLook;

    public GameObject Player;
    playerControlLerpSmoothAnim PlayerScript;

    public GameObject Door;
    public GameObject Door2;
    Animator DoorAnim;
    Animator DoorAnim2;
    Animator ButtonAnim;
    Renderer ButtonRenderer;

    public GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        camTrans = Camera.GetComponent<camTransition>();
        camLook = Camera.GetComponent<CameraLook>();

        PlayerScript = Player.GetComponent<playerControlLerpSmoothAnim>();

        DoorAnim = Door.GetComponentInChildren<Animator>();
        DoorAnim2 = Door2.GetComponentInChildren<Animator>();
        ButtonAnim = gameObject.GetComponent<Animator>();
        ButtonRenderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(camTrans.transitionComplete)
        {
            camTrans.transitionComplete = false;
            StartCoroutine(Sequence2());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag("LeftFist"))
      {
         if (!pressed)
         {
                pressed = true;
                Instantiate(effect, other.transform.position, other.transform.rotation);
                ButtonAnim.SetTrigger("Pressed");
                ButtonRenderer.material.SetColor("_Color", Color.green);

                //PlayerScript.enabled = false;
                PlayerScript.inCutscene = true;
                PlayerScript.velocity = new Vector3(0, PlayerScript.velocity.y, 0);
                PlayerScript.anim.SetBool("Moving", false);
                camLook.enabled = false;

                StartCoroutine(Sequence1());
                
         }
        }
    }

    IEnumerator Sequence1()
    {
        yield return new WaitForSeconds(.5f);
        camTrans.camPos2 = newCamPos;
        camTrans.enabled = true;
        //remove this when solution found for camTransition problem
        yield return new WaitForSeconds(2f);
        StartCoroutine(Sequence2());
    }

    IEnumerator  Sequence2()
    {
        camTrans.enabled = false;
        DoorAnim.SetTrigger("Open");
        DoorAnim2.SetTrigger("Open");
        yield return new WaitForSeconds(3.1f);

        camTrans.enabled = false;
        camLook.enabled = true;
        //PlayerScript.enabled = true;
        PlayerScript.inCutscene = false;

        if (isClosing)
        {
            yield return new WaitForSeconds(10);
            ButtonRenderer.material.SetColor("_Color", Color.cyan);
            ButtonAnim.SetTrigger("Unpressed");
            DoorAnim.SetTrigger("Close");
            DoorAnim2.SetTrigger("Close");
            yield return new WaitForSeconds(.5f);
            pressed = false;
        }
    }
}
