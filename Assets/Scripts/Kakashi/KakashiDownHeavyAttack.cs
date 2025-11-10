using UnityEngine;
using UnityEngine.Playables; 
using System.Collections;    

public class KakashiDownHeavyAttack : MonoBehaviour, InterfaceSkill
{
    [Header("S + I Settings (Special)")]
    public GameObject explosionPrefab;   
    public PlayableDirector cutInTimeline; 
    public AudioClip angerSound;         

    [Header("Chỉ số chiêu")]
    public int totalDamage = 50; 
    public float attackCooldown = 10f;
    public float cutInDuration = 1.5f;   
    public float holdDuration = 2f;      
    public float tickRate = 0.2f;        
    public float finalKnockback = 5f;    

    // --- Interface Properties ---
    public float coolDownTime { get; set; }
    public int damage { get; set; } 
    public KeyCode KeyCode { get; set; } 

    // --- Private Fields ---
    private Animator animator;
    private PlayerMovement playerMovement;
    private AudioSource audioSource;
    private float lastAttackTime = -99f;
    private bool isAttacking = false;
    private PlayerHealth lockedEnemy = null; 
    private string enemyTag;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>(); 
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        this.coolDownTime = this.attackCooldown;
        this.damage = this.totalDamage;
        
        enemyTag = (gameObject.CompareTag("P1")) ? "P2" : "P1";
    }

    void Start()
    {
        if (cutInTimeline != null)
            cutInTimeline.Stop(); 
    }

    /// <summary>
    /// Được gọi bởi KakashiSkillManager
    /// </summary>
    public void Attack()
    {
        if (isAttacking)
        {
            Debug.LogError("[S+I LOG] LỖI: Cố gắng tấn công khi 'isAttacking' VẪN LÀ TRUE!");
            return;
        }
        if (Time.time < lastAttackTime + coolDownTime)
        {
            Debug.Log("[S+I LOG] Chiêu đang trong thời gian hồi...");
            return;
        }

        Debug.Log("[S+I LOG] 1. Attack() được gọi. Kích hoạt Trigger.");
        animator.SetTrigger("DownHeavyAttack"); // Bắt đầu animation
    }

    /// <summary>
    /// GỌI BẰNG ANIMATION EVENT: Tại frame bắt đầu gồng chiêu
    /// </summary>
    public void StartSkill()
    {
        if (isAttacking) return; 

        Debug.Log("[S+I LOG] 2. StartSkill() (Event) đã chạy. Bắt đầu tìm địch.");
        
        GameObject enemyObj = GameObject.FindGameObjectWithTag(enemyTag);
        if (enemyObj == null)
        {
            Debug.LogWarning("[S+I LOG] Không tìm thấy địch! Hủy chiêu.");
            animator.ResetTrigger("DownHeavyAttack"); 
            return;
        }

        lockedEnemy = enemyObj.GetComponent<PlayerHealth>();
        if (lockedEnemy == null) return;

        isAttacking = true;
        lastAttackTime = Time.time;
        
        playerMovement.Stun(true);
        Debug.Log("[S+I LOG] 3. Đã tìm thấy & Khóa địch. Stun Kakashi.");

        PlayerMovement enemyMovement = lockedEnemy.GetComponent<PlayerMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.Stun(true);
        }

        if (angerSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(angerSound);
        }
        if (cutInTimeline != null)
        {
            cutInTimeline.Play(); 
            Debug.Log("[S+I LOG] 4. Bắt đầu chạy Timeline Cut-in.");
        }

        StartCoroutine(ExplosionSequence(lockedEnemy));
    }
    
    private IEnumerator ExplosionSequence(PlayerHealth enemy)
    {
        Debug.Log("[S+I LOG] 5. Coroutine bắt đầu. Đợi " + cutInDuration + "s cho cut-in.");
        yield return new WaitForSeconds(cutInDuration); 

        if (enemy == null)
        {
            Debug.LogWarning("[S+I LOG] Địch đã biến mất trong lúc cut-in. Hủy Coroutine.");
            yield break; // Thoát coroutine
        }

        Debug.Log("[S+I LOG] 6. Cut-in kết thúc. Tạo Vụ nổ & Bắt đầu DOT.");
        GameObject explosionGO = null;
        if (explosionPrefab != null)
        {
            explosionGO = Instantiate(explosionPrefab, enemy.transform.position, Quaternion.identity);
        }

        int totalTicks = (int)(holdDuration / tickRate);
        int damagePerTick = totalDamage / totalTicks;   
        float startTime = Time.time;

        while (Time.time < startTime + holdDuration)
        {
            if (enemy == null) break; 
            if (explosionGO != null)
            {
                enemy.transform.position = explosionGO.transform.position;
            }
            enemy.TakeDamage(damagePerTick, 0, Vector3.zero, false); 
            yield return new WaitForSeconds(tickRate);
        }

        Debug.Log("[S+I LOG] 7. DOT kết thúc. Gây nổ văng.");
        if (enemy != null)
        {
            Vector3 knockbackDir = (enemy.transform.position - transform.position).normalized;
            enemy.TakeDamage(0, finalKnockback, knockbackDir, true); 
            
            PlayerMovement enemyMovement = enemy.GetComponent<PlayerMovement>();
            if (enemyMovement != null)
            {
                enemyMovement.EndStun();
            }
        }
        
        lockedEnemy = null;
        Debug.Log("[S+I LOG] 8. Coroutine Kết thúc.");
    }

    /// <summary>
    /// GỌI BẰNG ANIMATION EVENT: Khi animation kết thúc
    /// </summary>
    public void EndSkill()
    {
        Debug.LogWarning("[S+I LOG] 9. EndSkill() (Event) ĐÃ CHẠY! Reset cờ 'isAttacking' và 'isStun'.");
        isAttacking = false;
        playerMovement.EndStun(); // Hết stun Kakashi
    }
    
    public void CoolDown() { } 
}