using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector2 movementVector;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float health = 5f;



    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        //movement
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");

        movementVector.Normalize(); //normalize to prevent diagonal movement being faster
        rb.velocity = movementVector * speed;
        
        GetComponentInChildren<PlayerAnimation>().playerVelocity = rb.velocity;
    }

    //When player collides with enemy, this function is called, passes in enemy's attack
    public void TakeDamage(float damage)
    {
        health -= damage; 
    }
}
