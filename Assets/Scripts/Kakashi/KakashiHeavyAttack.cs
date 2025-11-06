using UnityEngine;

public class KakashiHeavyAttack : MonoBehaviour, InterfaceSkill
{
    public static KakashiHeavyAttack instance;

    public float coolDownTime { get; set; }
    public int damage { get; set; }
    public KeyCode KeyCode { get; set; }

    public GameObject chidoriEffectPrefab;
    public Transform chidoriSpawnPoint;
    public float dashSpeed = 15f;
    public float dashDuration = 0.5f;

    private Animator animator;
    private Rigidbody2D rb;
    private bool isHeavyAttacking = false;
    private bool isDashing = false;
    public GameObject chidoriHurtBox;
    
    private LegPlayer legPlayer;
    private PlayerMovement playerMovement;

    void Awake()
    {
        instance = this;
        damage = 35;
        KeyCode = gameObject.CompareTag("P1") ? KeyCode.I : KeyCode.Keypad5;
        
        legPlayer = GetComponent<LegPlayer>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (chidoriHurtBox != null)
            chidoriHurtBox.SetActive(false);
    }

    public void Attack()
    {
        bool isGrounded = legPlayer != null ? legPlayer.isGrounded : false;
        
        if (Input.GetKeyDown(KeyCode) && isGrounded && !isHeavyAttacking)
        {
            Debug.Log("[HEAVY ATTACK] Attack() called!");
            animator.SetTrigger("HeavyAttack");
            isHeavyAttacking = true;
        }
    }

    public void TriggerHeavyAttackStart()
    {
        StartSkill();
    }

    public void TriggerHeavyAttackEnd()
    {
        EndSkill();
    }

    public void TriggerHeavyAttackCoolDown()
    {
        CoolDown();
    }

    public void StartSkill()
    {
        if (chidoriEffectPrefab != null && chidoriSpawnPoint != null)
        {
            Instantiate(chidoriEffectPrefab, chidoriSpawnPoint.position, Quaternion.identity, transform);
        }

        if (chidoriHurtBox != null)
            chidoriHurtBox.SetActive(true);

        StartCoroutine(ChidoriDash());
    }

    private System.Collections.IEnumerator ChidoriDash()
    {
        isDashing = true;
        float elapsed = 0f;
        float direction = playerMovement != null && playerMovement.isFacingRight ? 1f : -1f;

        while (elapsed < dashDuration)
        {
            rb.linearVelocity = new Vector2(direction * dashSpeed, rb.linearVelocity.y);
            elapsed += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    public void EndSkill()
    {
        if (chidoriHurtBox != null)
            chidoriHurtBox.SetActive(false);
        isHeavyAttacking = false;
    }

    public void CoolDown()
    {
        // Implement cooldown if needed
    }
}
