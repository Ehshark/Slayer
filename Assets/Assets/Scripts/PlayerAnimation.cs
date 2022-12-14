using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spr;

    private enum State {idle, running} //all animation states
    private State state = State.idle; //player's default state is idle

    public Vector3 playerVelocity;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        //flipping the sprite
        if(playerVelocity.x < 0)
        {
            spr.flipX = true;
        }
        if(playerVelocity.x > 0)
        {
            spr.flipX = false;
        }

        AnimationState();
        anim.SetInteger("state", (int)state);
    }

    //State machine
    private void AnimationState()
    {
        if(Mathf.Abs(playerVelocity.x) > 0f || Mathf.Abs(playerVelocity.y) > 0f) //if player has any velocity
        {
            state = State.running;
        }
        else
        {
            state = State.idle; //player standing still
        }
    }
}
