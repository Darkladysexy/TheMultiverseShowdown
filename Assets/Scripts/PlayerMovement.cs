using System;
using System.Collections;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Toc do chay va do cao")]
    public float speed = 2f;
    public float height = 5f;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator animator;
    
    private LegPlayer legPlayer;
    [HideInInspector] public bool isFacingRight = true; 
    [HideInInspector] public bool isStun = false; 
    public float time = 0.25f;
    private KeyCode keyCodeLeft;
    private KeyCode keyCodeRight;
    private KeyCode keyCodeJump;
    private bool isMovement = false;
    private int jumpUsage = 0; 
    private int playerLayer; 
    private int jumpingPlayer; 
    private int dashingLayer;

    // --- THÊM CÁC BIẾN SAU ---
    private PlayerHealth playerHealth;
    [HideInInspector] public bool isBlocking = false;
    // --- KẾT THÚC THÊM BIẾN ---

    void Awake()
    {
        keyCodeLeft = (this.gameObject.CompareTag("P1")) ? KeyCode.A : KeyCode.LeftArrow;
        keyCodeRight = (this.gameObject.CompareTag("P1")) ? KeyCode.D : KeyCode.RightArrow;
        keyCodeJump = (this.gameObject.CompareTag("P1")) ? KeyCode.K : KeyCode.Keypad2;

        playerLayer = LayerMask.NameToLayer("Player");
        jumpingPlayer = LayerMask.NameToLayer("Jumping_Player");
        dashingLayer = LayerMask.NameToLayer("DashingPlayer");
    }
    
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        
        // --- THÊM DÒNG NÀY ---
        playerHealth = GetComponent<PlayerHealth>();

        foreach (Transform child in this.gameObject.transform)
        {
            if (child.gameObject.name == "Leg")
            {
                legPlayer = child.gameObject.GetComponent<LegPlayer>();
            }
        }
    }
    
    void Update()
    {
        // Ngăn nhảy khi đang thủ
        if (!isStun && !isBlocking)
            Jump();

        animator.SetBool("isGrounded", legPlayer.isGrounded);
        animator.SetFloat("Speed", Math.Abs(rb.linearVelocity.x));
        animator.SetFloat("VerticalSpeed", rb.linearVelocity.y);
        animator.SetBool("isMovement", isMovement);
        ChangeLayerJump();

        // --- THÊM DÒNG NÀY ---
        // Gửi trạng thái thủ cho Animator
        animator.SetBool("IsBlocking", isBlocking);
    }
    
    void FixedUpdate()
    {
        // Ngăn di chuyển khi đang thủ
        if (!isStun && !isBlocking)
            Move();
    }
    
    // --- THÊM 2 HÀM MỚI SAU ĐÂY ---
    // SkillManager sẽ gọi hàm này
    public void StartBlocking()
    {
        if (isBlocking) return; // Đã thủ rồi thì thôi
        isBlocking = true;
        if (playerHealth != null)
            playerHealth.isBlocking = true;
        
        // Ngừng di chuyển ngay lập tức
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
    }

    // SkillManager sẽ gọi hàm này
    public void StopBlocking()
    {
        if (!isBlocking) return; // Đã nghỉ thủ rồi thì thôi
        isBlocking = false;
        if (playerHealth != null)
            playerHealth.isBlocking = false;
    }
    // --- KẾT THÚC THÊM HÀM ---


    private void Move()
    {
        if (Input.GetKey(keyCodeRight))
        {
            isMovement = true;
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
            animator.SetFloat("Speed", 2f);
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
            if (isFacingRight)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                isFacingRight = false;
            }
        }
        else
        {
            isMovement = false;
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            animator.SetFloat("Speed", 0f);
        }
    }
    
    private void Jump()
    {
        if (Input.GetKeyDown(keyCodeJump) && jumpUsage < 1)
        {
            if(legPlayer.isGrounded || rb.linearVelocityY < -0.1)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, height);
                jumpUsage++;
            }
        }
        if (legPlayer.isGrounded) jumpUsage = 0;
    }

    // (Hàm Stun và EndStun sẽ được gọi bởi PlayerHealth và Animation Event)
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