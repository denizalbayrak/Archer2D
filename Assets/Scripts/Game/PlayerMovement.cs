using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 velocity;
    private Vector2 inputMovement;
    public int moveSpeed = 10;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        velocity = new Vector2(moveSpeed, moveSpeed);
    }
    private void Update()
    {
        inputMovement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        } 
        
    }

    void FixedUpdate()
    {
        Vector2 delta = inputMovement * velocity * Time.deltaTime;
        Vector2 newPosition = rb.position + delta;
        rb.MovePosition(newPosition);
        
        if (inputMovement!= Vector2.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        if (inputMovement.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (inputMovement.x > 0) 
        {
            spriteRenderer.flipX = false;
        }        

    }
}

