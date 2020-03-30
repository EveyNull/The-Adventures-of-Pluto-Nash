using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEW_CAM : MonoBehaviour
{
    public Transform PLAYER; // setting the cameras transform around the player
   
    private float targetAngle = 0;//starting angle of which we chnage when we spin around the chracter
    const float ROT_AMOUNT = 3f; // the speed at which we rotate




    public bool BEEN_PRESSED; // a boolean that turns true when a button is pressed





    public Camera maincamera; // setting the camera as a object that i can acess
    public GameObject player; // setting the player as a game obect



    
    void Update()
    {




        // Trigger functions if Rotate is requested
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.JoystickButton4) && BEEN_PRESSED == false || Input.GetAxis("right_joyhori") <0 && BEEN_PRESSED == false)// when the bumper or the arros are pressed activate this peice of code
        {
     
            BEEN_PRESSED = true; // turn this to true
            targetAngle -= 90.0f; //  trake 90 away from the target angle
            StartCoroutine(PRESSED()); // start the couritine "PRESSED"

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.JoystickButton5) && BEEN_PRESSED == false || Input.GetAxis("right_joyhori") > 0 && BEEN_PRESSED == false)
        {
         
            BEEN_PRESSED = true;
            targetAngle += 90.0f; // add 90 onto the target angle.
            StartCoroutine(PRESSED());
        }


        Rotate(); // call the rotate function.



        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 4, player.transform.position.z); // keep updating the cameras position to the player postion
       transform.position += transform.forward * -2; // offset the cameras postion so it is constantly behind the player, no matter the roation 
    }
    IEnumerator PRESSED()
    {
        yield return new WaitForSeconds(0.5f); // wait for five seconds
        BEEN_PRESSED = false; // turned been pressed to true, this allows for a button to be presses every half a second so you can stack the button presses.
     
    }

  

    protected void Rotate()
    {

    
       

        if (targetAngle > 0)
        {
            transform.RotateAround(PLAYER.transform.position, Vector3.up , -ROT_AMOUNT);// rotate arounf the player 
            targetAngle -= ROT_AMOUNT; //take 90 away from the target angle so we are at - 90 dgrees on each button press;
      


        }
        else if (targetAngle < 0)
        {
            transform.RotateAround(PLAYER.transform.position, Vector3.up, ROT_AMOUNT);
            targetAngle += ROT_AMOUNT;
     
        }

        

    }
}
