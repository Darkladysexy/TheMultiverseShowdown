using UnityEngine;

public class KakashiHeavyAttack : MonoBehaviour, InterfaceSkill
{
    public static KakashiHeavyAttack instance;

    public float coolDownTime { get; set; }
    public int damage { get; set; }
    public KeyCode KeyCode { get; set; } // Không dùng

    public GameObject chidoriEffectPrefab;
    public Transform chidoriSpawnPoint;
    public float dashForce = 15f;

    private Animator animator;
    private Rigidbody2D rb;
    private bool isHeavyAttacking = false;
    public GameObject chidoriHurtBox;
    
    private LegPlayer legPlayer;
    private PlayerMovement playerMovement;

    // --- THÊM BIẾN NÀY ---
    private SendDamageKakashiHeavy sendDamageScript; // Script "tay" để gây sát thương

    void Awake()
    {
        instance = this;
        damage = 35;
        // (Không gán KeyCode ở đây nữa)
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        
        foreach(Transform child in this.gameObject.transform)
        {
            if(child.gameObject.name == "Leg")
            {
                legPlayer = child.gameObject.GetComponent<LegPlayer>();
                break;
            }
        }
        
        if (chidoriHurtBox != null)
        {
            // --- THÊM DÒNG NÀY ---
            // Lấy script gây sát thương từ hurtbox
            sendDamageScript = chidoriHurtBox.GetComponent<SendDamageKakashiHeavy>();
            if (sendDamageScript == null)
            {
                Debug.LogError("LỖI: chidoriHurtBox bị thiếu script SendDamageKakashiHeavy!");
            }

            chidoriHurtBox.SetActive(false);
        }
    }

    // Hàm Attack này đã được sửa, không kiểm tra input
    public void Attack()
    {
        if (!isHeavyAttacking)
        {
            animator.SetTrigger("HeavyAttack");
            isHeavyAttacking = true;
        }
    }
    
    // (Các hàm Trigger... giữ nguyên)
    public void TriggerChidoriEndDash()
    {
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }
    public void TriggerSpawnChidori()
    {
        if (chidoriEffectPrefab != null && chidoriSpawnPoint != null)
        {
            Instantiate(chidoriEffectPrefab, chidoriSpawnPoint.position, Quaternion.identity, transform);
        }
    }

    // ============ INTERFACE IMPLEMENTATION ============

    // Được gọi bởi Event "TriggerHeavyAttackStart"
    public void StartSkill()
    {
        if (playerMovement != null)
            playerMovement.Stun(true);
        
        // --- SỬA KHỐI NÀY ---
        if (sendDamageScript != null)
            chidoriHurtBox.SetActive(true); // Bật Hurtbox để FixedUpdate bắt đầu chạy
        
        if (rb != null && playerMovement != null)
        {
            if (playerMovement.isFacingRight)
                rb.AddForce(Vector2.right * dashForce, ForceMode2D.Impulse);
            else
                rb.AddForce(Vector2.left * dashForce, ForceMode2D.Impulse);
        }
    }

    // Được gọi bởi Event "TriggerHeavyAttackEnd"
    public void EndSkill()
    {
        if (playerMovement != null)
            playerMovement.EndStun();
        
        if (rb != null)
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        
        // --- LOGIC MỚI ---
        // 1. GỌI HÀM GÂY VĂNG
        if (sendDamageScript != null)
        {
            sendDamageScript.ApplyFinalKnockback();
        }

        // 2. TẮT HURTBOX (SAU KHI GÂY VĂNG)
        if (chidoriHurtBox != null)
            chidoriHurtBox.SetActive(false);
        
        isHeavyAttacking = false;
    }

    public void CoolDown()
    {
        // (Không cần dùng)
    }
}