using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationLockButton : Button
{
    public Transform buttonCameraPos;
    public Transform playerStandAt;
    public GameObject buttonObject;

    public CombinationLock combinationLock;

    private bool buttonPressing;
    private playerControlLerpSmoothAnim player;

    private void Start()
    {
        RaycastHit hit;
        Physics.Raycast(playerStandAt.position, Vector3.down, out hit);
        playerStandAt.position = hit.point;
        player = FindObjectOfType<playerControlLerpSmoothAnim>();
    }

    protected override void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("LeftFist"))
        {
            HitButton();
        }
    }

    public override void HitButton()
    {
        if (!combinationLock.opened && !buttonPressing)
        {
            buttonPressing = true;
            StartCoroutine(PressButton());
        }
    }

    IEnumerator PressButton()
    {
        Camera camera = Camera.main;

        camera.enabled = false;

        GameObject obj = new GameObject();
        Camera tempCamera = obj.AddComponent<Camera>();


        tempCamera.transform.position = buttonCameraPos.position;
        tempCamera.transform.rotation = buttonCameraPos.rotation;

        player.transform.position = playerStandAt.position;
        player.transform.rotation = playerStandAt.rotation;

        player.enabled = false;
        GetComponentInChildren<ParticleSystem>().Play();
        if (GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().Play();
        }
        Vector3 insideWall = buttonObject.transform.position + (transform.forward * 0.2f);
        Vector3 original = buttonObject.transform.position;

        while (Vector3.Distance(buttonObject.transform.position, insideWall) > 0.1f)
        {
            buttonObject.transform.position = Vector3.MoveTowards(buttonObject.transform.position, insideWall, Time.deltaTime * 4);
            yield return 0;
        }
        while (Vector3.Distance(buttonObject.transform.position, original) > 0f)
        {
            buttonObject.transform.position = Vector3.MoveTowards(buttonObject.transform.position, original, Time.deltaTime * 4);
            yield return 0;
        }

        yield return new WaitForSeconds(0.5f);

        bool complete = combinationLock.PressedButton(this);

        if (complete)
        {
            tempCamera.transform.position = combinationLock.doorLook.position;
            tempCamera.transform.rotation = combinationLock.doorLook.rotation;
        }
        else
        {
            tempCamera.transform.position = combinationLock.lightLook.position;
            tempCamera.transform.rotation = combinationLock.lightLook.rotation;
        }

        yield return new WaitForSeconds(1f);

        Destroy(tempCamera);

        camera.enabled = true;
        player.enabled = true;

        buttonPressing = false;
    }
}
