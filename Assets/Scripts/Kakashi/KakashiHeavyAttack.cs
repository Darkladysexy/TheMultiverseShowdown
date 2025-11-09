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

    void Awake()
    {
        instance = this;
        damage = 35;
        // KHÔNG CẦN GÁN KEYCODE Ở ĐÂY NỮA
        // KeyCode = gameObject.CompareTag("P1") ? KeyCode.I : KeyCode.Keypad5;
    }

    void Start()
    {
        Debug.Log("[HEAVY ATTACK] Start() called");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        
        foreach(Transform child in this.gameObject.transform)
        {
            if(child.gameObject.name == "Leg")
            {
                legPlayer = child.gameObject.GetComponent<LegPlayer>();
                Debug.Log("[HEAVY ATTACK] Found Leg child and got LegPlayer component");
                break;
            }
        }
        
        if (chidoriHurtBox != null)
            chidoriHurtBox.SetActive(false);
    }

    // Hàm Attack này đã được sửa, không kiểm tra input
    public void Attack()
    {
        // SkillManager đã kiểm tra input và isGrounded
        if (!isHeavyAttacking)
        {
            Debug.Log("[HEAVY ATTACK] Attack() called!");
            animator.SetTrigger("HeavyAttack");
            isHeavyAttacking = true;
        }
    }
    
    // ============ ANIMATION EVENTS (50%, 70%, 90%, 100%) ============

    

    // 70%: TriggerChidoriEndDash
    public void TriggerChidoriEndDash()
    {
        Debug.Log("[HEAVY ATTACK] 70% - TriggerChidoriEndDash");
        
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            Debug.Log("[HEAVY ATTACK] Force stopped velocity at 70%");
        }
    }

    // 90%: TriggerSpawnChidori
    public void TriggerSpawnChidori()
    {
        Debug.Log("[HEAVY ATTACK] 90% - TriggerSpawnChidori");
        
        if (chidoriEffectPrefab != null && chidoriSpawnPoint != null)
        {
            Instantiate(chidoriEffectPrefab, chidoriSpawnPoint.position, Quaternion.identity, transform);
        }
    }

    

    // ============ INTERFACE IMPLEMENTATION ============

    public void StartSkill()
    {
        Debug.Log("[HEAVY ATTACK] StartSkill() - Disable movement + Activate hurtbox + AddForce");
        
        if (playerMovement != null)
        {
            playerMovement.StartStun();
            Debug.Log("[HEAVY ATTACK] PlayerMovement DISABLED");
        }
        
        if (chidoriHurtBox != null)
            chidoriHurtBox.SetActive(true);
        
        if (rb != null && playerMovement != null)
        {
            if (playerMovement.isFacingRight)
                rb.AddForce(Vector2.right * dashForce, ForceMode2D.Impulse);
            else
                rb.AddForce(Vector2.left * dashForce, ForceMode2D.Impulse);
            
            Debug.Log($"[HEAVY ATTACK] AddForce applied: {dashForce}");
        }
    }

    public void EndSkill()
    {
        Debug.Log("[HEAVY ATTACK] EndSkill() - Re-enable movement + Deactivate hurtbox");
        
        if (playerMovement != null)
        {
            playerMovement.EndStun();
            Debug.Log("[HEAVY ATTACK] PlayerMovement RE-ENABLED");
        }
        
        if (rb != null)
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        
        if (chidoriHurtBox != null)
            chidoriHurtBox.SetActive(false);
        
        isHeavyAttacking = false;
    }

    public void CoolDown()
    {
        Debug.Log("[HEAVY ATTACK] CoolDown()");
    }
}