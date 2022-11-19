using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{   
    private Animator anim;

    //player's combat stats
    [SerializeField] private float attack = 1f;


    public Vector3 directionFacing; //last direction player was facing
    [SerializeField] private Transform playerLocation; //player's current position

    [SerializeField] private float attackRangeX = 2f; //x and y used to draw the range overlap box
    [SerializeField] private float attackRangeY = 2f;
    [SerializeField] private float attackInterval = 1f; //how fast the player can attack
    [SerializeField] private float attackTimer = 1f; //player's current attack cooldown

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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
            Collider2D[] enemiesHit = Physics2D.OverlapBoxAll(playerLocation.position + directionFacing, new Vector2(attackRangeX, attackRangeY), 0);

            foreach (Collider2D enemy in enemiesHit)
            {
                if (enemy.tag == "Enemy")
                {
                    enemy.gameObject.GetComponent<Enemy>().TakeDamage(attack);
                }
            }

            attackTimer = attackInterval;

            transform.position = playerLocation.position + directionFacing; //move the sprite outwards
            
            Vector3 temp =  Quaternion.Euler(0,0,90) * directionFacing;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, temp); //rotate sprite based on player's current direction
            anim.SetTrigger("attacking");
        }
    }

    //represents attack range for debugging purposes
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(playerLocation.position + directionFacing, new Vector3(attackRangeX, attackRangeY, 0));
    }
}
