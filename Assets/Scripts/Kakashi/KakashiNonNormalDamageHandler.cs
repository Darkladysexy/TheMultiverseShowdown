using UnityEngine;
using System.Collections.Generic;

public enum KakashiDamageMode
{
    None,
    UpNormal_WJ,         // W + J (Single Hit, OnEnable)
    UpLight_WU,          // W + U (Two Hit Logic, OnEnable)
    DownNormal_SJ,       // S + J (Dash, OnTriggerEnter2D)
    HeavyChidori_I,      // Ground + I (DOT, OnEnable/FixedUpdate)
    AirNormal_AirJ       // Air + J (Single Hit, OnEnable)
}

[RequireComponent(typeof(Collider2D))]
public class KakashiNonNormalDamageHandler : MonoBehaviour
{
    [Header("Attack Mode")]
    public KakashiDamageMode attackMode = KakashiDamageMode.None;
    
    // --- General Fields ---
    private string tagEnemy;
    private GameObject parent;
    private Collider2D hurtboxCollider;
    private bool hasHitThisEnable = false;

    // --- References to Skill Scripts (Must exist on parent) ---
    private PlayerMovement playerMovement;
    private KakashiUpSkills upSkills;
    private KakashiDownSkills downSkills;
    private KakashiAirSkills airSkills;
    private KakashiHeavyAttack heavyAttackScript;

    // --- Specific Fields for HeavyChidori ---
    private List<PlayerHealth> enemiesHitThisAttack;
    private float heavyKnockbackForce = 3f; // Lực văng cuối cùng của Chidori
    private float tickDamageInterval = 0.15f; 
    private float nextTickTime;
    private int tickDamage;

    // --- Specific Fields for UpLight (W+U) ---
    private float upLightLaunchForce = 4f; 
    private float upLightHorizontalForce = 4f; 

    // --- General Setup ---
    void Awake()
    {
        // 1. Get Components
        parent = transform.parent.gameObject;
        playerMovement = parent.GetComponent<PlayerMovement>();
        upSkills = parent.GetComponent<KakashiUpSkills>();
        downSkills = parent.GetComponent<KakashiDownSkills>();
        airSkills = parent.GetComponent<KakashiAirSkills>();
        heavyAttackScript = parent.GetComponent<KakashiHeavyAttack>();
        hurtboxCollider = GetComponent<Collider2D>();

        // 2. Error Check
        if (hurtboxCollider == null) { enabled = false; return; }
        hurtboxCollider.isTrigger = true; // All hurtboxes must be triggers

        // 3. Set Enemy Tag
        tagEnemy = (parent.CompareTag("P1")) ? "P2" : "P1";

        // 4. Heavy-specific setup
        if (attackMode == KakashiDamageMode.HeavyChidori_I)
        {
            if (heavyAttackScript == null) Debug.LogError("HeavyChidori mode missing KakashiHeavyAttack script!");
            enemiesHitThisAttack = new List<PlayerHealth>();
        }
        // 5. DownNormal-specific setup
        if (attackMode == KakashiDamageMode.DownNormal_SJ)
        {
            if (downSkills == null) Debug.LogError("DownNormal mode missing KakashiDownSkills script!");
        }
    }

    // --- Activation & Hit Check (Used for Instant Hit Modes) ---
    void OnEnable()
    {
        hasHitThisEnable = false;
        
        // Handle DOT setup
        if (attackMode == KakashiDamageMode.HeavyChidori_I)
        {
            enemiesHitThisAttack.Clear();
            nextTickTime = Time.time;
            if (heavyAttackScript != null)
                tickDamage = Mathf.Max(1, heavyAttackScript.damage / 5);
        }
        
        // Instant hit attacks run check immediately
        if (attackMode == KakashiDamageMode.UpNormal_WJ || 
            attackMode == KakashiDamageMode.UpLight_WU || 
            attackMode == KakashiDamageMode.AirNormal_AirJ)
        {
            CheckForHit_Instant();
        }
        // DownNormal (S+J) uses OnTriggerEnter2D
    }

    // --- FixedUpdate (Used for DOT) ---
    void FixedUpdate()
    {
        if (attackMode == KakashiDamageMode.HeavyChidori_I)
        {
            HandleHeavyChidori_DOT();
        }
    }

