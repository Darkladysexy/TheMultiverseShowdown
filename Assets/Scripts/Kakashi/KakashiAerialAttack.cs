using System.Collections;
using UnityEngine;

public class KakashiAerialAttack : MonoBehaviour, InterfaceSkill
{
    public static KakashiAerialAttack instance;

    public float coolDownTime { get; set; }
    public int damage { get; set; }
    public KeyCode KeyCode { get; set; } // Không dùng

    public GameObject kunaiPrefab;
    public Transform kunaiSpawnPoint;
    public int kunaiCount = 3;
    public float kunaiDelay = 0.15f;
    private int aerialDamage = 10;

    private Animator animator;
    private bool isAerialAttacking = false;
    private float currentCooldown = 0f;
    private LegPlayer legPlayer;
    private PlayerMovement playerMovement;

    void Awake()
    {
        instance = this;
        damage = 12;
        coolDownTime = 2f;
        // KHÔNG CẦN GÁN KEYCODE Ở ĐÂY NỮA (Phím O/Keypad6 đã sai)
        // KeyCode = gameObject.CompareTag("P1") ? KeyCode.O : KeyCode.Keypad6; 
        
        foreach(Transform child in this.gameObject.transform)
        {
            if(child.gameObject.name == "Leg")
            {
                legPlayer = child.gameObject.GetComponent<LegPlayer>();
            }
        }
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (currentCooldown > 0)
            currentCooldown -= Time.deltaTime;
    }

    // Hàm Attack này đã được sửa, không kiểm tra input
    public void Attack()
    {
        // SkillManager đã kiểm tra input và !isGrounded
        if (!isAerialAttacking && currentCooldown <= 0)
        {
            animator.SetTrigger("AerialAttack");
            isAerialAttacking = true;
            currentCooldown = coolDownTime;
        }
    }

    public void StartSkill()
    {
        Debug.Log("[AERIAL ATTACK] StartSkill() called - Spawning diagonal kunai!");
        StartCoroutine(ThrowKunaiSequence());
    }

    private IEnumerator ThrowKunaiSequence()
    {
        if (kunaiCount > 0)
        {
            for (int i = 0; i < kunaiCount; i++)
            {
                yield return new WaitForSeconds(kunaiDelay);

                GameObject kunai = Instantiate(kunaiPrefab, kunaiSpawnPoint.position, Quaternion.identity);

                if (gameObject.CompareTag("P1"))
                    kunai.tag = "P1Projectile";
                else if (gameObject.CompareTag("P2"))
                    kunai.tag = "P2Projectile";

                KakashiKunai kunaiScript = kunai.GetComponent<KakashiKunai>();

                if (kunaiScript != null)
                {
                    bool facingRight = playerMovement != null ? playerMovement.isFacingRight : true;
                    kunaiScript.SetDamage(aerialDamage);
                    kunaiScript.InitializeAerial(facingRight);
                }
            }
        }
    }

    public void EndSkill()
    {
        isAerialAttacking = false;
    }

    public void CoolDown()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown < 0)
            currentCooldown = 0;
    }
}