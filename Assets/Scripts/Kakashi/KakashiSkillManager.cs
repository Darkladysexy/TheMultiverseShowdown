using UnityEngine;

public class KakashiSkillManager : MonoBehaviour
{
    public static KakashiSkillManager instance;

    public Animator animator;
    private LegPlayer legPlayer;
    private PlayerMovement playerMovement; 
    private PlayerStamina playerStamina; // <<<< Đã thêm >>>>
    private PlayerHealth playerHealth;

    // == DANH SÁCH TẤT CẢ CÁC KỸ NĂNG ==
    // private KakashiNormalAttack normalAttack; // J (Ground)
    private KakashiLightAttack lightAttack; // U (Ground)
    private KakashiHeavyAttack heavyAttack; // I (Ground)
    private KakashiSubstitution substitution; // O

    // Kỹ năng đã gộp
    private KakashiAirSkills airSkills;      // Air + J/U
    private KakashiDownSkills downSkills;    // S + J/U/I
    private KakashiUpSkills upSkills;        // W + J/U/I

    // == CÁC PHÍM ĐIỀU KHIỂN ==
    private KeyCode keyJ, keyU, keyI, keyO, keyW, keyS;
    
    private int actionLayerIndex;

    void Awake()
    {
        instance = this;
        animator = GetComponent<Animator>();
        legPlayer = GetComponentInChildren<LegPlayer>(); 
        playerMovement = GetComponent<PlayerMovement>();
        playerStamina = GetComponent<PlayerStamina>(); // <<<< Khởi tạo PlayerStamina >>>>

        // Lấy các component skill cũ còn giữ lại
        lightAttack = GetComponent<KakashiLightAttack>();
        heavyAttack = GetComponent<KakashiHeavyAttack>();
        substitution = GetComponent<KakashiSubstitution>();

        // Tự động thêm và lấy 3 script đã gộp
        airSkills = GetOrAddComponent<KakashiAirSkills>(); 
        downSkills = GetOrAddComponent<KakashiDownSkills>(); 
        upSkills = GetOrAddComponent<KakashiUpSkills>(); 
    }

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

