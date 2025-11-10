using UnityEngine;

public class SendDamageKakashiDownNormal : MonoBehaviour
{
    private string tagEnemy;
    private GameObject parent;
    private KakashiDownNormalAttack attackScript; // Script chính
    private float force = 3f; // Lực đẩy lùi

    void Awake()
    {
        parent = transform.parent.gameObject;
        
        attackScript = parent.GetComponent<KakashiDownNormalAttack>();
        if (attackScript == null)
        {
            Debug.LogError($"[{gameObject.name}] Không tìm thấy script KakashiDownNormalAttack trên {parent.name}!");
            enabled = false;
            return;
        }

        tagEnemy = (parent.CompareTag("P1")) ? "P2" : "P1";
    }

    /// <summary>
    /// Dùng OnTriggerEnter2D vì đây là một detector di chuyển
    /// </summary>
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Nếu đã trúng rồi thì bỏ qua
        if (attackScript.hasHit) return;

        if (collision.gameObject.CompareTag(tagEnemy))
        {
            PlayerHealth enemyHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (enemyHealth != null)
            {
                Debug.Log($"[DownNormalAttack] ĐÃ TRÚNG {collision.name}!");

                // 1. Báo cáo cho script chính
                attackScript.hasHit = true;
                attackScript.enemyHit = collision.transform;

                // 2. Tự gây sát thương
                int damage = attackScript.damage;

                Vector3 knockbackDir = (collision.transform.position - parent.transform.position).normalized;
                
                // --- SỬA DÒNG NÀY ---
                enemyHealth.TakeDamage(damage, force, knockbackDir, true); // true = đòn ngã
                // --- KẾT THÚC SỬA ---
                
                // 3. Tắt detector ngay lập tức
                gameObject.SetActive(false);
            }
        }
    }
}