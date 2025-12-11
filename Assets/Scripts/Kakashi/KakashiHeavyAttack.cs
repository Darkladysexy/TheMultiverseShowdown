using UnityEngine;
using UnityEngine.Playables; // [QUAN TRỌNG] Thêm thư viện này

public class KakashiHeavyAttack : MonoBehaviour, InterfaceSkill
{
    public static KakashiHeavyAttack instance;

    public float coolDownTime { get; set; }
    public int damage { get; set; }
    public KeyCode KeyCode { get; set; } // Không dùng

    public GameObject chidoriEffectPrefab;
    public Transform chidoriSpawnPoint;
    public float dashForce = 15f;

    [Header("Timeline Effect")]
    public PlayableDirector cutInTimeline; // [MỚI] Biến chứa Timeline (kéo thả vào Inspector)

    private Animator animator;
    private Rigidbody2D rb;
    private bool isHeavyAttacking = false;
    public GameObject chidoriHurtBox;
    
    private LegPlayer legPlayer;
    private PlayerMovement playerMovement;

    private KakashiNonNormalDamageHandler sendDamageHandler;

    void Awake()
    {
        instance = this;
        damage = 35;
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
            sendDamageHandler = chidoriHurtBox.GetComponent<KakashiNonNormalDamageHandler>();
            if (sendDamageHandler == null)
            {
                sendDamageHandler = chidoriHurtBox.AddComponent<KakashiNonNormalDamageHandler>();
            }
            
            if (sendDamageHandler != null)
                sendDamageHandler.attackMode = KakashiDamageMode.HeavyChidori_I;

            chidoriHurtBox.SetActive(false);
        }
        
        // [MỚI] Đảm bảo Timeline dừng lúc đầu
        if (cutInTimeline != null) cutInTimeline.Stop();
    }

    public void Attack()
    {
        if (!isHeavyAttacking)
        {
            // [MỚI] Chạy Timeline hiện "Nộ" ngay khi bấm nút
            if (cutInTimeline != null) 
            {
                cutInTimeline.Play();
            }

            animator.SetTrigger("HeavyAttack");
            isHeavyAttacking = true;
        }
    }
    
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

    public void StartSkill()
    {
        if (playerMovement != null)
            playerMovement.Stun(true);
        
        if (chidoriHurtBox != null)
            chidoriHurtBox.SetActive(true); 
        
        if (rb != null && playerMovement != null)
        {
            if (playerMovement.isFacingRight)
                rb.AddForce(Vector2.right * dashForce, ForceMode2D.Impulse);
            else
                rb.AddForce(Vector2.left * dashForce, ForceMode2D.Impulse);
        }
    }

    public void EndSkill()
    {
        if (playerMovement != null)
            playerMovement.EndStun();
        
        if (rb != null)
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        
        if (sendDamageHandler != null)
        {
            sendDamageHandler.ApplyFinalKnockback_HeavyChidori();
        }

        if (chidoriHurtBox != null)
            chidoriHurtBox.SetActive(false);
        
        isHeavyAttacking = false;
    }

    public void CoolDown() { }
}