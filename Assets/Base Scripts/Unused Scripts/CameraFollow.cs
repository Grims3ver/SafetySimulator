using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform playerTarget;
    public float smoothness = 0.2f; //the higher the value, the faster the camera
    public Vector3 offset; //offset value for each axis
    private Vector3 velocity = Vector3.zero; //zero vector, it is adjusted automatically

    void LateUpdate() //run directly after update to make sure that the target moves PRIOR to the camera moving
    {

        Vector3 desiredCameraPosition = playerTarget.position + offset; 
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredCameraPosition, ref velocity, smoothness); //smooth the camera
        transform.position = smoothedPosition;  //move the camera's position (transform.position) to the player's current position 
      
    }

   
}
