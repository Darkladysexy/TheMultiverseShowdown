using UnityEngine;

public class KakashiNormalAttack : MonoBehaviour
{
    public static KakashiNormalAttack instance;
    private Animator animator;

    // Combo system
    [Header("Combo Settings")]
    public float comboResetTime = 0.8f; // Tăng thời gian reset lên một chút
    
    private int comboStep = 0;
    private float lastAttackTime = 0f;
    private bool isAttacking = false;
    private bool attackQueued = false; // QUAN TRỌNG: Cờ "xếp hàng" đòn đánh

    // Damage values
    [Header("Damage")]
    public int normalAttack1_Damage = 10;
    public int normalAttack2_Damage = 15;
    public int normalAttack3_Damage = 20;

    // HurtBoxes
    [Header("HurtBoxes")]
    public GameObject normalAttack1_HurtBox;
    public GameObject normalAttack2_HurtBox;
    public GameObject normalAttack3_HurtBox;

    // (Không cần 3 cờ isNormalAttack 1/2/3 nữa, vì comboStep là đủ)
    
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        if (normalAttack1_HurtBox != null) normalAttack1_HurtBox.SetActive(false);
        if (normalAttack2_HurtBox != null) normalAttack2_HurtBox.SetActive(false);
        if (normalAttack3_HurtBox != null) normalAttack3_HurtBox.SetActive(false);
    }

    void Update()
    {
        // Luôn kiểm tra để reset combo nếu chờ quá lâu
        if (!isAttacking && Time.time - lastAttackTime > comboResetTime)
        {
            comboStep = 0;
        }
    }

    /// <summary>
    /// Được gọi bởi SkillManager MỖI KHI nhấn J
    /// </summary>
    public void HandleNormalAttack()
    {
        if (!isAttacking)
        {
            // 1. CHƯA TẤN CÔNG: Bắt đầu đòn đánh
            isAttacking = true;
            attackQueued = false; // Xóa hàng chờ
            
            // Kiểm tra xem combo có bị reset do chờ lâu không
            if (Time.time - lastAttackTime > comboResetTime)
            {
                comboStep = 0;
            }

            comboStep++;
            
            if (comboStep == 1)
            {
                animator.SetTrigger("NormalAttack1");
            }
            else if (comboStep == 2)
            {
                animator.SetTrigger("NormalAttack2");
            }
            else if (comboStep == 3)
            {
                animator.SetTrigger("NormalAttack3");
                comboStep = 0; // Reset sau đòn 3
            }
        }
        else
        {
            // 2. ĐANG TẤN CÔNG: "Xếp hàng" (buffer) đòn tiếp theo
            if (comboStep != 0) // Đừng xếp hàng nếu vừa xong đòn 3
            {
                attackQueued = true;
            }
        }
    }

    /// <summary>
    /// Được gọi bởi Animation Event khi animation KẾT THÚC
    /// </summary>
    public void AttackFinished()
    {
        lastAttackTime = Time.time; // Cập nhật thời gian
        isAttacking = false;
        
        // Tắt tất cả hurtbox để an toàn
        EndNormalAttack1();
        EndNormalAttack2();
        EndNormalAttack3();

        // KIỂM TRA HÀNG CHỜ
        if (attackQueued)
        {
            attackQueued = false;
            HandleNormalAttack(); // Gọi ngay đòn tiếp theo
        }
    }

    public int GetDamageForComboStep(int step)
    {
        // Cập nhật hàm này để dùng comboStep
        if (step == 1) return normalAttack1_Damage;
        if (step == 2) return normalAttack2_Damage;
        if (step == 3) return normalAttack3_Damage;
        return 0;
    }

    // (Các hàm Start/End NormalAttack 1/2/3 giữ nguyên)
    public void StartNormalAttack1()
    {
        if (normalAttack1_HurtBox != null) normalAttack1_HurtBox.SetActive(true);
    }

    public void EndNormalAttack1()
    {
        if (normalAttack1_HurtBox != null) normalAttack1_HurtBox.SetActive(false);
    }

    public void StartNormalAttack2()
    {
        if (normalAttack2_HurtBox != null) normalAttack2_HurtBox.SetActive(true);
    }

    public void EndNormalAttack2()
    {
        if (normalAttack2_HurtBox != null) normalAttack2_HurtBox.SetActive(false);
    }

    public void StartNormalAttack3()
    {
        if (normalAttack3_HurtBox != null) normalAttack3_HurtBox.SetActive(true);
    }

    public void EndNormalAttack3()
    {
        if (normalAttack3_HurtBox != null) normalAttack3_HurtBox.SetActive(false);
    }
}