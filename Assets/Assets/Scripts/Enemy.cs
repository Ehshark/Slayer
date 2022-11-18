using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spr;
    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private float attack;

    [SerializeField] private Transform playerLocation;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        speed = 1f;
        health = 10f;
        attack = 1f;
    }

    private void FixedUpdate()
    {
        Vector3 direction = (playerLocation.position - transform.position).normalized; //enemy chases player's current location
        rb.velocity = direction * speed;

        spr.flipX = (direction.x < 0); //flipping the sprite
    }

    //When enemy touches player, cause the player is take damage equal to enemy's attack
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(attack);
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<PlayerController>().TakeDamage(attack);
        }
    }

    //when enemy is hit by player's attack, this function is called, passes in player's attack
    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
