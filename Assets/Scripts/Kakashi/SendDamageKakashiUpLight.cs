using UnityEngine;
using System.Collections.Generic;

public class SendDamageKakashiUpLight : MonoBehaviour
{
    private string tagEnemy;
    private GameObject parent;
    private KakashiUpLightAttack attackScript;
    private Collider2D hurtboxCollider;
    private float launchForce = 4f; // Lực hất tung ĐÒN 1
    private float horizontalForce = 4f; // <--- Lực bay ngang ĐÒN 2
    private bool hasHitThisEnable = false; // Cờ để đảm bảo chỉ đánh trúng 1 lần/lần bật

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
        hasHitThisEnable = false; // Reset cờ mỗi khi hurtbox được bật
        CheckForHit();
    }

    void CheckForHit()
    {
        if (attackScript == null || hasHitThisEnable) return;

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
                PlayerMovement kakashiMovement = parent.GetComponent<PlayerMovement>(); // <--- LẤY MOVEMENT
                
                if (enemyHealth != null && kakashiMovement != null)
                {
                    Vector3 knockbackDir;
                    float finalForce;
                    bool isHeavyHit = false; // Luôn là đòn nặng để kích hoạt stun/knockdown

                    // --- LOGIC MỚI: KIỂM TRA ĐÒN 1 HAY ĐÒN 2 ---
                    if (attackScript.isFollowUpHit) // ĐÒN 2 (Đá văng ngang)
                    {
                        // Hướng bay ngang (ra xa Kakashi)
                        float dirX = kakashiMovement.isFacingRight ? 1f : -1f;
                        knockbackDir = new Vector3(dirX, 0.2f, 0).normalized; // Bay ngang + hơi lên 
                        finalForce = horizontalForce; // Lực bay ngang mạnh
                        
                        // KHÔNG CẦN GỌI FollowUpJump()
                        Debug.Log("[W+U] Hit 2: Bay ngang!");
                    }
                    else // ĐÒN 1 (Đá hất tung)
                    {
                        knockbackDir = Vector3.up; // Hất văng lên trên
                        finalForce = launchForce; // Lực hất tung
                        
                        // Gọi hàm bay theo của Kakashi
                        attackScript.FollowUpJump();
                        Debug.Log("[W+U] Hit 1: Bay lên!");
                    }
                    // --- KẾT THÚC LOGIC PHÂN BIỆT ---
                    
                    // Gây sát thương
                    enemyHealth.TakeDamage(damage, finalForce, knockbackDir, isHeavyHit); 
                    
                    hasHitThisEnable = true; // Đánh dấu đã trúng

                    // Tắt hurtbox (Đòn 1: để đợi đòn 2. Đòn 2: để kết thúc)
                    gameObject.SetActive(false); 
                    break; 
                }
            }
        }
    }
}