        if(playerStamina == null) Debug.LogError("KakashiSkillManager: Missing PlayerStamina component!");
    }

    void Update()
    {
        if (playerHealth != null && playerHealth.isDead) return;
        bool isGrounded = legPlayer.isGrounded;
        bool isUpHeld = Input.GetKey(keyW);
        bool isDownHeld = Input.GetKey(keyS);
        
        bool isPlayingAttackAnim = !animator.GetCurrentAnimatorStateInfo(actionLayerIndex).IsTag("NoAction");
        
        if (playerStamina == null) return; 
        
        // --- 1. XỬ LÝ NÚT THẾ THÂN (O) ---
        if (Input.GetKeyDown(keyO))
        {
            if (substitution != null) 
                substitution.AttemptSubstitution();
            return; 
        }

        // --- 2. XỬ LÝ CÁC ĐÒN TẤN CÔNG (J, U, I) ---
        if (isPlayingAttackAnim)
        {
            return; 
        }
        
        // --- Check J Key (Normal Attack) ---
        if (Input.GetKeyDown(keyJ))
        {
            if (isDownHeld && isGrounded) // S + J (Down Normal Attack)
            {
                int cost = downSkills.downNormalDamage;
                if (playerStamina.currentStamina >= cost)
                {
                    playerStamina.UseStamina(cost);
                    downSkills.DownNormal_Attack();
                }
            }
            else if (isUpHeld && isGrounded) // W + J (Up Normal Attack)
            {
                int cost = upSkills.upNormalDamage;
                if (playerStamina.currentStamina >= cost)
                {
                    playerStamina.UseStamina(cost);
                    upSkills.UpNormal_Attack();
                }
            }
            else if (!isGrounded) // Air + J (Air Normal Attack)
            {
                int cost = airSkills.airNormalDamage;
                if (playerStamina.currentStamina >= cost)
                {
                    playerStamina.UseStamina(cost);
                    airSkills.AirNormal_Attack();
                }
            }
            // else if (isGrounded) // Ground + J (Dùng cho PlayerAttack.cs)
            // {
            //     normalAttack.HandleNormalAttack(); 
            // }
        }
        // --- Check U Key (Light Attack) ---
        else if (Input.GetKeyDown(keyU))
        {
            if (isDownHeld && isGrounded) // S + U (Down Light Attack)
            {
                int cost = downSkills.downLightDamage;
                if (playerStamina.currentStamina >= cost)
                {
                    playerStamina.UseStamina(cost);
                    downSkills.DownLight_Attack();
                }
            }
            else if (isUpHeld && isGrounded) // W + U (Up Light Attack)
            {
                int cost = upSkills.upLightDamage;
                if (playerStamina.currentStamina >= cost)
                {
                    playerStamina.UseStamina(cost);
                    upSkills.UpLight_Attack();
                }
            }
            else if (!isGrounded) // Air + U (Aerial Attack - Kunai)
            {
                int cost = airSkills.aerialDamage;
                if (playerStamina.currentStamina >= cost)
                {
                    playerStamina.UseStamina(cost);
                    airSkills.Aerial_Attack();
                }
            }
            else if (isGrounded) // Ground + U (Light Attack - Kunai)
            {
                int cost = lightAttack.damage;
                if (playerStamina.currentStamina >= cost)
                {
                    playerStamina.UseStamina(cost);
                    lightAttack.Attack();
                }
            }
        }
        // --- Check I Key (Heavy Attack) ---
        else if (Input.GetKeyDown(keyI))
        {
            if (isDownHeld && isGrounded) // S + I (Down Heavy Attack - Special)
            {
                // Down Heavy Attack yêu cầu Max Stamina
                int cost = playerStamina.maxStamina; 
                if (playerStamina.currentStamina == cost)
                {
                    playerStamina.UseStamina(cost);
                    downSkills.DownHeavy_Attack();
                }
            }
            else if (isUpHeld && isGrounded) // W + I (Up Heavy Attack - Dragon)
            {
                int cost = upSkills.upHeavyDamage;
                if (playerStamina.currentStamina >= cost)
                {
                    playerStamina.UseStamina(cost);
                    upSkills.UpHeavy_Attack();
                }
            }
            else if (isGrounded) // Ground + I (Heavy Attack - Chidori)
            {
                int cost = heavyAttack.damage;
                if (playerStamina.currentStamina >= cost)
                {
                    playerStamina.UseStamina(cost);
                    heavyAttack.Attack();
                }
            }
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
    
    // === EVENT CHO AIR SKILLS ===
    public void TriggerAirNormalAttackStart() => airSkills?.AirNormal_StartSkill(); 
    public void TriggerAirNormalAttackEnd() => airSkills?.AirNormal_EndSkill(); 
    public void TriggerAerialAttackStart() => airSkills?.Aerial_StartSkill(); 
    public void TriggerAerialAttackEnd() => airSkills?.Aerial_EndSkill(); 
    
    // === EVENT CHO DOWN SKILLS ===
    public void TriggerDownLightAttack_Spawn() => downSkills?.DownLight_SpawnNinken(); 
    public void TriggerDownLightAttack_End() => downSkills?.DownLight_EndSkill(); 
    public void TriggerDownHeavyAttack_Start() => downSkills?.DownHeavy_StartSkill(); 
    public void TriggerDownHeavyAttack_End() => downSkills?.DownHeavy_EndSkill(); 
    
    // === EVENT CHO UP SKILLS ===
    public void TriggerUpNormalAttack_Start() => upSkills?.UpNormal_StartSkill(); 
    public void TriggerUpNormalAttack_End() => upSkills?.UpNormal_EndSkill(); 
    public void TriggerUpLightAttack_Start() => upSkills?.UpLight_StartSkill(); 
    public void TriggerUpLightAttack_End() => upSkills?.UpLight_EndSkill(); 
    public void TriggerUpLightAttack_FollowUpHit() => upSkills?.UpLight_StartFollowUpHit(); 
    public void TriggerUpHeavyAttack_SpawnDragon() => upSkills?.UpHeavy_SpawnDragon(); 
    public void TriggerUpHeavyAttack_End() => upSkills?.UpHeavy_EndSkill(); 
}