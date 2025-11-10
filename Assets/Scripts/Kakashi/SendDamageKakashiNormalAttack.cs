using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendDamageKakashiNormalAttack : MonoBehaviour
{
    private string tagEnemy;
    private GameObject parent;
    private KakashiNormalAttack kakashiNormalAttack;
    public int attackComboStep; // Bắt buộc set 1, 2, hoặc 3
    private Collider2D hurboxCollider; // Sẽ được gán trong Awake
    private float force = 2f; // Lực văng cho đòn 3
    private bool hasHitThisEnable = false;

    // Awake() LUÔN chạy trước OnEnable(), sửa lỗi NullReferenceException
    void Awake()
    {
        // 1. Lấy Collider CỦA CHÍNH MÌNH trước tiên
        hurboxCollider = this.GetComponent<Collider2D>();
        if (hurboxCollider == null)
        {
             Debug.LogError($"[{gameObject.name}] KHÔNG CÓ COLLIDER2D!");
            enabled = false;
            return;
        }
        
        // 2. Lấy thông tin từ Parent (Kakashi)
        if (transform.parent == null)
        {
            Debug.LogError($"[{gameObject.name}] KHÔNG CÓ PARENT!");
            enabled = false; 
            return;
        }
        parent = transform.parent.gameObject;

        kakashiNormalAttack = parent.GetComponent<KakashiNormalAttack>(); 
        if (kakashiNormalAttack == null)
        {
            Debug.LogError($"[{gameObject.name}] KHÔNG TÌM THẤY KakashiNormalAttack trên {parent.name}!");
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
    }

    // OnEnable() được gọi MỖI KHI hurtbox được SetActive(true)
    void OnEnable()
    {
        // Nếu Awake() bị lỗi, thoát ra
        if (kakashiNormalAttack == null || hurboxCollider == null) return;

        hasHitThisEnable = false; 
        
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        contactFilter2D.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter2D.useTriggers = true; // Chỉ tìm Trigger
        
        List<Collider2D> results = new List<Collider2D>();
        
        // DÒNG 91 CŨ CỦA BẠN (giờ đã an toàn vì hurboxCollider được gán trong Awake)
        Physics2D.OverlapCollider(hurboxCollider, contactFilter2D, results); 
        
        int damage = kakashiNormalAttack.GetDamageForComboStep(attackComboStep); 
        
        if (damage <= 0) return;
        
        foreach (Collider2D collision in results)
        {
            if (collision.gameObject.CompareTag(tagEnemy) && !hasHitThisEnable)
            {
                PlayerHealth enemyHealth = collision.gameObject.GetComponent<PlayerHealth>();
                
                if (enemyHealth != null)
                {
                    // === LOGIC LỰC VĂNG MỚI ===
                    bool isHeavy = (attackComboStep == 3); // Chỉ đòn 3 là đòn nặng
                    float knockback = isHeavy ? this.force : 0f; // Đòn 1 và 2 không đẩy lùi
                    // =========================
                        
                    Vector3 knockbackDir = (collision.gameObject.transform.position - parent.transform.position).normalized;
                    
                    // GỌI HÀM GÂY SÁT THƯƠNG
                    enemyHealth.TakeDamage(damage, knockback, knockbackDir, isHeavy); 
                    
                    if (isHeavy)
                    {
                        // Thêm hiệu ứng nếu là đòn 3
                        GameManager.instant.PauseGame(this.transform.position);
                        CameraManager.instant.StartShake(0.1f, 0.1f,this.transform);
                    }
                    
                    Debug.Log($"[{gameObject.name}] Dealt {damage} damage to {collision.gameObject.name}");
                    hasHitThisEnable = true; 
                }
            }
        }
    }
}