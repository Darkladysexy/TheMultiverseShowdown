using UnityEngine;

public class KakashiSkillManager : MonoBehaviour
{
    public static KakashiSkillManager instance;

    private Animator animator;
    private int actionLayerIndex;

    private KakashiNormalAttack normalAttack;
    private KakashiLightAttack lightAttack;
    private KakashiAerialAttack aerialAttack;
    private KakashiHeavyAttack heavyAttack;

    void Awake()
    {
        instance = this;
        
        normalAttack = GetComponent<KakashiNormalAttack>();
        lightAttack = GetComponent<KakashiLightAttack>();
        aerialAttack = GetComponent<KakashiAerialAttack>();
        heavyAttack = GetComponent<KakashiHeavyAttack>();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator != null)
            actionLayerIndex = animator.GetLayerIndex("Attack Layer");
    }

    void Update()
    {
        // ✅ ALWAYS call Attack() methods - họ sẽ check điều kiện bên trong
        if (lightAttack != null)
        {
            lightAttack.Attack(); // Check U + ground
        }

        if (aerialAttack != null)
        {
            aerialAttack.Attack(); // Check U + air
        }

        if (heavyAttack != null)
        {
            heavyAttack.Attack(); // Check I
        }

        // NormalAttack tự Update() internal - không cần gọi từ đây
    }

    // ============ ANIMATION EVENTS - Gọi từ Animator ============

    public void TriggerLightAttackStart()
    {
        Debug.Log("[EVENT] TriggerLightAttackStart");
        if (lightAttack != null) lightAttack.StartSkill();
    }

    public void TriggerLightAttackEnd()
    {
        Debug.Log("[EVENT] TriggerLightAttackEnd");
        if (lightAttack != null) lightAttack.EndSkill();
    }

    public void TriggerLightAttackCoolDown()
    {
        Debug.Log("[EVENT] TriggerLightAttackCoolDown");
        if (lightAttack != null) lightAttack.CoolDown();
    }

    public void TriggerAerialAttackStart()
    {
        Debug.Log("[EVENT] TriggerAerialAttackStart");
        if (aerialAttack != null) aerialAttack.StartSkill();
    }

    public void TriggerAerialAttackEnd()
    {
        Debug.Log("[EVENT] TriggerAerialAttackEnd");
        if (aerialAttack != null) aerialAttack.EndSkill();
    }

    public void TriggerAerialAttackCoolDown()
    {
        Debug.Log("[EVENT] TriggerAerialAttackCoolDown");
        if (aerialAttack != null) aerialAttack.CoolDown();
    }

    public void TriggerHeavyAttackStart()
    {
        Debug.Log("[EVENT] TriggerHeavyAttackStart");
        if (heavyAttack != null) heavyAttack.StartSkill();
    }

    public void TriggerHeavyAttackEnd()
    {
        Debug.Log("[EVENT] TriggerHeavyAttackEnd");
        if (heavyAttack != null) heavyAttack.EndSkill();
    }

    public void TriggerHeavyAttackCoolDown()
    {
        Debug.Log("[EVENT] TriggerHeavyAttackCoolDown");
        if (heavyAttack != null) heavyAttack.CoolDown();
    }
}
