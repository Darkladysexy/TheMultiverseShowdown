using UnityEngine;
using System.Collections.Generic;

public class SendDamageKakashiUpNormal : MonoBehaviour
{
    private string tagEnemy;
    private GameObject parent;
    private KakashiUpNormalAttack attackScript;
    private Collider2D hurtboxCollider;
    private float force = 0f;
    private bool hasHit = false;

    void Awake()
    {
        parent = transform.parent.gameObject;
        attackScript = parent.GetComponent<KakashiUpNormalAttack>();
        if (attackScript == null) 
        {
            Debug.LogError($"[{gameObject.name}] Không tìm thấy script KakashiUpNormalAttack trên {parent.name}!");
            enabled = false;
            return;
        }
        tagEnemy = (parent.CompareTag("P1")) ? "P2" : "P1";
        hurtboxCollider = GetComponent<Collider2D>();
    }

    void OnEnable()
    {
        hasHit = false;
        CheckForHit();
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
                    
                    // Gây sát thương nhẹ (false = không ngã)
                    enemyHealth.TakeDamage(damage, force, knockbackDir, false);

                    hasHit = true;
                    gameObject.SetActive(false);
                    break;
                }
            }
        }
    }
}