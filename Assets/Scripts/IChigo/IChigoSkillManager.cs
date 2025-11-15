using UnityEngine;

public class IChigoSkillManager : SkillManager
{
    private HeavyAttack heavyAttack;
    private SpeacialAttack speacialAttack;
    private Down_Skill_IChigo down_Skill_IChigo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialize();
        heavyAttack = this.gameObject.GetComponent<HeavyAttack>();
        speacialAttack = this.gameObject.GetComponent<SpeacialAttack>();
        down_Skill_IChigo = this.gameObject.GetComponent<Down_Skill_IChigo>();
    }

    // Update is called once per frame
    void Update()
    {
        SkillActive();
    }
    public override void SkillActive()
    {
        if(NoAction())
        {
            heavyAttack.Attack();
            speacialAttack.Attack();
        }
        if(IsBloking())
        {
            if(Input.GetKeyDown(normalAttackKeyCode)) down_Skill_IChigo.Down_Normal_Attack();
        }
    }
}
