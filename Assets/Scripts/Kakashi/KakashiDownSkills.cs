using UnityEngine;
using System.Collections;
using UnityEngine.Playables;

public class KakashiDownSkills : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private string enemyTag;

    // --- Layer Masks ---
    private int playerLayer;
    private int dashingLayer;

    // ==========================================================
    // S + J (DOWN NORMAL ATTACK - DASH THROUGH)
    // ==========================================================
    [Header("S + J (Down Normal Attack) Settings")]
    public GameObject downNormalHurtBoxDetector; 
    public int downNormalDamage = 15;
    public float downNormalCooldown = 3f;
    public float downNormalDashSpeed = 10f;
    public float downNormalDashDuration = 0.3f; 
    public float downNormalSlideDistanceBehind = 1.5f; 
    public float downNormalSlideDuration = 0.2f; 
    private float downNormalLastAttackTime = -99f;
    private bool isDownNormalAttacking = false;
    
    [HideInInspector] public bool downNormalHasHit = false;
    [HideInInspector] public Transform downNormalEnemyHit = null;

    // ==========================================================
    // S + U (DOWN LIGHT ATTACK - NINKEN)
    // ==========================================================
    [Header("S + U (Down Light Attack - Ninken) Settings")]
    public GameObject ninkenPrefab; 
    public Transform ninkenSummonSpawnPoint; 
    public int downLightDamage = 20;
    public float downLightCooldown = 5f;
    public float ninkenJumpForce = 5f; 
    public float ninkenSpeed = 7f;     
    private float downLightLastAttackTime = -99f;
    private bool isDownLightAttacking = false;

    // ==========================================================
    // S + I (DOWN HEAVY ATTACK - SPECIAL)
    // ==========================================================
    [Header("S + I (Down Heavy Attack - Special) Settings")]
    public GameObject explosionPrefab;   
    public PlayableDirector cutInTimeline; 
    public AudioClip angerSound;         
    public int downHeavyTotalDamage = 50; 
    public float downHeavyCooldown = 10f;
    public float cutInDuration = 1.5f;   
    public float holdDuration = 2f;      
    public float tickRate = 0.2f;        
    public float finalKnockback = 5f;    
    private float downHeavyLastAttackTime = -99f;
    private bool isDownHeavyAttacking = false;
    private PlayerHealth downHeavyLockedEnemy = null; 

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();

        enemyTag = (gameObject.CompareTag("P1")) ? "P2" : "P1";
        playerLayer = LayerMask.NameToLayer("Player");
        dashingLayer = LayerMask.NameToLayer("DashingPlayer");
    }

    void Start()
    {
        if (downNormalHurtBoxDetector != null) downNormalHurtBoxDetector.SetActive(false);
        if (cutInTimeline != null) cutInTimeline.Stop(); 
    }

    // ==========================================================
    // S + J (DOWN NORMAL ATTACK) IMPLEMENTATION
    // ==========================================================

    public void DownNormal_Attack()
    {
        if (isDownNormalAttacking || Time.time < downNormalLastAttackTime + downNormalCooldown)
            return;

        StartCoroutine(DownNormal_Sequence());
    }

    private IEnumerator DownNormal_Sequence()
    {
        isDownNormalAttacking = true;
        downNormalLastAttackTime = Time.time;
        downNormalHasHit = false; 
        downNormalEnemyHit = null;

        playerMovement.Stun(true);
        animator.SetTrigger("DownNormalAttack"); 

        float dashDir = playerMovement.isFacingRight ? 1f : -1f;
        float dashStartTime = Time.time;
        
        if (downNormalHurtBoxDetector != null) downNormalHurtBoxDetector.SetActive(true);
        rb.linearVelocity = new Vector2(dashDir * downNormalDashSpeed, 0); 

        while (Time.time < dashStartTime + downNormalDashDuration && !downNormalHasHit)
        {
            yield return null;
        }

        rb.linearVelocity = Vector2.zero;
        if (downNormalHurtBoxDetector != null) downNormalHurtBoxDetector.SetActive(false);

        if (downNormalHasHit && downNormalEnemyHit != null)
        {
            gameObject.layer = dashingLayer;
            foreach (Transform child in transform) child.gameObject.layer = dashingLayer;

            float targetX = downNormalEnemyHit.position.x + (dashDir * downNormalSlideDistanceBehind);
            Vector2 targetPos = new Vector2(targetX, rb.position.y);
            
            float slideStartTime = Time.time;
            Vector2 startPos = rb.position;
            while (Time.time < slideStartTime + downNormalSlideDuration)
            {
                float t = (Time.time - slideStartTime) / downNormalSlideDuration;
                rb.MovePosition(Vector2.Lerp(startPos, targetPos, t));
                yield return null;
            }
            rb.MovePosition(targetPos);
            
            playerMovement.transform.rotation = Quaternion.Euler(0, playerMovement.isFacingRight ? 180 : 0, 0);
            playerMovement.isFacingRight = !playerMovement.isFacingRight;
        }
        
        yield return new WaitForSeconds(0.1f); 
        
        gameObject.layer = playerLayer;
        foreach (Transform child in transform) child.gameObject.layer = playerLayer;
        
        playerMovement.EndStun();
        isDownNormalAttacking = false;
    }
    
    // ==========================================================
    // S + U (DOWN LIGHT ATTACK) IMPLEMENTATION
    // ==========================================================
    
    public void DownLight_Attack()
    {
        if (isDownLightAttacking || Time.time < downLightLastAttackTime + downLightCooldown)
            return;

        isDownLightAttacking = true;
        downLightLastAttackTime = Time.time;
        
        playerMovement.Stun(true); 
        animator.SetTrigger("DownLightAttack"); 
    }

    public void DownLight_SpawnNinken()
    {
        if (ninkenPrefab == null || ninkenSummonSpawnPoint == null)
        {
            Debug.LogError("Thiếu Prefab hoặc SpawnPoint cho chiêu S+U!");
            DownLight_EndSkill();
            return;
        }

        GameObject ninkenGO = Instantiate(ninkenPrefab, ninkenSummonSpawnPoint.position, Quaternion.identity);
        
        NinkenProjectile ninkenAI = ninkenGO.GetComponent<NinkenProjectile>();
        if (ninkenAI != null)
        {
            ninkenAI.Initialize(playerMovement.isFacingRight, enemyTag, this.downLightDamage, ninkenJumpForce, ninkenSpeed);
        }
        else
        {
            Debug.LogError("Prefab chó bị thiếu script NinkenProjectile!");
        }
    }
    
    public void DownLight_EndSkill()
    {
        isDownLightAttacking = false;
        playerMovement.EndStun();
    }
    
    // ==========================================================
    // S + I (DOWN HEAVY ATTACK) IMPLEMENTATION
    // ==========================================================
    
    public void DownHeavy_Attack()
    {
        if (isDownHeavyAttacking || Time.time < downHeavyLastAttackTime + downHeavyCooldown)
            return;

        animator.SetTrigger("DownHeavyAttack"); 
    }

    public void DownHeavy_StartSkill()
    {
        if (isDownHeavyAttacking) return; 

        GameObject enemyObj = GameObject.FindGameObjectWithTag(enemyTag);
        if (enemyObj == null)
        {
            animator.ResetTrigger("DownHeavyAttack"); 
            return;
        }

        downHeavyLockedEnemy = enemyObj.GetComponent<PlayerHealth>();
        if (downHeavyLockedEnemy == null) return;

        isDownHeavyAttacking = true;
        downHeavyLastAttackTime = Time.time;
        
        playerMovement.Stun(true);
        PlayerMovement enemyMovement = downHeavyLockedEnemy.GetComponent<PlayerMovement>();
        if (enemyMovement != null) enemyMovement.Stun(true);

        if (angerSound != null && audioSource != null) audioSource.PlayOneShot(angerSound);
        if (cutInTimeline != null) cutInTimeline.Play(); 

        StartCoroutine(DownHeavy_ExplosionSequence(downHeavyLockedEnemy));
    }
    
    private IEnumerator DownHeavy_ExplosionSequence(PlayerHealth enemy)
    {
        yield return new WaitForSeconds(cutInDuration); 

        if (enemy == null) yield break;

        GameObject explosionGO = null;
        if (explosionPrefab != null)
        {
            // SỬA: Lấy X, Y của địch và Z của Player
            Vector3 explosionPos = new Vector3(enemy.transform.position.x, enemy.transform.position.y, this.transform.position.z);
            explosionGO = Instantiate(explosionPrefab, explosionPos, Quaternion.identity);
        }

        int totalTicks = (int)(holdDuration / tickRate);
        int damagePerTick = downHeavyTotalDamage / totalTicks;   
        float startTime = Time.time;

        while (Time.time < startTime + holdDuration)
        {
            if (enemy == null) break; 
            if (explosionGO != null)
                enemy.transform.position = explosionGO.transform.position;
                
            enemy.TakeDamage(damagePerTick, 0, Vector3.zero, false); 
            yield return new WaitForSeconds(tickRate);
        }

        if (enemy != null)
        {
            Vector3 knockbackDir = (enemy.transform.position - transform.position).normalized;
            enemy.TakeDamage(0, finalKnockback, knockbackDir, true); 
            
            PlayerMovement enemyMovement = enemy.GetComponent<PlayerMovement>();
            if (enemyMovement != null) enemyMovement.EndStun();
        }
        
        downHeavyLockedEnemy = null;
    }

    public void DownHeavy_EndSkill()
    {
        isDownHeavyAttacking = false;
        playerMovement.EndStun();
    }
}