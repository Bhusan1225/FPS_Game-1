using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.8f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float grounddistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;
    bool isMoving;

    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, grounddistance, groundMask);

        //resetting the default velocity
        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }


        //Getting the inputs
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        //create the moving vector
        Vector3 move = transform.right * x + transform.forward * y;

        //Actually moving the player 
        controller.Move(move * speed * Time.deltaTime);/////////////////////////////////////////////////// walk

        //Check if the player can jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        }

        //falling down
        velocity.y += gravity * Time.deltaTime;

        //execution of the jump 
        controller.Move(velocity * Time.deltaTime);//////////////////////////////////////////////////////////// jump

        if (lastPosition != gameObject.transform.position && isGrounded == true)

        {
            isMoving = true; // the player is moving
        }
        else
        {
            isMoving = false; // the player is standing
        }

        lastPosition = gameObject.transform.position; // this will continously update the new position of the player as the last position
    }
}