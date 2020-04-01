using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class oldPosUpdate : MonoBehaviour
{
    public GameObject topic;
    private Transform topicTrans;  
    private float countdown = .5f;
    private bool grounded = true;
    playerControlLerpSmoothAnim playerScript;

    PathCreator pathCreator;

    // Start is called before the first frame update
    void Start()
    {
        topicTrans = topic.transform;
        playerScript = topic.GetComponent<playerControlLerpSmoothAnim>();
    }

    // Update is called once per frame
    void Update()
    {
        if (topic == null)
        {
            Debug.Log("resetting to player");
            SwapTopic(playerScript.gameObject);  
            playerScript.inCutscene = false;
        }
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.35f, transform.position.z);
        transform.position = Vector3.Lerp(pos, topicTrans.position, Time.deltaTime * 8);

        if (playerScript.path != null) //consider (!playerScript.path)
        {
            pathCreator = playerScript.path;
            transform.rotation = pathCreator.path.GetRotationAtDistance(playerScript.pathPosition, playerScript.eOPinstruction);
        }
    }

    public void SwapTopic(GameObject target)
    {
        topic = target;
        topicTrans = topic.transform;
    }
}