    // --- Trigger (Used for DownNormal Dash) ---
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (attackMode == KakashiDamageMode.DownNormal_SJ)
        {
            HandleDownNormal_Trigger(collision);
        }
    }

    // --- CORE LOGIC HANDLERS ---

    private void CheckForHit_Instant()
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        filter.useTriggers = true;

        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCollider(hurtboxCollider, filter, results);

        foreach (Collider2D collision in results)
        {
            if (collision.gameObject.CompareTag(tagEnemy) && !hasHitThisEnable)
            {
                PlayerHealth enemyHealth = collision.gameObject.GetComponent<PlayerHealth>();
                if (enemyHealth != null)
                {
                    Vector3 knockbackDir = (collision.transform.position - parent.transform.position).normalized;
                    int damage = 0;
                    float force = 0f;
                    bool isHeavyHit = false;

                    switch (attackMode)
                    {
                        case KakashiDamageMode.UpNormal_WJ: // W + J
                            damage = upSkills.upNormalDamage;
                            break;

                        case KakashiDamageMode.AirNormal_AirJ: // Air + J
                            damage = airSkills.airNormalDamage;
                            knockbackDir.y = Mathf.Abs(knockbackDir.y * 0.5f);
                            break;

                        case KakashiDamageMode.UpLight_WU: // W + U (Multi-hit logic)
                            damage = upSkills.upLightDamage;
                            isHeavyHit = true;
                            
                            if (upSkills.isUpLightFollowUpHit) // ĐÒN 2 (Đá văng ngang)
                            {
                                float dirX = playerMovement.isFacingRight ? 1f : -1f;
                                knockbackDir = new Vector3(dirX, 0.2f, 0).normalized; 
                                force = upLightHorizontalForce;
                            }
                            else // ĐÒN 1 (Đá hất tung)
                            {
                                knockbackDir = Vector3.up; 
                                force = upLightLaunchForce; 
                                upSkills.UpLight_FollowUpJump(); 
                            }
                            break;
                    }

                    enemyHealth.TakeDamage(damage, force, knockbackDir, isHeavyHit);
                    hasHitThisEnable = true;
                    gameObject.SetActive(false);
                    break;
                }
            }
        }
    }

    // Heavy Chidori (Ground + I) uses FixedUpdate for Damage Over Time (DOT)
    private void HandleHeavyChidori_DOT()
    {
        if (Time.time < nextTickTime) return;
        nextTickTime = Time.time + tickDamageInterval;

        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        filter.useTriggers = true; 
        
        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCollider(hurtboxCollider, filter, results);
        
        foreach (Collider2D collision in results)
        {
            if (collision.gameObject.CompareTag(tagEnemy))
            {
                PlayerHealth enemyHealth = collision.gameObject.GetComponent<PlayerHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(tickDamage, 0f, Vector3.zero, false); 
                    
                    if (!enemiesHitThisAttack.Contains(enemyHealth))
                    {
                        enemiesHitThisAttack.Add(enemyHealth);
                    }
                }
            }
        }
    }
    
    // Down Normal (S + J) uses OnTriggerEnter2D for the dash hit detection
    private void HandleDownNormal_Trigger(Collider2D collision)
    {
        if (downSkills == null) return;
        
        if (downSkills.downNormalHasHit) return;

        if (collision.gameObject.CompareTag(tagEnemy))
        {
            PlayerHealth enemyHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (enemyHealth != null)
            {
                downSkills.downNormalHasHit = true;
                downSkills.downNormalEnemyHit = collision.transform;

                int damage = downSkills.downNormalDamage;
                Vector3 knockbackDir = (collision.transform.position - parent.transform.position).normalized;
                
                // Lực văng = 0, nhưng là đòn nặng (true) để kích hoạt ngã
                enemyHealth.TakeDamage(damage, 0f, knockbackDir, true);
                
                gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Được gọi bởi KakashiHeavyAttack (Ground I) tại Event KẾT THÚC
    /// </summary>
    public void ApplyFinalKnockback_HeavyChidori()
    {
        if (attackMode != KakashiDamageMode.HeavyChidori_I || enemiesHitThisAttack == null) return;
        
        foreach (PlayerHealth enemyHealth in enemiesHitThisAttack)
        {
            if (enemyHealth != null)
            {
                Vector3 knockbackDir = (enemyHealth.transform.position - parent.transform.position).normalized;
                enemyHealth.TakeDamage(0, heavyKnockbackForce, knockbackDir, true); 
            }
        }
        enemiesHitThisAttack.Clear(); 
    }

    void OnDisable()
    {
        if (enemiesHitThisAttack != null)
            enemiesHitThisAttack.Clear();
    }
}