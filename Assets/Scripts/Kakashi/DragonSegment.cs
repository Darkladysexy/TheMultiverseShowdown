using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Collider2D))]
public class DragonSegment : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 10f;     // Tốc độ bay tới
    public float lifetime = 2f;     // Thời gian 1 khúc rồng tồn tại

    [Header("Wave (Lắc Lư)")]
    public float amplitude = 0.5f; // Biên độ (độ cao) sóng
    public float frequency = 5f;   // Tần số (tốc độ) sóng

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private string enemyTag;
    private int damage;
    private Vector3 knockbackDirection;
    private bool hasHit = false;
    private float xSpeed;
    private float timeElapsed = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Thiết lập Rigidbody
        rb.isKinematic = false;
        rb.gravityScale = 0;
        
        // Tắt collider, script Initialize sẽ bật nó
        GetComponent<Collider2D>().enabled = false;
    }

    /// <summary>
    /// Được gọi bởi KakashiUpHeavyAttack để khởi động khúc rồng
    /// </summary>
    public void Initialize(bool facingRight, string tag, int dmg, Sprite spriteToUse, Vector3 knockbackDir)
    {
        this.enemyTag = tag;
        this.damage = dmg;
        this.knockbackDirection = knockbackDir;

        // 1. Gán Sprite (khúc rồng thứ 1, 2, 3...)
        spriteRenderer.sprite = spriteToUse;

        // 2. Lưu tốc độ bay tới
        this.xSpeed = facingRight ? speed : -speed;

        // 3. Lật hình nếu cần
        if (!facingRight)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // 4. Bật collider và tự hủy
        GetComponent<Collider2D>().enabled = true;
        Destroy(gameObject, lifetime);
    }

    // Dùng FixedUpdate để xử lý vật lý (bay và lắc lư)
    void FixedUpdate()
    {
        // Nếu chưa được Initialize (xSpeed = 0) thì không làm gì cả
        if (xSpeed == 0) return;

        timeElapsed += Time.fixedDeltaTime;

        // Tính toán vận tốc Y dựa trên hàm Cos (để tạo sóng)
        float yVelocity = amplitude * frequency * Mathf.Cos(timeElapsed * frequency);

        // Đặt vận tốc cho Rigidbody để nó tự di chuyển
        rb.linearVelocity = new Vector2(this.xSpeed, yVelocity);
    }

    // Xử lý va chạm
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit) return; 

        // Va chạm với Địch
        if (collision.CompareTag(enemyTag))
        {
            PlayerHealth enemyHealth = collision.GetComponent<PlayerHealth>();
            if (enemyHealth != null)
            {
                hasHit = true; 
                enemyHealth.TakeDamage(damage, 3f, knockbackDirection, true); // true = đòn nặng
                // Destroy(gameObject); 
            }
        }
        // Va chạm với Đất
        else if (collision.CompareTag("Ground"))
        {
            //  Destroy(gameObject); 
        }
    }
}