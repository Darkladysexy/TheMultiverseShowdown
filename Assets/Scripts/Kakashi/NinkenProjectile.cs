using UnityEngine;
using System.Collections.Generic;

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
    private bool hasHit = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Đảm bảo Collider là Trigger để có thể va chạm và đi xuyên
        GetComponent<Collider2D>().isTrigger = true;
        
        // Tự hủy sau một khoảng thời gian
        Destroy(gameObject, lifetime);
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
        // Nếu đã đánh trúng rồi thì bỏ qua
        if (hasHit) return;

        // Kiểm tra xem có va phải kẻ thù không
        if (collision.gameObject.CompareTag(enemyTag))
        {
            PlayerHealth enemyHealth = collision.GetComponent<PlayerHealth>();
            if (enemyHealth != null)
            {
                hasHit = true;
                Vector3 knockbackDir = (collision.transform.position - transform.position).normalized;
                
                // Gây sát thương (true = đòn đánh ngã)
                enemyHealth.TakeDamage(damage, knockbackForce, knockbackDir, true);

                // (Tùy chọn) Thêm hiệu ứng đặc biệt khi đánh trúng
                // GameManager.instant.PauseGame(...)
                
                // Hủy con chó sau khi đánh trúng
                Destroy(gameObject, 0.1f); 
            }
        }
    }
}