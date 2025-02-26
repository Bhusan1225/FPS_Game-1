using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 1000f;

    float xRotation = 0f;
    float yRotation = 0f;

    public float topClamp = -90f;
    public float bottomClamp = 90f;


    // Start is called before the first frame update
    void Start()
    {
        //Locking the cursor to the middle of the screen and making it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        //Getting the mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Rotation around the x axis (look up and down)
        xRotation -= mouseY; // this is for when you rotate in x axis it will minus(-) all the mouseY info

        //clamp the rotation 
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp); 

        //Rotation around the y axis (Look left and right)
        yRotation += mouseX;

        //Apply rotation to our transformation 
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
