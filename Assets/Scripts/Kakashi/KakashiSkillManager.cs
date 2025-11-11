using UnityEngine;

public class KakashiSkillManager : MonoBehaviour
{
    public static KakashiSkillManager instance;

    public Animator animator;
    private LegPlayer legPlayer;
    private PlayerMovement playerMovement; 

    // == DANH SÁCH TẤT CẢ CÁC KỸ NĂNG ==
    // private KakashiNormalAttack normalAttack; // J (Ground)
    private KakashiLightAttack lightAttack; // U (Ground)
    private KakashiHeavyAttack heavyAttack; // I (Ground)
    private KakashiAerialAttack aerialAttack; // U (Air)

    // Kỹ năng mới (sẽ tạo ở Bước 5)
    private KakashiAirNormalAttack airNormalAttack; // J (Air)
    private KakashiDownNormalAttack downNormalAttack; // S + J
    private KakashiDownLightAttack downLightAttack; // S + U
    private KakashiDownHeavyAttack downHeavyAttack; // S + I
    private KakashiUpNormalAttack upNormalAttack; // W + J
    private KakashiUpLightAttack upLightAttack; // W + U
    private KakashiUpHeavyAttack upHeavyAttack; // W + I
    private KakashiSubstitution substitution; // O

    // == CÁC PHÍM ĐIỀU KHIỂN ==
    private KeyCode keyJ, keyU, keyI, keyO, keyW, keyS;
    
    // Biến kiểm tra Layer (nếu cần)
    private int actionLayerIndex;

    void Awake()
    {
        instance = this;
        animator = GetComponent<Animator>();
        legPlayer = GetComponentInChildren<LegPlayer>(); 
        playerMovement = GetComponent<PlayerMovement>();

        // Lấy tất cả các component skill
        // normalAttack = GetComponent<KakashiNormalAttack>();
        lightAttack = GetComponent<KakashiLightAttack>();
        heavyAttack = GetComponent<KakashiHeavyAttack>();
        aerialAttack = GetComponent<KakashiAerialAttack>();

        // Tự động thêm và lấy 8 script mới
        airNormalAttack = GetOrAddComponent<KakashiAirNormalAttack>();
        downNormalAttack = GetOrAddComponent<KakashiDownNormalAttack>();
        downLightAttack = GetOrAddComponent<KakashiDownLightAttack>();
        downHeavyAttack = GetOrAddComponent<KakashiDownHeavyAttack>();
        upNormalAttack = GetOrAddComponent<KakashiUpNormalAttack>();
        upLightAttack = GetOrAddComponent<KakashiUpLightAttack>();
        upHeavyAttack = GetOrAddComponent<KakashiUpHeavyAttack>();
        substitution = GetOrAddComponent<KakashiSubstitution>();
    }

    // Hàm trợ giúp để tự động thêm script nếu nó chưa tồn tại
    private T GetOrAddComponent<T>() where T : Component
    {
        T component = GetComponent<T>();
        if (component == null)
        {
            component = gameObject.AddComponent<T>();
        }
        return component;
    }

    void Start()
    {
        actionLayerIndex = animator.GetLayerIndex("Attack Layer");

        // GÁN PHÍM DỰA TRÊN TAG
        if (gameObject.CompareTag("P1"))
        {
            keyJ = KeyCode.J; keyU = KeyCode.U; keyI = KeyCode.I; 
            keyO = KeyCode.O; keyW = KeyCode.W; keyS = KeyCode.S;
        }
        else if (gameObject.CompareTag("P2"))
        {
            keyJ = KeyCode.Keypad1; keyU = KeyCode.Keypad4; keyI = KeyCode.Keypad5; 
            keyO = KeyCode.Keypad6; keyW = KeyCode.UpArrow; keyS = KeyCode.DownArrow;
        }
    }

