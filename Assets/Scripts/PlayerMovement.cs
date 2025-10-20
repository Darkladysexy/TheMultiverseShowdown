using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2f;
    public float height = 100f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isFacingRight = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        animator.SetBool("isGrounded", LegPlayer.instant.isGrounded);
        animator.SetFloat("Speed", Math.Abs(rb.linearVelocity.x));
        animator.SetFloat("verticalSpeed", rb.linearVelocity.y);

    }
    void FixedUpdate()
    {
        Move();
        
    }
    private void Move()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
            animator.SetFloat("Speed", 2f);
            // rb.AddForce(moveVector.normalized, ForceMode2D.Impulse);
            if (!isFacingRight)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                isFacingRight = true;
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
            animator.SetFloat("Speed", 2f);
            // rb.AddForce(moveVector.normalized, ForceMode2D.Impulse);
            if (isFacingRight)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                isFacingRight = false;
            }
        }
        else
        {
            // Khi không nhấn phím nào, đặt vận tốc về 0
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            animator.SetFloat("Speed", 0f);
        }
    }
    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && LegPlayer.instant.isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, height);
        }
    }
}
