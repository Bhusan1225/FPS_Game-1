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

    Vector3 jump;

    bool isGrounded;
    bool isMoving;

   Vector3 lastPosition = new Vector3(0f, 0f, 0f);


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        Move();
        Jump();

    }
    void Move()
    {
        //Getting the inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //create the moving vector
        Vector3 move = transform.right * x + transform.forward * z;

        //Actually moving the player 
        controller.Move(move * speed * Time.deltaTime);/////////////////////////////////////////////////// walk

        
    }

    void Jump()

    {
        //ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, grounddistance, groundMask);

        //resetting the default jump
        if (isGrounded && jump.y < 0f)
        {
            jump.y = -2f;
        }
        //Check if the player can jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jump.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        }

        //falling down
        jump.y += gravity * Time.deltaTime;

        //execution of the jump 
        controller.Move(jump * Time.deltaTime);//////////////////////////////////////////////////////////// jump

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