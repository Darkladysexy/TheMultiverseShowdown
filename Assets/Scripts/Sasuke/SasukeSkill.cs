using UnityEngine;

public class SasukeSkill : MonoBehaviour
{
    [HideInInspector] public static SasukeSkill instant;
    [HideInInspector] public Animator animator;
    private int actionLayerIndex;
    private SpecialAttackSasuke specialAttackSasuke;
    private HeavyAttackSasuke heavyAttackSasuke;
    void Awake()
    {
        instant = this;
        specialAttackSasuke = this.GetComponent<SpecialAttackSasuke>();
        heavyAttackSasuke = this.GetComponent<HeavyAttackSasuke>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Lay index layer Attack Layer
        animator = this.GetComponent<Animator>();
        actionLayerIndex = animator.GetLayerIndex("Attack Layer");
    }

    // Update is called once per frame
    void Update()
    {
        // Kiem tra xem AttackLayer co dang o state NoAction khong
        // Neu co thi moi thuc hien duoc don danh
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(actionLayerIndex);
        bool isReadyForAction = stateInfo.IsTag("NoAction");
        if (isReadyForAction)
        {
            specialAttackSasuke.Attack();
            heavyAttackSasuke.Attack();
        }
    }
    // Cac animation event cua skill
    public void TriggerHeavyAttackStart()
    {
        heavyAttackSasuke.StartSkill();
    }
    public void TriggerHeavyAttackEnd()
    {
        heavyAttackSasuke.EndSkill();
    }
    public void TriggerHeavyAttackCoolDown()
    {
        heavyAttackSasuke.CoolDown();
    }
    public void TriggerSpecialAttackStart()
    {
        specialAttackSasuke.StartSkill();
    }
    public void TriggerSpecialAttackEnd()
    {
        specialAttackSasuke.EndSkill();
    }
    public void TriggerSpecialAttackCoolDown()
    {
        specialAttackSasuke.CoolDown();
    }
}
