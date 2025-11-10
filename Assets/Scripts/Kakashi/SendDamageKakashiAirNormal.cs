using UnityEngine;
using System.Collections.Generic;

public class SendDamageKakashiAirNormal : MonoBehaviour
{
    private string tagEnemy;
    private GameObject parent;
    private KakashiAirNormalAttack attackScript;
    private Collider2D hurtboxCollider;
    private float force = 2.5f; // Lực đẩy lùi
    private bool hasHit = false; // Cờ để đảm bảo chỉ đánh trúng 1 lần

    void Awake()
    {
        parent = transform.parent.gameObject;
        
        attackScript = parent.GetComponent<KakashiAirNormalAttack>();
        if (attackScript == null)
        {
            Debug.LogError($"[{gameObject.name}] Không tìm thấy script KakashiAirNormalAttack trên {parent.name}!");
            enabled = false;
            return;
        }

        tagEnemy = (parent.CompareTag("P1")) ? "P2" : "P1";
        hurtboxCollider = GetComponent<Collider2D>();
    }

    void OnEnable()
    {
        hasHit = false; // Reset cờ mỗi khi hurtbox được bật
        CheckForHit(); // Kiểm tra va chạm ngay lập tức
    }

    void CheckForHit()
    {
        if (attackScript == null || hasHit) return;

        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        filter.useTriggers = true;

        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCollider(hurtboxCollider, filter, results);

        int damage = attackScript.damage; 

        foreach (Collider2D collision in results)
        {
            if (collision.gameObject.CompareTag(tagEnemy))
            {
                PlayerHealth enemyHealth = collision.gameObject.GetComponent<PlayerHealth>();
                if (enemyHealth != null)
                {
                    Vector3 knockbackDir = (collision.transform.position - parent.transform.position).normalized;
                    knockbackDir.y = Mathf.Abs(knockbackDir.y * 0.5f); // Hơi đẩy lên một chút
                    
                    // --- SỬA DÒNG NÀY ---
                    enemyHealth.TakeDamage(damage, force, knockbackDir, false); // false = không phải đòn ngã
                    // --- KẾT THÚC SỬA ---

                    Debug.Log($"[AirNormalAttack] Gây {damage} sát thương cho {collision.gameObject.name}");

                    hasHit = true; 
                    gameObject.SetActive(false); 
                    break; 
                }
            }
        }
    }
}