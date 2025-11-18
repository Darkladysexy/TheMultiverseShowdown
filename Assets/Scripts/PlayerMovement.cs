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
    private PlayerBlock playerBlock;
    private LegPlayer legPlayer;
    [HideInInspector] public bool isFacingRight = true; 
    [HideInInspector] public bool isStun = false; 
    public float time = 0.25f; // Thời gian stun TỐI THIỂU cho đòn nhẹ
    private KeyCode keyCodeLeft;
    private KeyCode keyCodeRight;
    private KeyCode keyCodeJump;
    private bool isMovement = false;
    private int jumpUsage = 0; 
    private int playerLayer; 
    private int jumpingPlayer; 
    private int dashingLayer;

    private PlayerHealth playerHealth;
    // [HideInInspector] public bool isBlocking;
    
    private Coroutine stunCoroutine; // Biến để lưu trữ coroutine stun

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
        playerHealth = GetComponent<PlayerHealth>();
        playerBlock = this.gameObject.GetComponent<PlayerBlock>();

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
        if (isStun)
        {
            return; // KHÓA INPUT KHI STUN
        }

        if (!playerBlock.isBlocking)
            Jump();

        animator.SetBool("isGrounded", legPlayer.isGrounded);
        animator.SetFloat("Speed", Math.Abs(rb.linearVelocity.x));
        animator.SetFloat("VerticalSpeed", rb.linearVelocity.y); 
        animator.SetBool("isMovement", isMovement);
        ChangeLayerJump();
        // animator.SetBool("IsBlocking", isBlocking);
    }
    
    void FixedUpdate()
    {
        if (!isStun && !playerBlock.isBlocking)
            Move();
    }
    
    // // (Các hàm StartBlocking, StopBlocking, Move, Jump giữ nguyên)
    // public void StartBlocking()
    // {
    //     if (isBlocking) return; 
    //     isBlocking = true;
    //     if (playerHealth != null)
    //         playerHealth.isBlocking = true;
    //     rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
    // }

    // public void StopBlocking()
    // {
    //     if (!isBlocking) return; 
    //     isBlocking = false;
    //     if (playerHealth != null)
    //         playerHealth.isBlocking = false;
    // }

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


    // === HÀM STUN ĐÃ CẬP NHẬT ===
    
    // Hàm này được PlayerHealth hoặc các Skill gọi
    public void Stun(bool isHeavyHit)
    {
        // 1. Đặt isStun = true NGAY LẬP TỨC
        isStun = true;
        
        // 2. Dừng Coroutine stun cũ (nếu có)
        if (stunCoroutine != null)
        {
            StopCoroutine(stunCoroutine);
        }
        
        // 3. Chỉ bắt đầu Coroutine đếm ngược nếu đây là đòn nhẹ (flinch)
        if (!isHeavyHit)
        {
            stunCoroutine = StartCoroutine(StunDuration(time));
        }
        // Nếu là đòn nặng (isHeavyHit = true), Coroutine sẽ KHÔNG chạy.
        // Trạng thái stun sẽ chỉ kết thúc khi anim 'GetUp' gọi EndStun().
    }
    
    private IEnumerator StunDuration(float time)
    {
        yield return new WaitForSeconds(time);
        
        // Hết giờ? Tự động gọi EndStun()
        // Đặt coroutine về null trước khi gọi EndStun
        stunCoroutine = null; 
        EndStun();
    }
    
    // (StartStun() không còn được dùng bên ngoài nữa)  
    public void StartStun()
    {
        // rb.linearVelocity = new Vector2(0,rb.linearVelocity.y);
        isStun = true;
    }

    // Hàm này được Coroutine ở trên HOẶC Animation Event 'GetUp' gọi
    public void EndStun()
    {
        // Dừng Coroutine (nếu nó vẫn đang chạy)
        if (stunCoroutine != null)
        {
            StopCoroutine(stunCoroutine);
            stunCoroutine = null;
        }
        
        // Mở khóa
        isStun = false;
    }

    // (Các hàm Jump/Layer giữ nguyên)
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
        if (rb.linearVelocity.y > 0.1)
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