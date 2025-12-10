using UnityEngine;
using System.Collections;

// nút o dùng khi đang bị dính combo
public class KakashiSubstitution : MonoBehaviour
{
    [Header("Thế thân Settings")]
    public GameObject logPrefab; // Prefab khúc cây
    public float substitutionCooldown = 15f;
    public float teleportDistanceBehind = 2f; // Khoảng cách dịch chuyển ra sau lưng

    private PlayerMovement playerMovement;
    private Animator animator;
    private Rigidbody2D rb;
    private PlayerHealth playerHealth; // --- THÊM BIẾN NÀY ---
    private float lastSubTime = -99f;
    private string enemyTag;
    
    private int actionLayerIndex;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>(); // --- LẤY COMPONENT ---

        actionLayerIndex = animator.GetLayerIndex("Attack Layer");

        if (gameObject.CompareTag("P1"))
            enemyTag = "P2";
        else if (gameObject.CompareTag("P2"))
            enemyTag = "P1";
    }

    public void AttemptSubstitution()
    {
        // 1. Kiểm tra Cooldown
        if (Time.time < lastSubTime + substitutionCooldown)
        {
            Debug.Log("[Thế thân] Đang hồi chiêu!");
            return;
        }

        // 2. Kiểm tra điều kiện Stun (Dựa trên biến isStun)
        if (playerMovement == null || !playerMovement.isStun)
        {
            Debug.Log("[Thế thân] Chỉ dùng được khi đang bị dính đòn (isStun = true)!");
            return;
        }

        // 3. Thực hiện
        lastSubTime = Time.time;
        Debug.Log("[Thế thân] KAWASAKI!"); 

        // 4. Tìm kẻ thù
        GameObject enemy = GameObject.FindGameObjectWithTag(enemyTag);
        Vector2 teleportPosition;

        // 5. Spawn khúc cây tại vị trí CŨ
        if (logPrefab != null)
        {
            Instantiate(logPrefab, transform.position, transform.rotation);
        }

        if (enemy != null)
        {
            float enemyFacingDir = (enemy.transform.rotation.y == 0) ? 1f : -1f; 
            teleportPosition = enemy.transform.position + new Vector3(enemyFacingDir * teleportDistanceBehind, 0.5f, 0);
        }
        else
        {
            float myFacingDir = playerMovement.isFacingRight ? 1f : -1f;
            teleportPosition = (Vector2)transform.position - new Vector2(myFacingDir * teleportDistanceBehind, 0);
        }

        // 6. Dịch chuyển player
        transform.position = teleportPosition;

        // 7. Quay mặt vào địch
        if (enemy != null)
        {
            bool shouldFaceRight = (enemy.transform.position.x > transform.position.x);
            playerMovement.transform.rotation = Quaternion.Euler(0, shouldFaceRight ? 0 : 180, 0);
            playerMovement.isFacingRight = shouldFaceRight;
        }

        // --- 8. LOGIC QUAN TRỌNG ĐỂ SỬA LỖI ---
        // Hủy lực đẩy knockback đang chờ (nếu có)
        if (playerHealth != null)
        {
            playerHealth.CancelKnockback();
        }
        
        // Ngắt Stun và Dừng lực vật lý ngay lập tức
        playerMovement.EndStun(); 
        rb.linearVelocity = Vector2.zero; 

        // 9. Kích hoạt animation và Reset trigger bị đánh
        animator.SetTrigger("Substitution"); 
        animator.ResetTrigger("TakeDamage");
        animator.ResetTrigger("TakeDamageFall");
    }
}