using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// to tell the player what direction you have to move.
/// </summary>

public class PlayerController : MonoBehaviour
{

    MovementController movementController;

    public SpriteRenderer spriteRenderer;
    public Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        movementController = GetComponent<MovementController>();
        movementController.lastMovingDirection = "left";
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movementController.SetDirection("left");
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movementController.SetDirection("right");
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            movementController.SetDirection("up");
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            movementController.SetDirection("down");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Player will go idle state to moving state
        animator.SetBool("Moving", true);

        bool flipX = false;
        bool flipY = false;
        if(movementController.lastMovingDirection == "left")
        {
            animator.SetInteger("direction", 0);
        } else if (movementController.lastMovingDirection == "right")
        {
            animator.SetInteger("direction", 0);
            flipX = true;
        } else if (movementController.lastMovingDirection == "up")
        {
            animator.SetInteger("direction", 1);
        } else if (movementController.lastMovingDirection == "down")
        {
            animator.SetInteger("direction", 1);
            flipY = true;
        }
        //change the direction of sprite
        spriteRenderer.flipY = flipY;
        spriteRenderer.flipX = flipX;
    }
}
