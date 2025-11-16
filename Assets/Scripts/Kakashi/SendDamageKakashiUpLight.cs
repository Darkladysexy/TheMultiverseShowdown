using UnityEngine;
using System.Collections.Generic;

public class SendDamageKakashiUpLight : MonoBehaviour
{
    private string tagEnemy;
    private GameObject parent;
    private KakashiUpLightAttack attackScript;
    private Collider2D hurtboxCollider;
    private float launchForce = 4f; // Lực hất tung

    void Awake()
    {
        parent = transform.parent.gameObject;
        attackScript = parent.GetComponent<KakashiUpLightAttack>();
        if (attackScript == null) 
        {
            Debug.LogError($"[{gameObject.name}] Không tìm thấy script KakashiUpLightAttack trên {parent.name}!");
            enabled = false;
            return;
        }
        tagEnemy = (parent.CompareTag("P1")) ? "P2" : "P1";
        hurtboxCollider = GetComponent<Collider2D>();
    }

    void OnEnable()
    {
        CheckForHit();
    }

    void CheckForHit()
    {
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
                    // Hất văng lên trên
                    Vector3 knockbackDir = Vector3.up; 
                    
                    // Gây sát thương nặng (true = đòn ngã/hất tung)
                    enemyHealth.TakeDamage(damage, launchForce, knockbackDir, true); 
                    
                    // Gọi hàm bay theo của Kakashi
                    if(attackScript != null)
                        attackScript.FollowUpJump();

                    gameObject.SetActive(false); 
                    break; 
                }
            }
        }
    }
}