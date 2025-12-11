using UnityEngine;
using System.Collections;
using UnityEngine.Playables; 

public class KakashiUpSkills : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private string enemyTag;

    // ==========================================================
    // W + J (UP NORMAL ATTACK)
    // ==========================================================
    [Header("W + J (Up Normal Attack) Settings")]
    public GameObject upNormalHurtBox;
    public int upNormalDamage = 15;
    public float upNormalCooldown = 2f;
    public float upNormalForwardForce = 3f; 
    private float upNormalLastAttackTime = -99f;
    private bool isUpNormalAttacking = false;

    // ==========================================================
    // W + U (UP LIGHT ATTACK)
    // ==========================================================
    [Header("W + U (Up Light Attack) Settings")]
    public GameObject upLightLaunchHurtBox;
    public int upLightDamage = 25;
    public float upLightCooldown = 6f;
    public float upLightTeleportDistanceBehind = 1.5f; 
    public float upLightFollowUpJumpForce = 10f; 
    [HideInInspector] public bool isUpLightFollowUpHit = false; 
    private float upLightLastAttackTime = -99f;
    private bool isUpLightAttacking = false;
    private Transform upLightLockedEnemy = null;

    // ==========================================================
    // W + I (UP HEAVY ATTACK - DRAGON)
    // ==========================================================
    [Header("W + I (Up Heavy Attack - Dragon) Settings")]
    public GameObject dragonSegmentPrefab; 
    public Transform dragonSpawnPoint;
    public int upHeavyDamage = 30;
    public float upHeavyCooldown = 7f;
    public Sprite[] dragonSprites;
    public int segmentCount = 10;
    public float spawnDelay = 0.08f;
    private float upHeavyLastAttackTime = -99f;
    private bool isUpHeavyAttacking = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();

        enemyTag = (gameObject.CompareTag("P1")) ? "P2" : "P1";
    }

    void Start()
    {
        if (upNormalHurtBox != null) upNormalHurtBox.SetActive(false);
        if (upLightLaunchHurtBox != null) upLightLaunchHurtBox.SetActive(false);
    }
    
    // ==========================================================
    // W + J (UP NORMAL ATTACK) IMPLEMENTATION
    // ==========================================================

    public void UpNormal_Attack()
    {
        if (isUpNormalAttacking || Time.time < upNormalLastAttackTime + upNormalCooldown)
            return;

        isUpNormalAttacking = true;
        upNormalLastAttackTime = Time.time;
        
        playerMovement.Stun(true); 
        animator.SetTrigger("UpNormalAttack"); 

        float dashDir = playerMovement.isFacingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(dashDir * upNormalForwardForce, rb.linearVelocity.y);
    }

    public void UpNormal_StartSkill()
    {
        if (upNormalHurtBox != null)
            upNormalHurtBox.SetActive(true);
    }

    public void UpNormal_EndSkill()
    {
        if (upNormalHurtBox != null)
            upNormalHurtBox.SetActive(false);
        
        playerMovement.EndStun(); 
        isUpNormalAttacking = false;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x * 0.1f, rb.linearVelocity.y);
    }

    // ==========================================================
    // W + U (UP LIGHT ATTACK) IMPLEMENTATION
    // ==========================================================

    public void UpLight_Attack()
    {
        if (isUpLightAttacking || Time.time < upLightLastAttackTime + upLightCooldown)
            return;

        StartCoroutine(UpLight_Sequence());
    }

    private IEnumerator UpLight_Sequence()
    {
        isUpLightAttacking = true;
        upLightLastAttackTime = Time.time;
        upLightLockedEnemy = null;
        isUpLightFollowUpHit = false;

        GameObject enemyObj = GameObject.FindGameObjectWithTag(enemyTag);
        if (enemyObj == null)
        {
            isUpLightAttacking = false;
            yield break;
        }
        upLightLockedEnemy = enemyObj.transform;
        
        playerMovement.Stun(true); 

        float originalZ = transform.position.z;
        float enemyFacingDir = (upLightLockedEnemy.localScale.x > 0) ? 1f : -1f; 
        float dirBehind = -enemyFacingDir; 
        Vector3 targetPosXY = upLightLockedEnemy.position + new Vector3(dirBehind * upLightTeleportDistanceBehind, 0.1f, 0);
        
        transform.position = new Vector3(targetPosXY.x, targetPosXY.y, originalZ);
        bool shouldFaceRight = (upLightLockedEnemy.position.x > transform.position.x);
        playerMovement.Flip(shouldFaceRight);

        yield return null;
        yield return null; 

        animator.SetTrigger("UpLightAttack");
    }

    public void UpLight_StartSkill()
    {
        if (upLightLaunchHurtBox != null)
            upLightLaunchHurtBox.SetActive(true);
    }

    public void UpLight_FollowUpJump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); 
        rb.AddForce(Vector2.up * upLightFollowUpJumpForce, ForceMode2D.Impulse);
    }

    public void UpLight_StartFollowUpHit()
    {
        isUpLightFollowUpHit = true; 
        
        if (upLightLaunchHurtBox != null)
        {
            upLightLaunchHurtBox.SetActive(true); 
        }
    }

    public void UpLight_EndSkill()
    {
        if (upLightLaunchHurtBox != null)
            upLightLaunchHurtBox.SetActive(false);
        
        isUpLightFollowUpHit = false; 
        playerMovement.EndStun(); 
        isUpLightAttacking = false;
    }

    // ==========================================================
    // W + I (UP HEAVY ATTACK) IMPLEMENTATION
    // ==========================================================

    public void UpHeavy_Attack()
    {
        if (isUpHeavyAttacking || Time.time < upHeavyLastAttackTime + upHeavyCooldown)
            return;

        isUpHeavyAttacking = true;
        upHeavyLastAttackTime = Time.time;
        
        playerMovement.Stun(true); 
        animator.SetTrigger("UpHeavyAttack");
    }

    public void UpHeavy_SpawnDragon()
    {
        if (dragonSegmentPrefab == null || dragonSpawnPoint == null) return;
        StartCoroutine(UpHeavy_SpawnDragonStream());
    }

    private IEnumerator UpHeavy_SpawnDragonStream()
    {
        bool facingRight = playerMovement.isFacingRight;
        Vector3 knockbackDir = (facingRight) ? Vector3.right : Vector3.left;

        for (int i = 0; i < segmentCount; i++)
        {
            GameObject segmentGO = Instantiate(dragonSegmentPrefab, dragonSpawnPoint.position, Quaternion.identity);
            segmentGO.tag = (gameObject.CompareTag("P1")) ? "P1Projectile" : "P2Projectile";

            DragonSegment segmentScript = segmentGO.GetComponent<DragonSegment>();
            if (segmentScript != null)
            {
                segmentScript.Initialize(
                    facingRight, 
                    enemyTag, 
                    this.upHeavyDamage, 
                    dragonSprites[i % dragonSprites.Length], 
                    knockbackDir
                );
            }
            
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void UpHeavy_EndSkill()
    {
        playerMovement.EndStun();
        isUpHeavyAttacking = false;
    }
}