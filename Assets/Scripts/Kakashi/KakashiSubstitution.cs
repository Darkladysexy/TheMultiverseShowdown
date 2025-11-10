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
    private float lastSubTime = -99f;
    private string enemyTag;
    
    private int actionLayerIndex; // Index của "Attack Layer"

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        actionLayerIndex = animator.GetLayerIndex("Attack Layer");

        if (gameObject.CompareTag("P1"))
            enemyTag = "P2";
        else if (gameObject.CompareTag("P2"))
            enemyTag = "P1";
    }

    /// <summary>
    /// Được gọi bởi KakashiSkillManager khi nhấn phím O
    /// </summary>
    public void AttemptSubstitution()
    {
        // 1. Kiểm tra Cooldown
        if (Time.time < lastSubTime + substitutionCooldown)
        {
            Debug.Log("[Thế thân] Đang hồi chiêu!");
            return;
        }

        // 2. === SỬA LOGIC KIỂM TRA ===
        // Kiểm tra trực tiếp biến 'isStun' thay vì tên animation
        // Hàm Stun() trong PlayerMovement đã đặt isStun = true ngay lập tức
        if (playerMovement == null || !playerMovement.isStun)
        {
            Debug.Log("[Thế thân] Chỉ dùng được khi đang bị dính đòn (isStun = true)!");
            return;
        }
        // ============================

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
            // Tìm vị trí sau lưng địch
            float enemyFacingDir = (enemy.transform.rotation.y == 0) ? 1f : -1f; 
            teleportPosition = enemy.transform.position + new Vector3(enemyFacingDir * teleportDistanceBehind, 0.5f, 0);
        }
        else
        {
            // Không tìm thấy địch, dịch chuyển lùi 1 đoạn
            float myFacingDir = playerMovement.isFacingRight ? 1f : -1f;
            teleportPosition = (Vector2)transform.position - new Vector2(myFacingDir * teleportDistanceBehind, 0);
        }

        // 6. Dịch chuyển player
        transform.position = teleportPosition;

        // 7. Quay mặt vào địch (nếu có)
        if (enemy != null)
        {
            bool shouldFaceRight = (enemy.transform.position.x > transform.position.x);
            playerMovement.transform.rotation = Quaternion.Euler(0, shouldFaceRight ? 0 : 180, 0);
            playerMovement.isFacingRight = shouldFaceRight;
        }

        // 8. Ngắt Stun (Rất quan trọng)
        playerMovement.EndStun(); // Gọi hàm này để reset isStun = false
        rb.linearVelocity = Vector2.zero; // Dừng mọi lực tác động (ngăn bị văng tiếp)

        // 9. Kích hoạt animation (Poof!) và ngắt anim bị đánh
        animator.SetTrigger("Substitution"); 
        
        // Reset các trigger bị đánh để ngừng anim "TakeDamage"
        animator.ResetTrigger("TakeDamage");
        animator.ResetTrigger("TakeDamageFall");
    }
}