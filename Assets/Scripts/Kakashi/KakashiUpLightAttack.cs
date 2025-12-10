using UnityEngine;
using System.Collections;

public class KakashiUpLightAttack : MonoBehaviour, InterfaceSkill
{
    [Header("W + U Settings")]
    public GameObject launchHurtBox; // Hurtbox dùng để hất tung
    public int attackDamage = 25;
    public float attackCooldown = 6f;
    public float teleportDistanceBehind = 1.5f; // Khoảng cách dịch chuyển
    public float followUpJumpForce = 10f; // Lực nhảy đuổi theo
    
    // --- THÊM DÒNG NÀY ---
    [HideInInspector] public bool isFollowUpHit = false; // Cờ báo hiệu Đòn 2 đang được kích hoạt
    // -----------------------
    
    // --- Interface Properties ---
    public float coolDownTime { get; set; }
    public int damage { get; set; }
    public KeyCode KeyCode { get; set; } // Không dùng

    // --- Private Fields ---
    private Animator animator;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;
    private string enemyTag;
    private float lastAttackTime = -99f;
    private bool isAttacking = false;
    private Transform lockedEnemy = null;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();

        this.coolDownTime = this.attackCooldown;
        this.damage = this.attackDamage;
        
        enemyTag = (gameObject.CompareTag("P1")) ? "P2" : "P1";
    }
    
    void Start()
    {
        if (launchHurtBox != null)
            launchHurtBox.SetActive(false);
    }

    /// <summary>
    /// Được gọi bởi KakashiSkillManager
    /// </summary>
    public void Attack()
    {
        if (isAttacking || Time.time < lastAttackTime + coolDownTime)
            return;

        StartCoroutine(UpLightAttackSequence());
    }

    private IEnumerator UpLightAttackSequence()
    {
        isAttacking = true;
        lastAttackTime = Time.time;
        lockedEnemy = null;
        isFollowUpHit = false; // <--- RESET CỜ CHO ĐÒN 1

        // 1. Tìm kẻ thù
        GameObject enemyObj = GameObject.FindGameObjectWithTag(enemyTag);
        if (enemyObj == null)
        {
            isAttacking = false;
            yield break; // Không tìm thấy địch, hủy chiêu
        }
        lockedEnemy = enemyObj.transform;
        
        playerMovement.Stun(true); // Khóa Kakashi

        // 2. Dịch chuyển ra sau
        float enemyFacingDir = (lockedEnemy.rotation.y == 0) ? 1f : -1f; 
        Vector2 teleportPosition = lockedEnemy.position + new Vector3(enemyFacingDir * teleportDistanceBehind, 0.1f, 0);
        transform.position = teleportPosition;

        // 3. Quay mặt vào địch
        bool shouldFaceRight = (lockedEnemy.position.x > transform.position.x);
        playerMovement.transform.rotation = Quaternion.Euler(0, shouldFaceRight ? 0 : 180, 0);
        playerMovement.isFacingRight = shouldFaceRight;

        // 4. Chờ 1 frame để vị trí được cập nhật trước khi đánh
        yield return null; 

        // 5. Kích hoạt animation
        animator.SetTrigger("UpLightAttack");
    }

    /// <summary>
    /// GỌI BẰNG ANIMATION EVENT: Ngay khi anim W+U bắt đầu (sau khi dịch chuyển)
    /// </summary>
    public void StartSkill()
    {
        if (launchHurtBox != null)
            launchHurtBox.SetActive(true);
    }

    /// <summary>
    /// GỌI BẰNG ANIMATION EVENT: Ngay khi đòn đánh hất tung trúng (Đòn 1)
    /// </summary>
    public void FollowUpJump()
    {
        // Bay lên theo địch
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Reset Y vel
        rb.AddForce(Vector2.up * followUpJumpForce, ForceMode2D.Impulse);
    }

    /// <summary>
    /// GỌI BẰNG ANIMATION EVENT: Tại frame Kakashi thực hiện cú đá thứ 2
    /// </summary>
    public void StartFollowUpHit() // <--- HÀM MỚI
    {
        isFollowUpHit = true; // <--- ĐẶT CỜ = TRUE
        
        // RE-ENABLE HURTBOX (Vẫn dùng Hurtbox cũ)
        if (launchHurtBox != null)
        {
            launchHurtBox.SetActive(true); 
        }
    }

    /// <summary>
    /// GỌI BẰNG ANIMATION EVENT: Khi kết thúc chuỗi animation
    /// </summary>
    public void EndSkill()
    {
        if (launchHurtBox != null)
            launchHurtBox.SetActive(false);
        
        isFollowUpHit = false; // <--- RESET CỜ VỀ FALSE KHI KẾT THÚC
        playerMovement.EndStun(); // Mở khóa Kakashi
        isAttacking = false;
    }
    
    public void CoolDown() { }
}