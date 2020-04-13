using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeData : MonoBehaviour
{
    public int previousScene = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectsOfType<SceneChangeData>().Length > 1)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }


}
