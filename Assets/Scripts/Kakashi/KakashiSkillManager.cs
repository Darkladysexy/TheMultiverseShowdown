using UnityEngine;

public class KakashiSkillManager : MonoBehaviour
{
    public static KakashiSkillManager instance;

    private int actionLayerIndex;
    public Animator animator;

    private KakashiLightAttack lightAttack;
    private KakashiHeavyAttack heavyAttack;
    private KakashiAerialAttack aerialAttack;
    
    private LegPlayer legPlayer;

    // Biến để lưu trữ phím bấm cho P1 hoặc P2
    private KeyCode keyLight; // Phím U hoặc Keypad4
    private KeyCode keyHeavy; // Phím I hoặc Keypad5

    void Awake()
    {
        instance = this;
        lightAttack = GetComponent<KakashiLightAttack>();
        heavyAttack = GetComponent<KakashiHeavyAttack>();
        aerialAttack = GetComponent<KakashiAerialAttack>();
        foreach(Transform child in this.gameObject.transform)
        {
            if(child.gameObject.name == "Leg")
            {
                legPlayer = child.gameObject.GetComponent<LegPlayer>();
            }
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        actionLayerIndex = animator.GetLayerIndex("Attack Layer");

        // === GÁN PHÍM DỰA TRÊN TAG ===
        if (gameObject.CompareTag("P1"))
        {
            keyLight = KeyCode.U;
            keyHeavy = KeyCode.I;
        }
        else if (gameObject.CompareTag("P2"))
        {
            keyLight = KeyCode.Keypad4;
            keyHeavy = KeyCode.Keypad5;
        }
    }

    void Update()
    {
        bool isGrounded = legPlayer != null ? legPlayer.isGrounded : false;

        // Kiểm tra input bằng các biến đã gán
        bool isLightPressed = Input.GetKeyDown(keyLight);
        bool isHeavyPressed = Input.GetKeyDown(keyHeavy);

        // GROUND + U/4: Light Attack
        if (isLightPressed && isGrounded)
        {
            if (lightAttack != null)
            {
                lightAttack.Attack(); 
                Debug.Log("[SKILL MANAGER] ✓ Light Attack triggered!");
            }
        }
        // AIR + U/4: Aerial Attack
        else if (isLightPressed && !isGrounded)
        {
            if (aerialAttack != null)
            {
                aerialAttack.Attack(); 
                Debug.Log("[SKILL MANAGER] ✓ Aerial Attack triggered!");
            }
        }
        // GROUND + I/5: Heavy Attack
        else if (isHeavyPressed && isGrounded) 
        {
            if (heavyAttack != null)
            {
                heavyAttack.Attack(); 
                Debug.Log("[SKILL MANAGER] ✓ Heavy Attack triggered!");
            }
        }
    }

    // =========================================================
    // HỆ THỐNG ANIMATION EVENT (ĐÃ ĐƯỢC CẬP NHẬT)
    // =========================================================

    // === Light Attack Events ===
    public void TriggerLightAttackStart()
    {
        Debug.Log("[SKILL MANAGER EVENT] ✓ TriggerLightAttackStart called!");
        lightAttack?.StartSkill();
    }
    public void TriggerLightAttackEnd() => lightAttack?.EndSkill();
    public void TriggerLightAttackCoolDown() => lightAttack?.CoolDown();

    // === Heavy Attack Events ===
    public void TriggerHeavyAttackStart()
    {
        Debug.Log("[SKILL MANAGER EVENT] ✓ TriggerHeavyAttackStart called!");
        heavyAttack?.StartSkill();
    }
    
    // HÀM ĐƯỢC THÊM VÀO ĐỂ DỪNG LƯỚT
    public void TriggerChidoriEndDash()
    {
        Debug.Log("[SKILL MANAGER EVENT] ✓ TriggerChidoriEndDash called!");
        heavyAttack?.TriggerChidoriEndDash();
    }

    // HÀM ĐƯỢC THÊM VÀO ĐỂ SPAWN
    public void TriggerSpawnChidori()
    {
        Debug.Log("[SKILL MANAGER EVENT] ✓ TriggerSpawnChidori called!");
        heavyAttack?.TriggerSpawnChidori();
    }

    public void TriggerHeavyAttackEnd()
    {
        Debug.Log("[SKILL MANAGER EVENT] ✓ TriggerHeavyAttackEnd called!");
        heavyAttack?.EndSkill();
    }
    public void TriggerHeavyAttackCoolDown() => heavyAttack?.CoolDown();

    // === Aerial Attack Events ===
    public void TriggerAerialAttackStart()
    {
        Debug.Log("[SKILL MANAGER EVENT] ✓ TriggerAerialAttackStart called!");
        aerialAttack?.StartSkill();
    }
    public void TriggerAerialAttackEnd() => aerialAttack?.EndSkill();
    public void TriggerAerialAttackCoolDown() => aerialAttack?.CoolDown();
}