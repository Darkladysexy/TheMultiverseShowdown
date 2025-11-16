using UnityEngine;
using System.Collections;

public class KakashiUpNormalAttack : MonoBehaviour, InterfaceSkill
{
    [Header("W + J Settings")]
    public GameObject hurtBox;
    public int attackDamage = 15;
    public float attackCooldown = 2f;
    public float forwardForce = 3f; // Lực đẩy Kakashi

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

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();

        this.coolDownTime = this.attackCooldown;
        this.damage = this.attackDamage;
    }

    void Start()
    {
        if (hurtBox != null)
            hurtBox.SetActive(false);
    }

    /// <summary>
    /// Được gọi bởi KakashiSkillManager
    /// </summary>
    public void Attack()
    {
        if (isAttacking || Time.time < lastAttackTime + coolDownTime)
            return;

        isAttacking = true;
        lastAttackTime = Time.time;
        
        playerMovement.Stun(true); // Khóa di chuyển
        animator.SetTrigger("UpNormalAttack"); // Kích hoạt animation W+J

        // Thêm lực đẩy tức thì
        float dashDir = playerMovement.isFacingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(dashDir * forwardForce, rb.linearVelocity.y);
    }

    /// <summary>
    /// GỌI BẰNG ANIMATION EVENT: Khi bắt đầu vung tay
    /// </summary>
    public void StartSkill()
    {
        if (hurtBox != null)
            hurtBox.SetActive(true);
        
        // (Bạn cần 1 script SendDamage... cho hurtBox này, 
        // tương tự SendDamageKakashiNormalAttack,
        // để nó có thể gây 'damage' và 'isHeavyHit = false')
    }

    /// <summary>
    /// GỌI BẰNG ANIMATION EVENT: Khi kết thúc animation
    /// </summary>
    public void EndSkill()
    {
        if (hurtBox != null)
            hurtBox.SetActive(false);
        
        playerMovement.EndStun(); // Trả lại điều khiển
        isAttacking = false;
        
        // Hãm bớt lực đẩy
        rb.linearVelocity = new Vector2(rb.linearVelocity.x * 0.1f, rb.linearVelocity.y);
    }
    
    public void CoolDown() { }
}