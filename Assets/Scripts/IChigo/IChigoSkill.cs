using Unity.VisualScripting;
using UnityEngine;

public class IChigoSkill : MonoBehaviour
{
    [HideInInspector] public static IChigoSkill instant;
    [HideInInspector] public Animator animator;
    private int actionLayerIndex;
    private HeavyAttack heavyAttack;
    private SpeacialAttack speacialAttack;
    // private KeyCode heavyAttackKey;
    // private KeyCode specialAttackKey;
    
    void Awake()
    {
        instant = this;
        heavyAttack = this.GetComponent<HeavyAttack>();
        speacialAttack = this.GetComponent<SpeacialAttack>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = this.GetComponent<Animator>();
        // Lay animator layer (Attack Layer)
        actionLayerIndex = animator.GetLayerIndex("Attack Layer");
    }

    // Update is called once per frame
    void Update()
    {
        // Kiem tra xem Attack Layer dang o state NoAction khong
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(actionLayerIndex);
        bool isReadyForAction = stateInfo.IsTag("NoAction");
        if (isReadyForAction)
        {
            heavyAttack.Attack();
            speacialAttack.Attack();
        }
    }
    // Dung de lam Animation event
    public void TriggerHeavyAttackStart()
    {
        heavyAttack.StartSkill();
    }
    public void TriggerHeavyAttackEnd()
    {
        heavyAttack.EndSkill();
    }
    public void TriggerHeavyAttackCoolDown()
    {
        heavyAttack.CoolDown();
    }
    public void TriggerSpecialAttackStart()
    {
        speacialAttack.StartSkill();
    }
    public void TriggerSpecialAttackEnd()
    {
        speacialAttack.EndSkill();
    }
    public void TriggerSpecialAttackCoolDown()
    {
        speacialAttack.CoolDown();
    }
}
