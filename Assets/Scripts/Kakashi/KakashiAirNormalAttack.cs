using UnityEngine;
// air + j : đánh 1 phát ở trên không
public class KakashiAirNormalAttack : MonoBehaviour, InterfaceSkill
{
    [Header("Air Normal Attack Settings")]
    public GameObject hurtBox; // Hurtbox cho đòn đánh
    public int attackDamage = 12;
    public float attackCooldown = 1f; // Cooldown 1 giây

    // --- Interface Properties ---
    public float coolDownTime { get; set; }
    public int damage { get; set; }
    public KeyCode KeyCode { get; set; } // Không dùng

    // --- Private Fields ---
    private Animator animator;
    private float lastAttackTime = -99f; // Đặt thời gian hồi chiêu âm để có thể dùng ngay

    void Awake()
    {
        animator = GetComponent<Animator>();
        // Gán giá trị từ public fields sang properties của interface
        this.coolDownTime = this.attackCooldown;
        this.damage = this.attackDamage;
    }

    void Start()
    {
        if (hurtBox != null)
            hurtBox.SetActive(false); // Đảm bảo hurtbox tắt khi bắt đầu
    }

    /// <summary>
    /// Được gọi bởi KakashiSkillManager khi nhấn J trên không
    /// </summary>
    public void Attack()
    {
        // Kiểm tra cooldown
        if (Time.time >= lastAttackTime + coolDownTime)
        {
            lastAttackTime = Time.time;
            animator.SetTrigger("AirNormalAttack"); // Kích hoạt animation
            Debug.Log("[KakashiAirNormalAttack] Kích hoạt!");
        }
    }

    /// <summary>
    /// Được gọi bởi Animation Event để BẬT hurtbox
    /// </summary>
    public void StartSkill()
    {
        if (hurtBox != null)
            hurtBox.SetActive(true);
    }

    /// <summary>
    /// Được gọi bởi Animation Event để TẮT hurtbox
    /// </summary>
    public void EndSkill()
    {
        if (hurtBox != null)
            hurtBox.SetActive(false);
    }

    /// <summary>
    /// (Bắt buộc bởi Interface)
    /// </summary>
    public void CoolDown()
    {
        // Cooldown được xử lý trong hàm Attack()
    }
}