using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEWCOL : MonoBehaviour
{

   private float TargetZoom = 5f,Zoom = 5f; //setting the zoom and target zoom
    private float ZoomSense = 20;


    private Vector2 zoomclamp = new Vector2(0.5f, 4f);

    private float ZoomVel = 0.5f;
    private float ZoomDamp = 0.1f;
    // Start is called before the first frame update
   
    void Update()
    {

        transform.localPosition = Vector3.back * Zoom; // always updating so we are zoom bakwards
        zoom(); // calling the zoom function
    }
    void zoom()
    {
        Ray ray = new Ray(transform.parent.position, -transform.parent.forward); // make a new ray cast at the postion of the parent, CAMBASE for me
        RaycastHit hit; // doing this ryacast hit thingy 

        if (Physics.Raycast(ray, out hit, TargetZoom)) // if the raycast hits at the position of the target zoom, then do this
            {
            Zoom = Mathf.SmoothDamp(Zoom, hit.distance, ref ZoomVel, ZoomDamp);// set the zoom position to the hit distance, which is where the base hits something, thus pushing it against the wall.
        }
        else
        {
            Zoom = Mathf.SmoothDamp(Zoom, TargetZoom, ref ZoomVel, ZoomDamp);// if you hit nothing then put the zoom distace back to the target zoom set amount, the zoom damp is how smoothyl it returens to this position
            
        }
        Zoom = Mathf.Clamp(Zoom, zoomclamp.x, zoomclamp.y); // updatte zoom and make sure it is restricted
        transform.localPosition = Vector3.back * Zoom; //make sure the local position is updated to the zoom position
    }
}
