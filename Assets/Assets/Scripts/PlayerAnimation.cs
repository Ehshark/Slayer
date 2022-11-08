using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;

    private enum State {idle, running}
    private State state = State.idle;

    public Vector2 playerVelocity;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if(playerVelocity.x < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        if(playerVelocity.x > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }

        AnimationState();
        anim.SetInteger("state", (int)state);

        Debug.Log(state);
    }

    private void AnimationState()
    {
        if(Mathf.Abs(playerVelocity.x) > 0f || Mathf.Abs(playerVelocity.y) > 0f)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }
}
