using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2f;
    public float dashSpeed = 5f;
    public float runSpeed = 2f;
    public float height = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isFacingRight = true;
    private bool isStun = false;
    private bool isDash = false;
    public float time = 0.25f;
        // Start is called once before   the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = runSpeed;
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isStun);
        if (!isStun)
            Jump();
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && Input.GetKeyDown(KeyCode.L) && !isDash) animator.SetTrigger("Dash");
        animator.SetBool("isGrounded", LegPlayer.instant.isGrounded);
        animator.SetFloat("Speed", Math.Abs(rb.linearVelocity.x));
        animator.SetFloat("VerticalSpeed", rb.linearVelocity.y);

    }
    void FixedUpdate()
    {
        if (!isStun)
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
        if (Input.GetKeyDown(KeyCode.Space) && LegPlayer.instant.isGrounded)
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
    public void StartDash()
    {
        speed = dashSpeed;
        isDash = true;
    }
    public void EndDash()
    {
        speed = runSpeed;
        isDash = false;
    }

}
