using Unity.VisualScripting;
using UnityEngine;

public class IChigoSkill : MonoBehaviour
{
    public static IChigoSkill instant;
    private int actionLayerIndex;
    private HeavyAttack heavyAttack;
    private SpeacialAttack speacialAttack;
    public Animator animator;
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
        actionLayerIndex = animator.GetLayerIndex("Attack Layer");

        // heavyAttackKey = (this.gameObject.tag == "P1") ? KeyCode.U : KeyCode.Alpha4;
        // specialAttackKey = (this.gameObject.tag == "P1") ? KeyCode.I : KeyCode.Alpha5;

    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(actionLayerIndex);
        bool isReadyForAction = stateInfo.IsTag("NoAction");
        if (isReadyForAction)
        {
            heavyAttack.Attack();
            speacialAttack.Attack();
        }
    }
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
