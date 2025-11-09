using System.Collections;
using UnityEngine;

public class KakashiLightAttack : MonoBehaviour, InterfaceSkill
{
    public static KakashiLightAttack instance;

    public float coolDownTime { get; set; }
    public int damage { get; set; }
    public KeyCode KeyCode { get; set; } // Thuộc tính này vẫn tồn tại do Interface, nhưng chúng ta không dùng

    // Kunai projectile settings
    public GameObject kunaiPrefab;
    public Transform kunaiSpawnPoint;
    public int kunaiCount = 4;
    public float kunaiDelay = 0.1f;

    private Animator animator;
    private bool isLightAttacking = false;
    private float currentCooldown = 0f;
    
    private LegPlayer legPlayer;
    private PlayerMovement playerMovement;

    void Awake()
    {
        instance = this;
        damage = 8;
        coolDownTime = 1.5f;
        // KHÔNG CẦN GÁN KEYCODE Ở ĐÂY NỮA
        // KeyCode = gameObject.CompareTag("P1") ? KeyCode.U : KeyCode.Keypad4; 
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        foreach(Transform child in this.gameObject.transform)
        {
            if(child.gameObject.name == "Leg")
            {
                legPlayer = child.gameObject.GetComponent<LegPlayer>();
            }
        }
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (currentCooldown > 0)
            currentCooldown -= Time.deltaTime;
    }

    // Hàm Attack này đã được sửa, không kiểm tra input
    public void Attack()
    {
        // SkillManager đã kiểm tra input và isGrounded
        if (!isLightAttacking && currentCooldown <= 0)
        {
            animator.SetTrigger("LightAttack");
            isLightAttacking = true;
            currentCooldown = coolDownTime;
        }
    }

    public void StartSkill()
    {
        Debug.Log("[LIGHT ATTACK] StartSkill() called - Spawning horizontal kunai!");
        StartCoroutine(ThrowKunaiSequence());
    }

    private IEnumerator ThrowKunaiSequence()
    {
        for (int i = 0; i < kunaiCount; i++)
        {
            yield return new WaitForSeconds(kunaiDelay);
            ThrowKunai();
        }
    }

    private void ThrowKunai()
    {
        if (kunaiPrefab != null && kunaiSpawnPoint != null)
        {
            GameObject kunai = Instantiate(kunaiPrefab, kunaiSpawnPoint.position, Quaternion.identity);

            // Set tag
            if (gameObject.CompareTag("P1"))
                kunai.tag = "P1Projectile";
            else if (gameObject.CompareTag("P2"))
                kunai.tag = "P2Projectile";

            KakashiKunai kunaiScript = kunai.GetComponent<KakashiKunai>();
            if (kunaiScript != null)
            {
                bool facingRight = playerMovement != null ? playerMovement.isFacingRight : true;
                kunaiScript.SetDamage(damage);
                kunaiScript.Initialize(facingRight);
            }
        }
    }

    public void EndSkill()
    {
        isLightAttacking = false;
    }

    public void CoolDown()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown < 0)
            currentCooldown = 0;
    }
}