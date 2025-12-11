using UnityEngine;
using System.Collections.Generic;
using System.Collections; 

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class NinkenProjectile : MonoBehaviour
{
    [Header("Chỉ số")]
    public float lifetime = 3f;         // Thời gian tồn tại
    public float knockbackForce = 0.1f;   // Lực đẩy lùi khi trúng

    // Các biến nội bộ
    private Rigidbody2D rb;
    private string enemyTag;
    private int damage;
    // Cờ để đảm bảo chỉ gây sát thương 1 lần.
    private bool hasHitEnemy = false; 
    // Cờ để đảm bảo quá trình hủy chỉ bắt đầu 1 lần.
    private bool isDestroying = false; 
    private const float DESTROY_DELAY = 0.2f; 

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<Collider2D>().isTrigger = true;
        
        // Bắt đầu Coroutine tự hủy theo thời gian (lifetime)
        StartCoroutine(SelfDestructAfterTime(lifetime)); 
    }

    /// <summary>
    /// Hàm này được Kakashi gọi khi triệu hồi
    /// </summary>
    public void Initialize(bool isFacingRight, string tagToHit, int dmg, float jumpForce, float speed)
    {
        this.enemyTag = tagToHit;
        this.damage = dmg;

        float direction = isFacingRight ? 1f : -1f;

        // Tác dụng lực nhảy và lướt tới
        rb.linearVelocity = new Vector2(speed * direction, jumpForce);

        // Lật sprite nếu Kakashi quay sang trái
        if (!isFacingRight)
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1; // Lật ngược
            transform.localScale = newScale;
        }
    }

    /// <summary>
    /// Xử lý va chạm
    /// </summary>
    void OnTriggerEnter2D(Collider2D collision)
    {
        bool isHitTarget = collision.gameObject.CompareTag(enemyTag);
        bool isHitGround = collision.gameObject.CompareTag("Ground");

        // --- 1. XỬ LÝ VA CHẠM VỚI KẺ THÙ ---
        if (isHitTarget && !hasHitEnemy)
        {
            PlayerHealth enemyHealth = collision.GetComponent<PlayerHealth>();
            if (enemyHealth != null)
            {
                hasHitEnemy = true; // Đã gây sát thương
                Vector3 knockbackDir = (collision.transform.position - transform.position).normalized;
                
                // Gây sát thương (true = đòn đánh ngã)
                enemyHealth.TakeDamage(damage, knockbackForce, knockbackDir, true);
                
                // KHÔNG DỪNG CHUYỂN ĐỘNG. NÓ SẼ TIẾP TỤC RƠI.
                // KHÔNG GỌI DelayedDestroy.
            }
        }
        
        // --- 2. XỬ LÝ VA CHẠM VỚI ĐẤT ---
        // Bắt đầu quá trình hủy dù nó có hit enemy hay chưa.
        if (isHitGround && !isDestroying) 
        {
            isDestroying = true; // Ngăn không cho gọi lại
            StopAllCoroutines(); // Dừng coroutine SelfDestructAfterTime

            // Dừng ngay lập tức vật lý
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.isKinematic = true; 
            }
            
            // Chờ 0.2s rồi hủy
            StartCoroutine(DelayedDestroy(DESTROY_DELAY));
        }
    }
    
    /// <summary>
    /// Coroutine chờ 0.2s rồi hủy
    /// </summary>
    private IEnumerator DelayedDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
    
    /// <summary>
    /// Coroutine tự hủy nếu không va chạm (fallback)
    /// </summary>
    private IEnumerator SelfDestructAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (!isDestroying) 
        {
            isDestroying = true;
            Destroy(gameObject);
        }
    }
}