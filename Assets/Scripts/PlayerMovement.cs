using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instant;
    public float speed = 2f;
    public float height = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    public bool isFacingRight = true;
    private bool isStun = false;
    public float time = 0.25f;
    private KeyCode keyCodeLeft;
    private KeyCode keyCodeRight;
    private KeyCode keyCodeJump;
    private bool isMovement = false;
    void Awake()
    {
        instant = this;
        keyCodeLeft = (this.gameObject.CompareTag("P1")) ? KeyCode.A : KeyCode.LeftArrow;
        keyCodeRight = (this.gameObject.CompareTag("P1")) ? KeyCode.D : KeyCode.RightArrow;
        keyCodeJump = (this.gameObject.CompareTag("P1")) ? KeyCode.K : KeyCode.Keypad2;
    }
    // Start is called once before   the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStun)
            Jump();
        animator.SetBool("isGrounded", LegPlayer.instant.isGrounded);
        animator.SetFloat("Speed", Math.Abs(rb.linearVelocity.x));
        animator.SetFloat("VerticalSpeed", rb.linearVelocity.y);
        animator.SetBool("isMovement", isMovement);

    }
    void FixedUpdate()
    {
        if (!isStun)
        Move();
        
    }
    private void Move()
    {
        if (Input.GetKey(keyCodeRight))
        {
            isMovement = true;
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
            animator.SetFloat("Speed", 2f);
            // rb.AddForce(moveVector.normalized, ForceMode2D.Impulse);
            if (!isFacingRight)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                isFacingRight = true;
            }
        }
        else if (Input.GetKey(keyCodeLeft))
        {
            isMovement = true;
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
            isMovement = false;
            // Khi không nhấn phím nào, đặt vận tốc về 0
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            animator.SetFloat("Speed", 0f);
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(keyCodeJump) && LegPlayer.instant.isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, height);
        }   
    }

    public void Stun()
    {
        isStun = true;
        StartCoroutine(StunDuration(time));
    }
    private IEnumerator StunDuration(float time)
    {
        yield return new WaitForSeconds(time);
        isStun = false;
    }
    public void StartStun()
    {
        isStun = true;
    }
    public void EndStun()
    {
        isStun = false;
    }
}
