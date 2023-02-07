using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float Sensitivity = 1;

    private Vector3 currentRotation;

    // Update is called once per frame
    void Update()
    {
        //Get "strength" of horizontal and verical mouse movements
        currentRotation.x += Input.GetAxis("Mouse X") * Sensitivity;
        currentRotation.y -= Input.GetAxis("Mouse Y") * Sensitivity;

        //X rotation is looped based on 360 degrees
        currentRotation.x = Mathf.Repeat(currentRotation.x, 360);

        //Y is clamped so the camera never flips
        currentRotation.y = Mathf.Clamp(currentRotation.y, -90, 90);

        //rotate the player's view
        Camera.main.transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
    }
}
