using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector3 movementVector;

    //player's stats
    [SerializeField] private float speed = 2f;
    [SerializeField] private float maxHealth = 5f;
    [SerializeField] private float health = 5f;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //movement
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");

        //movementVector.Normalize(); //normalize to prevent diagonal movement being faster
        rb.velocity = movementVector.normalized * speed;
        
        GetComponentInChildren<PlayerAnimation>().playerVelocity = rb.velocity;

        //change the direction the player is facing only if they're moving
        if(movementVector != Vector3.zero)
        {
            GetComponentInChildren<PlayerAttack>().directionFacing = movementVector;
        }
    }

    //When player collides with enemy, this function is called, passes in enemy's attack
    public void TakeDamage(float damage)
    {
        health -= damage; 
        
        if(health <= 0)
        {
            //player dies, lose game
        }
    }
}
