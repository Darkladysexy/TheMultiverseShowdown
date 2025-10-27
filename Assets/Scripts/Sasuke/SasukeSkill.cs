using UnityEngine;

public class SasukeSkill : MonoBehaviour
{
    public static SasukeSkill instant;
    private int actionLayerIndex;
    public Animator animator;
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
        animator = this.GetComponent<Animator>();
        actionLayerIndex = animator.GetLayerIndex("Attack Layer");
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(actionLayerIndex);
        bool isReadyForAction = stateInfo.IsTag("NoAction");
        if (isReadyForAction)
        {
            specialAttackSasuke.Attack();
            heavyAttackSasuke.Attack();
        }
    }
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
