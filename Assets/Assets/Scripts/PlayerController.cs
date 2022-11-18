using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector3 movementVector;

    //player's combat stats
    [SerializeField] private float speed = 5f;
    [SerializeField] private float health = 5f;
    [SerializeField] private float attack = 1f;

    //player's attack variables
    private Vector3 directionFacing; //last direction player was facing
    [SerializeField] private float attackRangeX = 1f; //x and y used to draw the range overlap box
    [SerializeField] private float attackRangeY = 1f;
    [SerializeField] private float attackInterval = 1f; //how fast the player can attack
    [SerializeField] private float attackTimer = 1f; //player's current attack cooldown

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
            directionFacing = movementVector;
        }
        
        Attack();
    }

    //player basic attack function
    private void Attack()
    {
        attackTimer -= Time.deltaTime; //player's attack on cooldown

        //automatically attack
        if (attackTimer <= 0)
        {
            //attack in the direction player is currently facing
            Collider2D[] enemiesHit = Physics2D.OverlapBoxAll(transform.position + directionFacing, new Vector2(attackRangeX, attackRangeY), 0);

            foreach (Collider2D enemy in enemiesHit)
            {
                if (enemy.tag == "Enemy")
                {
                    Debug.Log(enemy.name + " hit");
                    enemy.gameObject.GetComponent<Enemy>().TakeDamage(attack);
                }
            }

            attackTimer = attackInterval;

            this.gameObject.transform.GetChild(1).transform.position = transform.position + directionFacing;
            //this.gameObject.transform.GetChild(1).transform.rotation = Quaternion.LookRotation(directionFacing, new Vector3(0,0,1));
            this.gameObject.transform.GetChild(1).GetComponent<Animator>().SetTrigger("attacking");

            this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().flipX = (directionFacing.x < 0);
        }
    }

    //represents attack range for debugging purposes
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + directionFacing, new Vector3(attackRangeX, attackRangeY, 0));
    }

    //When player collides with enemy, this function is called, passes in enemy's attack
    public void TakeDamage(float damage)
    {
        health -= damage; 
    }
}
