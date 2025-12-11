using UnityEngine;
using System.Collections;

public class KakashiAirSkills : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;
    private string enemyTag;

    // ==========================================================
    // AIR + J (AIR NORMAL ATTACK)
    // ==========================================================
    [Header("Air + J (Air Normal Attack) Settings")]
    public GameObject airNormalHurtBox; 
    public int airNormalDamage = 12;
    public float airNormalCooldown = 1f; 
    private float airNormalLastAttackTime = -99f;

    // ==========================================================
    // AIR + U (AERIAL ATTACK - KUNAI)
    // ==========================================================
    [Header("Air + U (Aerial Attack - Kunai) Settings")]
    public GameObject kunaiPrefab;
    public Transform kunaiSpawnPoint;
    public int kunaiCount = 3;
    public float kunaiDelay = 0.15f;
    public int aerialDamage = 10;
    public float aerialCooldown = 2f;
    private float aerialLastAttackTime = -99f;
    private bool isAerialAttacking = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        enemyTag = (gameObject.CompareTag("P1")) ? "P2" : "P1";
    }

    void Start()
    {
        if (airNormalHurtBox != null) airNormalHurtBox.SetActive(false);
        if (kunaiSpawnPoint == null) Debug.LogWarning("KakashiAirSkills: Thiếu Kunai Spawn Point!");
    }

    // ==========================================================
    // AIR + J (AIR NORMAL ATTACK) IMPLEMENTATION
    // ==========================================================

    public void AirNormal_Attack()
    {
        if (Time.time >= airNormalLastAttackTime + airNormalCooldown)
        {
            airNormalLastAttackTime = Time.time;
            animator.SetTrigger("AirNormalAttack"); 
        }
    }

    public void AirNormal_StartSkill()
    {
        if (airNormalHurtBox != null)
            airNormalHurtBox.SetActive(true);
    }

    public void AirNormal_EndSkill()
    {
        if (airNormalHurtBox != null)
            airNormalHurtBox.SetActive(false);
    }

    // ==========================================================
    // AIR + U (AERIAL ATTACK) IMPLEMENTATION
    // ==========================================================
    
    public void Aerial_Attack()
    {
        if (!isAerialAttacking && Time.time >= aerialLastAttackTime + aerialCooldown)
        {
            animator.SetTrigger("AerialAttack");
            isAerialAttacking = true;
            aerialLastAttackTime = Time.time;
        }
    }

    public void Aerial_StartSkill()
    {
        StartCoroutine(Aerial_ThrowKunaiSequence());
    }

    private IEnumerator Aerial_ThrowKunaiSequence()
    {
        if (kunaiCount > 0)
        {
            for (int i = 0; i < kunaiCount; i++)
            {
                yield return new WaitForSeconds(kunaiDelay);

                if (kunaiPrefab == null || kunaiSpawnPoint == null) continue;
                
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

    public void Aerial_EndSkill()
    {
        isAerialAttacking = false;
    }
}