using UnityEngine;
using System.Collections;

public class KakashiUpHeavyAttack : MonoBehaviour, InterfaceSkill
{
    [Header("W + I Settings")]
    // Kéo Prefab "DragonSegment" (sẽ tạo ở Bước 2) vào đây
    public GameObject dragonSegmentPrefab; 
    public Transform dragonSpawnPoint;
    public int attackDamage = 30;
    public float attackCooldown = 7f;

    [Header("Dragon Stream")]
    public Sprite[] dragonSprites; // 10 sprites của con rồng
    public int segmentCount = 10;
    public float spawnDelay = 0.08f; // Delay giữa mỗi khúc

    // --- Interface Properties ---
    public float coolDownTime { get; set; }
    public int damage { get; set; }
    public KeyCode KeyCode { get; set; } // Không dùng

    // --- Private Fields ---
    private Animator animator;
    private PlayerMovement playerMovement;
    private float lastAttackTime = -99f;
    private bool isAttacking = false;
    private string enemyTag;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

        this.coolDownTime = this.attackCooldown;
        this.damage = this.attackDamage;
        
        enemyTag = (gameObject.CompareTag("P1")) ? "P2" : "P1";
    }

    void Start()
    {
        if (dragonSegmentPrefab == null) 
            Debug.LogError("Kakashi W+I: Thiếu Prefab DragonSegment!");
        if (dragonSpawnPoint == null)
            Debug.LogError("Kakashi W+I: Thiếu Dragon Spawn Point!");
        if (dragonSprites == null || dragonSprites.Length < segmentCount)
            Debug.LogError("Kakashi W+I: Thiếu Dragon Sprites hoặc số lượng không đủ 10!");
    }

    public void Attack()
    {
        if (isAttacking || Time.time < lastAttackTime + coolDownTime)
            return;

        isAttacking = true;
        lastAttackTime = Time.time;
        
        playerMovement.Stun(true); 
        animator.SetTrigger("UpHeavyAttack"); // Kích hoạt animation W+I
    }

    /// <summary>
    /// GỌI BẰNG ANIMATION EVENT: Tại frame Kakashi "gầm" lên
    /// </summary>
    public void SpawnDragon()
    {
        if (dragonSegmentPrefab == null || dragonSpawnPoint == null) return;

        // Bắt đầu Coroutine để bắn 10 khúc rồng
        StartCoroutine(SpawnDragonStream());
    }

    private IEnumerator SpawnDragonStream()
    {
        bool facingRight = playerMovement.isFacingRight;
        Vector3 knockbackDir = (facingRight) ? Vector3.right : Vector3.left;

        for (int i = 0; i < segmentCount; i++)
        {
            // 1. TẠO 1 KHÚC RỒNG TẠI ĐÚNG dragonSpawnPoint
            GameObject segmentGO = Instantiate(dragonSegmentPrefab, dragonSpawnPoint.position, Quaternion.identity);

            // 2. Gắn Tag
            segmentGO.tag = (gameObject.CompareTag("P1")) ? "P1Projectile" : "P2Projectile";

            // 3. Lấy script và bảo nó TỰ BAY ĐI
            DragonSegment segmentScript = segmentGO.GetComponent<DragonSegment>();
            if (segmentScript != null)
            {
                // Truyền cho nó sprite, hướng, sát thương, v.v.
                segmentScript.Initialize(
                    facingRight, 
                    enemyTag, 
                    this.damage, 
                    dragonSprites[i % dragonSprites.Length], // Lấy sprite thứ i
                    knockbackDir
                );
            }
            
            // 4. Chờ một chút rồi bắn khúc tiếp theo
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    /// <summary>
    /// GỌI BẰNG ANIMATION EVENT: Khi kết thúc animation
    /// </summary>
    public void EndSkill()
    {
        playerMovement.EndStun();
        isAttacking = false;
    }
    
    public void CoolDown() { } 
    public void StartSkill() { }
}