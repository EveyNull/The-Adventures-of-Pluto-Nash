using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public int sceneToID;
    private void OnTriggerEnter (Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            FindObjectOfType<SceneChangeData>().previousScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(sceneToID);
        }
    }
}
