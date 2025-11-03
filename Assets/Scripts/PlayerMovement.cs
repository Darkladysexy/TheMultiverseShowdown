using System;
using System.Collections;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2f;
    public float height = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    public bool isFacingRight = true;
    public bool isStun = false;
    public float time = 0.25f;
    private KeyCode keyCodeLeft;
    private KeyCode keyCodeRight;
    private KeyCode keyCodeJump;
    private bool isMovement = false;
    private LegPlayer legPlayer;
    private int jumpUsage = 0;
    private int playerLayer;
    private int jumpingPlayer;
    private int dashingLayer;
    void Awake()
    {
        keyCodeLeft = (this.gameObject.CompareTag("P1")) ? KeyCode.A : KeyCode.LeftArrow;
        keyCodeRight = (this.gameObject.CompareTag("P1")) ? KeyCode.D : KeyCode.RightArrow;
        keyCodeJump = (this.gameObject.CompareTag("P1")) ? KeyCode.K : KeyCode.Keypad2;

        playerLayer = LayerMask.NameToLayer("Player");
        jumpingPlayer = LayerMask.NameToLayer("Jumping_Player");
        dashingLayer = LayerMask.NameToLayer("DashingPlayer");
    }
    // Start is called once before   the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();

        foreach(Transform child in this.gameObject.transform)
        {
            if(child.gameObject.name == "Leg")
            {
                legPlayer = child.gameObject.GetComponent<LegPlayer>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStun)
            Jump();
        animator.SetBool("isGrounded", legPlayer.isGrounded);
        animator.SetFloat("Speed", Math.Abs(rb.linearVelocity.x));
        animator.SetFloat("VerticalSpeed", rb.linearVelocity.y);
        animator.SetBool("isMovement", isMovement);
        ChangeLayerJump();

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
        if (Input.GetKeyDown(keyCodeJump) && jumpUsage < 1)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, height);
            jumpUsage++;
        }
        if (legPlayer.isGrounded) jumpUsage = 0;
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
    public void StartJump()
    {
        this.gameObject.layer = jumpingPlayer;
        foreach(Transform child in this.gameObject.transform)
        {
            child.gameObject.layer = jumpingPlayer;
        }
    }
    public void EndJump()
    {
        this.gameObject.layer = playerLayer;
        foreach (Transform child in this.gameObject.transform)
        {
            child.gameObject.layer = playerLayer;
        }
    }
    public void ChangeLayerJump()
    {
        if (rb.linearVelocityY > 0.1)
        {
            this.gameObject.layer = jumpingPlayer;
            foreach (Transform child in this.gameObject.transform)
            {
                child.gameObject.layer = jumpingPlayer;
            }
        }
        else
        {
            if (this.gameObject.layer != dashingLayer)
            {
                this.gameObject.layer = playerLayer;
                foreach (Transform child in this.gameObject.transform)
                {
                    child.gameObject.layer = playerLayer;
                }
            }
        }
    }
}
