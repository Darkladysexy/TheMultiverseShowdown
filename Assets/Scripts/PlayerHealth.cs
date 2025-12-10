using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour, ITakeDamage
{
    public int health = 100; 
    private Rigidbody2D rb;
    
    private Animator animator;
    private PlayerMovement playerMovement;
    private PlayerBlock playerBlock;

    [Header("Knockback Settings")]
    public float heavyKnockbackUpwardForce = 1.2f; 
    public float heavyKnockbackDelay = 0.1f; 

    // --- BIẾN MỚI ĐỂ QUẢN LÝ COROUTINE ---
    private Coroutine knockbackCoroutine;

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
        
        if (force > 0)
        {
            if (isHeavyHit)
            {
                // Nếu đang có knockback chờ, hủy nó đi để nhận cái mới
                if (knockbackCoroutine != null) StopCoroutine(knockbackCoroutine);
                
                // Lưu coroutine lại để quản lý
                knockbackCoroutine = StartCoroutine(ApplyDelayedKnockback(force, dirForce));
            }
            else
            {
                rb.AddForce(dirForce * force, ForceMode2D.Impulse);
            }
        }

        // Logic Stun
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(1); 
        bool isAlreadyInHeavyStun = stateInfo.IsName("TakeDamageFall") || stateInfo.IsName("GetUp");

        if (playerMovement != null)
        {
            if (isHeavyHit)
            {
                playerMovement.Stun(true); 
                animator.SetTrigger("TakeDamageFall"); 
            }
            else if (!isAlreadyInHeavyStun)
            {
                playerMovement.Stun(false);
                animator.SetTrigger("TakeDamage"); 
            }
        }
    }

    // --- HÀM MỚI: HỦY KNOCKBACK (Dùng cho Thế Thân) ---
    public void CancelKnockback()
    {
        // Hủy lệnh đẩy lùi đang chờ
        if (knockbackCoroutine != null)
        {
            StopCoroutine(knockbackCoroutine);
            knockbackCoroutine = null;
        }
        // Dừng lực hiện tại ngay lập tức để nhân vật đứng yên
        if (rb != null) rb.linearVelocity = Vector2.zero;
    }

    private IEnumerator ApplyDelayedKnockback(float force, Vector3 dirForce)
    {
        yield return new WaitForSeconds(heavyKnockbackDelay);
        
        if (this != null && rb != null && gameObject.activeSelf)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); 
            Vector2 knockbackVector = new Vector2(dirForce.x, heavyKnockbackUpwardForce).normalized; 
            rb.AddForce(knockbackVector * force, ForceMode2D.Impulse);
        }
        
        knockbackCoroutine = null; // Reset biến khi chạy xong
    }

    void Update()
    {
        
    }
}