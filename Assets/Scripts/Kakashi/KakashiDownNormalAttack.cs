using UnityEngine;
using System.Collections;

// s+ j : dash player đến trước nếu nhận biết được đối thủ thì đánh rồi trượt ra sau đối thủ và quay mặt lại
public class KakashiDownNormalAttack : MonoBehaviour, InterfaceSkill
{
    [Header("S + J Settings")]
    public GameObject hurtBoxDetector; // GameObject dùng để phát hiện
    public int attackDamage = 15;
    public float attackCooldown = 3f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.3f; // Thời gian lướt tối đa
    public float slideDistanceBehind = 1.5f; // Khoảng cách trượt ra sau lưng
    public float slideDuration = 0.2f; // Thời gian trượt

    // --- Interface Properties ---
    public float coolDownTime { get; set; }
    public int damage { get; set; }
    public KeyCode KeyCode { get; set; } // Không dùng

    // --- Private Fields ---
    private Animator animator;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;
    private float lastAttackTime = -99f;
    private bool isAttacking = false;

    // --- Cờ (Flag) cho Coroutine ---
    [HideInInspector] public bool hasHit = false;
    [HideInInspector] public Transform enemyHit = null;

    // --- Layer Masks ---
    private int playerLayer;
    private int dashingLayer;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();

        this.coolDownTime = this.attackCooldown;
        this.damage = this.attackDamage;

        playerLayer = LayerMask.NameToLayer("Player");
        dashingLayer = LayerMask.NameToLayer("DashingPlayer");
    }

    void Start()
    {
        if (hurtBoxDetector != null)
            hurtBoxDetector.SetActive(false);
    }

    /// <summary>
    /// Được gọi bởi KakashiSkillManager
    /// </summary>
    public void Attack()
    {
        if (isAttacking || Time.time < lastAttackTime + coolDownTime)
            return; // Đang đánh hoặc đang cooldown

        StartCoroutine(DownAttackSequence());
    }

    private IEnumerator DownAttackSequence()
    {
        isAttacking = true;
        lastAttackTime = Time.time;
        hasHit = false; // Reset cờ
        enemyHit = null;

        // 1. Bắt đầu: Ngăn người chơi di chuyển, kích hoạt animation
        playerMovement.StartStun();
        animator.SetTrigger("DownNormalAttack"); // Kích hoạt animation S+J

        // 2. Lướt tới (Dash)
        float dashDir = playerMovement.isFacingRight ? 1f : -1f;
        float dashStartTime = Time.time;
        
        hurtBoxDetector.SetActive(true); // Bật detector
        rb.linearVelocity = new Vector2(dashDir * dashSpeed, 0); // Lướt

        // Chờ cho đến khi hết thời gian lướt HOẶC trúng mục tiêu
        while (Time.time < dashStartTime + dashDuration && !hasHit)
        {
            yield return null; // Chờ frame tiếp theo
        }

        // 3. Dừng lướt
        rb.linearVelocity = Vector2.zero;
        hurtBoxDetector.SetActive(false); // Tắt detector

        // 4. Kiểm tra kết quả
        if (hasHit && enemyHit != null)
        {
            // Đã trúng!
            // 5. Trượt ra sau (Slide)
            // Đổi layer để đi xuyên qua
            gameObject.layer = dashingLayer;
            foreach (Transform child in transform) child.gameObject.layer = dashingLayer;

            // Tính vị trí mục tiêu (sau lưng địch)
            float targetX = enemyHit.position.x + (dashDir * slideDistanceBehind);
            Vector2 targetPos = new Vector2(targetX, rb.position.y);
            
            // Di chuyển mượt (Lerp) đến vị trí đó
            float slideStartTime = Time.time;
            Vector2 startPos = rb.position;
            while (Time.time < slideStartTime + slideDuration)
            {
                float t = (Time.time - slideStartTime) / slideDuration;
                rb.MovePosition(Vector2.Lerp(startPos, targetPos, t));
                yield return null;
            }
            rb.MovePosition(targetPos); // Đảm bảo đến đúng vị trí
            
            // 6. Quay mặt lại
            playerMovement.transform.rotation = Quaternion.Euler(0, playerMovement.isFacingRight ? 180 : 0, 0);
            playerMovement.isFacingRight = !playerMovement.isFacingRight;
        }
        else
        {
            // Lướt trượt, không làm gì cả
        }

        // 7. Dọn dẹp
        // Đợi một chút để animation kết thúc (nếu cần)
        yield return new WaitForSeconds(0.1f); 
        
        // Trả lại layer cũ
        gameObject.layer = playerLayer;
        foreach (Transform child in transform) child.gameObject.layer = playerLayer;
        
        playerMovement.EndStun();
        isAttacking = false;
    }

    // (Các hàm interface còn lại không cần dùng)
    public void StartSkill() { }
    public void EndSkill() { }
    public void CoolDown() { }
}