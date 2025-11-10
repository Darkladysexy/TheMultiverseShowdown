using UnityEngine;
using System.Collections; // Cần thêm Coroutine

// s+ u sẽ biến ra 1 con chó nhảy về trước tấn công
public class KakashiDownLightAttack : MonoBehaviour, InterfaceSkill
{
    [Header("S + U Settings (Triệu hồi chó)")]
    public GameObject ninkenPrefab; // Kéo Prefab con chó vào đây
    public Transform summonSpawnPoint; // Vị trí triệu hồi (tạo 1 G.Object rỗng)
    public int attackDamage = 20;
    public float attackCooldown = 5f;
    
    [Header("Chỉ số chó")]
    public float ninkenJumpForce = 5f; // Lực nhảy của chó
    public float ninkenSpeed = 7f;     // Tốc độ di chuyển của chó

    // --- Interface Properties ---
    public float coolDownTime { get; set; }
    public int damage { get; set; }
    public KeyCode KeyCode { get; set; } // Không dùng

    // --- Private Fields ---
    private Animator animator;
    private PlayerMovement playerMovement;
    private string enemyTag; // Tag của đối thủ (P1 hoặc P2)
    private float lastAttackTime = -99f;
    private bool isAttacking = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        
        this.coolDownTime = this.attackCooldown;
        this.damage = this.attackDamage; // Sát thương này sẽ được gán cho con chó
        
        // Xác định tag của kẻ thù
        if (gameObject.CompareTag("P1"))
            enemyTag = "P2";
        else
            enemyTag = "P1";
    }
    
    void Start()
    {
        if (summonSpawnPoint == null)
            Debug.LogWarning("KakashiDownLightAttack: Chưa gán SummonSpawnPoint!");
        if (ninkenPrefab == null)
            Debug.LogWarning("KakashiDownLightAttack: Chưa gán NinkenPrefab!");
    }

    /// <summary>
    /// Được gọi bởi KakashiSkillManager
    /// </summary>
    public void Attack()
    {
        if (isAttacking || Time.time < lastAttackTime + coolDownTime)
            return;

        isAttacking = true;
        lastAttackTime = Time.time;
        
        // Khóa Kakashi trong khi làm động tác triệu hồi
        playerMovement.StartStun(); 
        animator.SetTrigger("DownLightAttack"); // Kích hoạt animation S+U
    }

    // --- HÀM NÀY SẼ ĐƯỢC GỌI BẰNG ANIMATION EVENT ---
    public void SpawnNinken()
    {
        if (ninkenPrefab == null || summonSpawnPoint == null)
        {
            Debug.LogError("Thiếu Prefab hoặc SpawnPoint cho chiêu S+U!");
            EndSkill(); // Phải gọi EndSkill để nhân vật không bị kẹt
            return;
        }

        // Tạo ra con chó
        GameObject ninkenGO = Instantiate(ninkenPrefab, summonSpawnPoint.position, Quaternion.identity);
        
        // Lấy script AI của nó
        NinkenProjectile ninkenAI = ninkenGO.GetComponent<NinkenProjectile>();
        if (ninkenAI != null)
        {
            // Gán sát thương và nói cho nó biết phải bay hướng nào
            ninkenAI.Initialize(playerMovement.isFacingRight, enemyTag, this.damage, ninkenJumpForce, ninkenSpeed);
        }
        else
        {
            Debug.LogError("Prefab chó bị thiếu script NinkenProjectile!");
        }
    }
    
    // --- HÀM NÀY SẼ ĐƯỢC GỌI BẰNG ANIMATION EVENT (ở cuối anim) ---
    public void EndSkill()
    {
        isAttacking = false;
        playerMovement.EndStun(); // Mở khóa Kakashi
    }
    
    // (Các hàm interface còn lại)
    public void StartSkill() { } // Chúng ta dùng SpawnNinken() thay thế
    public void CoolDown() { } // Cooldown đã được xử lý trong hàm Attack()
}