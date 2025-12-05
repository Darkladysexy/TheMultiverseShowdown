using UnityEngine;
using System.Collections;
using System;

public class PlayerHealth : MonoBehaviour, ITakeDamage
{
    public int health = 1000; 
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerMovement playerMovement;
    private PlayerBlock playerBlock;
    public event Action<int> OnChangeHealth;
    [Header("Knockback Settings")]
    public float heavyKnockbackUpwardForce = 1.2f; 
    public float heavyKnockbackDelay = 0.1f; 

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        playerBlock = this.gameObject.GetComponent<PlayerBlock>();
    }

    public void TakeDamage(int damage, float force, Vector3 dirForce, bool isHeavyHit)
    {
        if (playerBlock.isBlocking)
        {
            rb.AddForce(dirForce * (force * 0.5f), ForceMode2D.Impulse); 
            return;
        }

        health -= damage; 
        OnChangeHealth?.Invoke(health);
        
        if (force > 0)
        {
            if (isHeavyHit)
            {
                StartCoroutine(ApplyDelayedKnockback(force, dirForce));
            }
            else
            {
                rb.AddForce(dirForce * force, ForceMode2D.Impulse);
            }
        }

        // === LOGIC STUN MỚI (KIỂM TRA ANIMATOR) ===
        
        // Lấy State hiện tại của Attack Layer (Layer 1)
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(1); 
        bool isAlreadyInHeavyStun = stateInfo.IsName("TakeDamageFall") || stateInfo.IsName("GetUp");

        if (playerMovement != null)
        {
            if (isHeavyHit)
            {
                // 1. Nếu là đòn nặng: Luôn luôn kích hoạt Stun(true) (để ngắt Coroutine cũ)
                playerMovement.Stun(true); 
                animator.SetTrigger("TakeDamageFall"); 
            }
            else if (!isAlreadyInHeavyStun)
            {
                // 2. Nếu là đòn nhẹ (DOT) VÀ KHÔNG ĐANG TRONG TRẠNG THÁI NGÃ:
                // Mới kích hoạt Stun(false) (bật Coroutine 0.25s) và anim giật mình
                playerMovement.Stun(false);
                animator.SetTrigger("TakeDamage"); 
            }
            // 3. (Trường hợp 3): Nếu là đòn nhẹ (DOT) VÀ ĐANG NGÃ (isAlreadyInHeavyStun = true)
            // -> KHÔNG LÀM GÌ CẢ. 
            //    Không gọi Stun(false), không chạy timer 0.25s.
            //    Không gọi SetTrigger("TakeDamage").
            //    Chỉ để cho anim ngã tiếp tục.
        }
    }

    private IEnumerator ApplyDelayedKnockback(float force, Vector3 dirForce)
    {
        yield return new WaitForSeconds(heavyKnockbackDelay);
        if (this != null && rb != null)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); 
            Vector2 knockbackVector = new Vector2(dirForce.x, heavyKnockbackUpwardForce).normalized; 
            rb.AddForce(knockbackVector * force, ForceMode2D.Impulse);
        }
    }

    void Update()
    {
        
    }
}