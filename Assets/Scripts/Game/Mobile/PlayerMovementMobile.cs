using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementMobile : MonoBehaviour
{
    public Animator animator;
    private SpriteRenderer spriteRenderer;

    public Joystick movementJoystick;
    public float playerSpeed;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
   
    void FixedUpdate()
    {
        if (movementJoystick.Direction.y != 0)
        {
            rb.velocity = new Vector2(movementJoystick.Direction.x * playerSpeed, movementJoystick.Direction.y * playerSpeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        if (movementJoystick.Direction != Vector2.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        if (movementJoystick.Direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (movementJoystick.Direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }

    }

    public void Attacked()
    {
        animator.SetTrigger("Attack");
    }

}