    // --- HÀM UPDATE() ĐÃ ĐƯỢC SỬA ---
    void Update()
    {
        // Không nhận input nếu đang bị stun
        if (playerMovement.isStun) return;

        // Đọc trạng thái
        bool isGrounded = legPlayer.isGrounded;
        bool isUpHeld = Input.GetKey(keyW);
        bool isDownHeld = Input.GetKey(keyS);
        
        // Kiểm tra xem có đang trong 1 animation tấn công không
        bool isPlayingAttackAnim = !animator.GetCurrentAnimatorStateInfo(actionLayerIndex).IsTag("NoAction");

        // --- 1. XỬ LÝ NÚT THẾ THÂN (O) ---
        if (Input.GetKeyDown(keyO))
        {
            if (substitution != null) 
                substitution.AttemptSubstitution();
            return; 
        }

        // --- 2. XỬ LÝ CÁC ĐÒN TẤN CÔNG (J, U, I) ---
        bool attackTriggeredThisFrame = false; // Dùng để quyết định có nên Thủ hay không

        // Nếu đang trong animation tấn công (vd: NormalAttack1)
        if (isPlayingAttackAnim)
        {
            // Chỉ cho phép "buffering" (xếp hàng) combo đánh thường (J)
            // if (Input.GetKeyDown(keyJ) && !isDownHeld && !isUpHeld && isGrounded)
            // {
            //     normalAttack.HandleNormalAttack();
            // }
            
            // Nếu đang tấn công, thì không cho phép Thủ
            HandleBlocking(false, isGrounded, true); // Tắt block
            return; // Thoát ra, không kiểm tra các đòn khác
        }
        
        // Nếu không đang tấn công (đang Idle, Run, Jump, Fall, hoặc Block)
        
        // --- Check J Key ---
        if (Input.GetKeyDown(keyJ))
        {
            attackTriggeredThisFrame = true; // Báo hiệu đã nhấn 1 nút
            if (isDownHeld && isGrounded) // S + J
            {
                Debug.Log("SkillManager: Kích hoạt S+J (DownNormalAttack)");
                downNormalAttack.Attack();
            }
            else if (isUpHeld && isGrounded) // W + J
            {
                Debug.Log("SkillManager: Kích hoạt W+J (UpNormalAttack)");
                upNormalAttack.Attack();
            }
            else if (!isGrounded) // Air + J
            {
                Debug.Log("SkillManager: Kích hoạt Air+J (AirNormalAttack)");
                airNormalAttack.Attack();
            }
            // else if (isGrounded) // Ground + J (chỉ khi rảnh)
            // {
            //     Debug.Log("SkillManager: Kích hoạt J (NormalAttack)");
            //     normalAttack.HandleNormalAttack(); 
            // }
            else
                attackTriggeredThisFrame = false; // J đã được nhấn, nhưng không có hành động hợp lệ
        }
        // --- Check U Key ---
        else if (Input.GetKeyDown(keyU))
        {
            attackTriggeredThisFrame = true;
            if (isDownHeld && isGrounded) // S + U
                downLightAttack.Attack();
            else if (isUpHeld && isGrounded) // W + U
                upLightAttack.Attack();
            else if (!isGrounded) // Air + U
                aerialAttack.Attack();
            else if (isGrounded) // Ground + U
                lightAttack.Attack();
        }
        // --- Check I Key ---
        else if (Input.GetKeyDown(keyI))
        {
            attackTriggeredThisFrame = true;
            if (isDownHeld && isGrounded) // S + I
                downHeavyAttack.Attack();
            else if (isUpHeld && isGrounded) // W + I
                upHeavyAttack.Attack();
            else if (isGrounded) // Ground + I
                heavyAttack.Attack();
        }

        // --- 3. XỬ LÝ THỦ (S) ---
        HandleBlocking(isDownHeld, isGrounded, attackTriggeredThisFrame);
    }
    // --- KẾT THÚC HÀM UPDATE() ĐÃ SỬA ---


    void HandleBlocking(bool isDownHeld, bool isGrounded, bool attackTriggeredThisFrame)
    {
        // Nếu giữ 'S', trên mặt đất, VÀ không nhấn J/U/I trong frame này
        if (isDownHeld && isGrounded && !attackTriggeredThisFrame)
        {
            playerMovement.StartBlocking();
        }
        else
        {
            playerMovement.StopBlocking();
        }
    }

    // =========================================================
    // HỆ THỐNG ANIMATION EVENT (Giữ nguyên)
    // =========================================================

    public void TriggerLightAttackStart() => lightAttack?.StartSkill();
    public void TriggerLightAttackEnd() => lightAttack?.EndSkill();
    public void TriggerLightAttackCoolDown() => lightAttack?.CoolDown();
    public void TriggerHeavyAttackStart() => heavyAttack?.StartSkill();
    public void TriggerChidoriEndDash() => heavyAttack?.TriggerChidoriEndDash();
    public void TriggerSpawnChidori() => heavyAttack?.TriggerSpawnChidori();
    public void TriggerHeavyAttackEnd() => heavyAttack?.EndSkill();
    public void TriggerHeavyAttackCoolDown() => heavyAttack?.CoolDown();
    public void TriggerAerialAttackStart() => aerialAttack?.StartSkill();
    public void TriggerAerialAttackEnd() => aerialAttack?.EndSkill();
    public void TriggerAerialAttackCoolDown() => aerialAttack?.CoolDown();

    // === EVENT CHO AIR NORMAL ATTACK (J) ===
    public void TriggerAirNormalAttackStart()
    {
        airNormalAttack?.StartSkill();
    }

    public void TriggerAirNormalAttackEnd()
    {
        airNormalAttack?.EndSkill();
    }
    // === THÊM EVENT CHO DOWN LIGHT ATTACK (S+U) ===
    
    /// <summary>
    /// Gọi bởi Animation Event tại thời điểm triệu hồi
    /// </summary>
    public void TriggerDownLightAttack_Spawn()
    {
        downLightAttack?.SpawnNinken();
    }

    /// <summary>
    /// Gọi bởi Animation Event khi kết thúc animation
    /// </summary>
    public void TriggerDownLightAttack_End()
    {
        downLightAttack?.EndSkill();
    }
    // === THÊM EVENT CHO DOWN HEAVY ATTACK (S+I) ===
    
    /// <summary>
    /// Gọi bởi Animation Event tại thời điểm bắt đầu tìm địch
    /// </summary>
    public void TriggerDownHeavyAttack_Start()
    {
        downHeavyAttack?.StartSkill();
    }

    /// <summary>
    /// Gọi bởi Animation Event khi kết thúc animation
    /// </summary>
    public void TriggerDownHeavyAttack_End()
    {
        downHeavyAttack?.EndSkill();
    }
}