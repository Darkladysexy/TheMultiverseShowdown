using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SendDamageKakashiHeavy : MonoBehaviour
{
    private string tagEnemy;
    private GameObject parent;
    private KakashiHeavyAttack kakashiHeavyAttack;
    private Collider2D hurboxCollider;
    private float force = 3f; // Lực văng CUỐI CÙNG

    // --- Biến cho logic DOT (Damage Over Time) ---
    private List<PlayerHealth> enemiesHitThisAttack; // Danh sách địch đã trúng chiêu
    private float tickDamageInterval = 0.15f; // Gây sát thương mỗi 0.15s
    private float nextTickTime;
    private int tickDamage;

    void Awake()
    {
        // 1. Lấy Collider CỦA CHÍNH MÌNH
        hurboxCollider = this.GetComponent<Collider2D>();
        if (hurboxCollider == null)
        {
            Debug.LogError($"[{gameObject.name}] KHÔNG CÓ COLLIDER2D!");
            enabled = false;
            return;
        }
        // Đảm bảo là Trigger
        hurboxCollider.isTrigger = true; 

        // 2. Lấy thông tin từ Parent (Kakashi)
        if (transform.parent == null)
        {
            Debug.LogError($"[{gameObject.name}] KHÔNG CÓ PARENT!");
            enabled = false;
            return;
        }
        parent = transform.parent.gameObject;
        
        kakashiHeavyAttack = parent.GetComponent<KakashiHeavyAttack>();
        if (kakashiHeavyAttack == null)
        {
            Debug.LogError($"[{gameObject.name}] KHÔNG TÌM THẤY KakashiHeavyAttack trên {parent.name}!");
            enabled = false;
            return;
        }
        
        // 3. Lấy tag địch
        if (parent.CompareTag("P1"))
            tagEnemy = "P2";
        else if (parent.CompareTag("P2"))
            tagEnemy = "P1";
        else
            tagEnemy = "P2"; // Default
            
        // 4. Khởi tạo danh sách
        enemiesHitThisAttack = new List<PlayerHealth>();

        // 5. Tính sát thương mỗi tick (ví dụ: damage 35, chia làm 5 lần)
        // (Bạn có thể thay đổi số 5 này)
        tickDamage = Mathf.Max(1, kakashiHeavyAttack.damage / 5);
    }

    // OnEnable() được gọi MỖI KHI hurtbox được SetActive(true)
    void OnEnable()
    {
        // Reset danh sách địch đã trúng
        enemiesHitThisAttack.Clear();
        // Sẵn sàng gây sát thương ngay lập tức
        nextTickTime = Time.time;
    }

    // FixedUpdate() chạy liên tục khi GameObject active
    void FixedUpdate()
    {
        // Kiểm tra xem đã đến lúc gây sát thương tick tiếp theo chưa
        if (Time.time < nextTickTime)
        {
            return; // Chưa đến lúc
        }
        
        nextTickTime = Time.time + tickDamageInterval; // Hẹn giờ cho lần sau

        ContactFilter2D contactFilter2D = new ContactFilter2D();
        contactFilter2D.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter2D.useTriggers = true; 
        
        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCollider(hurboxCollider, contactFilter2D, results);
        
        foreach (Collider2D collision in results)
        {
            if (collision.gameObject.CompareTag(tagEnemy))
            {
                PlayerHealth enemyHealth = collision.gameObject.GetComponent<PlayerHealth>();
                if (enemyHealth != null)
                {
                    // Gây sát thương tick (0 lực văng, không phải đòn ngã)
                    enemyHealth.TakeDamage(tickDamage, 0f, Vector3.zero, false); 
                    
                    // Thêm địch vào danh sách (nếu chưa có) để gây văng ở cuối
                    if (!enemiesHitThisAttack.Contains(enemyHealth))
                    {
                        enemiesHitThisAttack.Add(enemyHealth);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Hàm này được KakashiHeavyAttack gọi tại Event KẾT THÚC
    /// </summary>
    public void ApplyFinalKnockback()
    {
        Debug.Log($"[SendDamageKakashiHeavy] Gây văng cuối cùng cho {enemiesHitThisAttack.Count} kẻ địch.");
        foreach (PlayerHealth enemyHealth in enemiesHitThisAttack)
        {
            if (enemyHealth != null) // Kiểm tra xem địch còn sống không
            {
                Vector3 knockbackDir = (enemyHealth.transform.position - parent.transform.position).normalized;
                
                // Gây 0 sát thương, NHƯNG đẩy lùi và kích hoạt anim ngã
                enemyHealth.TakeDamage(0, force, knockbackDir, true); 
            }
        }
        enemiesHitThisAttack.Clear(); // Dọn dẹp danh sách
    }

    void OnDisable()
    {
        // Dọn dẹp danh sách khi tắt
        enemiesHitThisAttack.Clear();
    }
}