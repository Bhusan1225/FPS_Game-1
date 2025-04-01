using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //prefab
   
    public float enemySpeed;
   
    //public GameObject player;
    //private GameObject spawnedEnemy; // Reference to the spawned enemy
  
    public bool isGrounded;
    //public Transform groundCheck;
    public float groundDistance = 0.5f; // Radius of the checkSphere
    public LayerMask groundMask;

    public Animator animator;
    //private Ray ray;

    public enum EnemyType
    {
        RoboCrab,
        Mutant
    }

    public EnemyType enemyName;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>(); // Get the Animator component
        }
    }


    void Update()
    {
        
        groundCheck();
    }

     void groundCheck()
    {
        
        

            isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);

            if (isGrounded)
            {
                Debug.Log("Yes, grounded.");
                movetowords();
            }
        
    }

    private void movetowords()
    {
        Vector3 enemyPosition = transform.position;
        //Vector3 playerPosition = player.transform.position;
        Vector3 playerPosition = PlayerController.Instance.getplayerPosition();

        //playerController.GetComponent<Transform>().;
        Vector3 distance = (playerPosition - enemyPosition); //we get the direction
        Vector3 direction = distance.normalized;

       transform.LookAt(playerPosition); // the eyes will be towards the player

        enemyPosition += direction * enemySpeed * Time.deltaTime; //movement towards the player

        transform.position = enemyPosition; // Apply the new position 

        moveAnimation();
        
        
    }

    private void moveAnimation()
    {
        if (enemyName == EnemyType.Mutant)
        {
            animator.SetTrigger("CRAWLLING");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;


            // Draw CheckSphere at the position of the spawned enemy
            Gizmos.DrawWireSphere(transform.position, groundDistance);


    }
}
