using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendDamageKakashiHeavy : MonoBehaviour
{
    private string tagEnemy;
    private GameObject parent;
    private KakashiHeavyAttack kakashiHeavyAttack;
    private Collider2D hurboxCollider;
    private float force = 3f;
    private bool hasHitThisEnable = false; // Ngăn 1 đòn đánh trúng nhiều lần

    void Awake()
    {
        // ... (Giữ nguyên Awake) ...
        if (transform.parent == null)
        {
            Debug.LogError($"[{gameObject.name}] KHÔNG CÓ PARENT! HurtBox phải là child của Kakashi!");
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
        if (parent.CompareTag("P1"))
            tagEnemy = "P2";
        else if (parent.CompareTag("P2"))
            tagEnemy = "P1";
        else
        {
            tagEnemy = "P2"; // Default
        }
    }

    void OnEnable()
    {
        hasHitThisEnable = false; // Reset
        
        if (kakashiHeavyAttack == null) return;
        
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        contactFilter2D.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter2D.useTriggers = true;
        
        hurboxCollider = this.GetComponent<Collider2D>();
        
        if (hurboxCollider == null) return;
        
        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCollider(hurboxCollider, contactFilter2D, results);
        
        int damage = kakashiHeavyAttack.damage;
        
        if (damage <= 0) return;
        
        foreach (Collider2D collision in results)
        {
            if (collision.gameObject.CompareTag(tagEnemy) && !hasHitThisEnable)
            {
                PlayerHealth enemyHealth = collision.gameObject.GetComponent<PlayerHealth>();
                
                if (enemyHealth != null)
                {
                    Vector3 knockbackDir = (collision.gameObject.transform.position - parent.transform.position).normalized;
                    
                    // --- SỬA DÒNG NÀY ---
                    enemyHealth.TakeDamage(damage, force, knockbackDir, true); // true = đây là đòn nặng
                    // --- KẾT THÚC SỬA ---
                    
                    Debug.Log($"[{gameObject.name}] Dealt {damage} damage to {collision.gameObject.name}");
                    hasHitThisEnable = true;
                }
            }
        }
    }
}