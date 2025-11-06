using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendDamageKakashiNormalAttack : MonoBehaviour
{
    private string tagEnemy;
    private GameObject parent;
    private KakashiNormalAttack kakashiNormalAttack;
    public int attackComboStep;
    private Collider2D hurboxCollider;
    private float force = 2f;

    void Awake()
    {
        // Kiểm tra parent
        if (transform.parent == null)
        {
            Debug.LogError($"[{gameObject.name}] KHÔNG CÓ PARENT! HurtBox phải là child của Kakashi!");
            enabled = false; // Tắt script này
            return;
        }
        
        parent = transform.parent.gameObject;
        
        // Tìm component KakashiNormalAttack
        kakashiNormalAttack = parent.GetComponent<KakashiNormalAttack>();
        
        if (kakashiNormalAttack == null)
        {
            Debug.LogError($"[{gameObject.name}] KHÔNG TÌM THẤY KakashiNormalAttack trên {parent.name}!");
            Debug.LogError($"Hãy đảm bảo {parent.name} có component KakashiNormalAttack!");
            enabled = false; // Tắt script này
            return;
        }
        
        // Set enemy tag
        if (parent.CompareTag("P1"))
            tagEnemy = "P2";
        else if (parent.CompareTag("P2"))
            tagEnemy = "P1";
        else
        {
            Debug.LogWarning($"[{gameObject.name}] Parent không có tag P1 hoặc P2!");
            tagEnemy = "P2"; // Default
        }
    }

    void OnEnable()
    {
        // Safety check
        if (kakashiNormalAttack == null)
        {
            Debug.LogError($"[{gameObject.name}] kakashiNormalAttack NULL trong OnEnable!");
            return;
        }
        
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        contactFilter2D.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter2D.useTriggers = true;
        
        hurboxCollider = this.GetComponent<Collider2D>();
        
        if (hurboxCollider == null)
        {
            Debug.LogError($"[{gameObject.name}] KHÔNG CÓ COLLIDER2D!");
            return;
        }
        
        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCollider(hurboxCollider, contactFilter2D, results);
        
        int damage = kakashiNormalAttack.GetDamageForComboStep(attackComboStep);
        
        if (damage <= 0)
        {
            Debug.LogWarning($"[{gameObject.name}] Damage = 0 cho combo step {attackComboStep}");
            return;
        }
        
        foreach (Collider2D collision in results)
        {
            if (collision.gameObject.CompareTag(tagEnemy))
            {
                PlayerHealth enemyHealth = collision.gameObject.GetComponent<PlayerHealth>();
                Animator enemyAnimator = collision.gameObject.GetComponent<Animator>();
                
                if (enemyHealth != null)
                {
                    if (enemyAnimator != null)
                        enemyAnimator.SetTrigger("TakeDamage");
                        
                    Vector3 knockbackDir = (collision.gameObject.transform.position - parent.transform.position).normalized;
                    enemyHealth.TakeDamage(damage, force, knockbackDir);
                    
                    Debug.Log($"[{gameObject.name}] Dealt {damage} damage to {collision.gameObject.name}");
                }
            }
        }
    }
}
