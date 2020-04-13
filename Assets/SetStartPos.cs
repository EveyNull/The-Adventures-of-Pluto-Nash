using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStartPos : MonoBehaviour
{
    [System.Serializable]
    public class startPositionBySceneID
    {
        public int sceneID;
        public Transform startPosition;
    }

    public List<startPositionBySceneID> startPositions;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPos = FindStartPosFromID(FindObjectOfType<SceneChangeData>().previousScene);
        transform.position = startPos;
        transform.GetChild(0).localPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 FindStartPosFromID(int id)
    {
        foreach(startPositionBySceneID startPos in startPositions)
        {
            if(startPos.sceneID == id)
            {
                return startPos.startPosition.position;
            }
        }
        return Vector3.zero;
    }
}
