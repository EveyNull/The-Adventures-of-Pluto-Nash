using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorOpenButton : Button
{
    private bool doorClosed = true;

    public GameObject doorCollapse;

    public DoorOpener door;
    public Transform doorLookAt;
    public GameObject buttonObject;

    public Light buttonLight;
    public ParticleSystem particles;

    public Transform playerStandAt;
    public Transform playerMoveTo;

    public Transform buttonCameraPos;
    public Transform doorCameraPos;

    public float pauseBeforeDoor = 2f;
    public float pauseAfterDoor = 2f;

    private bool wait = false;

    private void Start()
    {
        if (FindObjectOfType<SceneChangeData>().previousScene == 1)
        {
            door.SetDoorOpen();
            doorClosed = false;

        }

        RaycastHit hit;
        Physics.Raycast(playerStandAt.position + Vector3.up, Vector3.down, out hit);
        playerStandAt.position = hit.point;

        Physics.Raycast(playerMoveTo.position + Vector3.up, Vector3.down, out hit);
        playerMoveTo.position = hit.point;

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
        if (doorClosed)
        {
            StartCoroutine(DoorOpenCutscene());
        }
    }

    IEnumerator DoorOpenCutscene()
    {
        doorClosed = false;

        Camera camera = Camera.main;

        camera.enabled = false;

        GameObject obj = new GameObject();
        Camera tempCamera = obj.AddComponent<Camera>();

        tempCamera.transform.position = buttonCameraPos.position;
        tempCamera.transform.rotation = buttonCameraPos.rotation;
        playerControlLerpSmoothAnim player = FindObjectOfType<playerControlLerpSmoothAnim>();
        player.transform.position = playerStandAt.position;
        player.transform.rotation = playerStandAt.rotation;

        player.enabled = false;

        yield return new WaitForSeconds(0.2f);

        

        if (buttonLight)
        {
            buttonLight.color = Color.green;
        }
        if(particles)
        {
            particles.Play();
        }
        if(GetComponent<AudioSource>())
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
        while (Vector3.Distance(buttonObject.transform.position, original) > 0.1f)
        {
            buttonObject.transform.position = Vector3.MoveTowards(buttonObject.transform.position, original, Time.deltaTime * 4);
            yield return 0;
        }

        if(doorCollapse)
        {
            doorCollapse.GetComponent<Rigidbody>().isKinematic = false;
            yield return new WaitForSeconds(3f);
        }


        yield return new WaitForSeconds(0.5f);
        wait = true;
        StartCoroutine(MoveCamera(tempCamera, doorCameraPos));
        while (wait) yield return 0;
        

        door.StartDoorOpen();

        while (!door.complete)
        {
            yield return 0;
        }


        Destroy(tempCamera);

        camera.enabled = true;
        player.enabled = true;

        yield break;
    }

    IEnumerator MoveCamera(Camera camera, Transform target)
    {
        float startDistance = Vector3.Distance(camera.transform.position, target.position);
        Quaternion startRot = camera.transform.rotation;
        float y = 0f;
        while (Vector3.Distance(camera.transform.position, target.position) > 0.2f)
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, target.transform.position, y += 0.0005f);
            //camera.transform.position = Vector3.MoveTowards(camera.transform.position, target.transform.position, Time.deltaTime * 3);
            float x = 1 - Vector3.Distance(camera.transform.position, target.position) / startDistance;
            float lerpValue = 1 / (1 + Mathf.Exp((-12f * (x - 0.5f))));
            camera.transform.rotation = Quaternion.Slerp(startRot, target.transform.rotation, lerpValue);
            yield return 0;
        }
        wait = false;
    }

    IEnumerator RotateObjectToLookAt(Transform target, Transform lookAt)
    {
        Vector3 lookAtFlat = lookAt.position;
        lookAtFlat.y = 0f;
        Vector3 targetFlat = target.position;
        targetFlat.y = 0f;
        while (Vector3.Dot((lookAtFlat - targetFlat).normalized, target.transform.forward) < 0.99f)
        {
            targetFlat = target.position;
            targetFlat.y = 0f;
            Vector3 targetRotation = Vector3.RotateTowards(target.transform.forward, (lookAtFlat - targetFlat), Time.deltaTime * 2f, 0f);
            target.transform.rotation = Quaternion.LookRotation(targetRotation);
            yield return 0;
        }
        wait = false;
    }

    IEnumerator MovePlayerToPos(MoveScript player, Transform pos)
    {
        player.ForceMoveToLocation(pos.position);
        while(Vector3.Distance(player.transform.position, pos.position) > 0.1f) yield return 0;
        player.animator.SetFloat("z", 0f);
        wait = false;
    }
}
