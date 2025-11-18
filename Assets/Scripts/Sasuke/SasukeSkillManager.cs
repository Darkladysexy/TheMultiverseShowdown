using UnityEngine;

public class SasukeSkillManager : SkillManager
{
    private HeavyAttackSasuke heavyAttackSasuke;
    private SpecialAttackSasuke specialAttackSasuke;
    private Down_Skill_Sasuke down_Skill_Sasuke;
    private Up_Skill_Sasuke up_Skill_Sasuke;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialize();
        heavyAttackSasuke = this.gameObject.GetComponent<HeavyAttackSasuke>();
        specialAttackSasuke = this.gameObject.GetComponent<SpecialAttackSasuke>();
        down_Skill_Sasuke = this.gameObject.GetComponent<Down_Skill_Sasuke>();
        up_Skill_Sasuke = this.gameObject.GetComponent<Up_Skill_Sasuke>();
    }

    // Update is called once per frame
    void Update()
    {
        SkillActive();
    }
    public override void SkillActive()
    {
        Debug.Log("Sasuke Skill");
        if(NoAction())
        {
            heavyAttackSasuke.Attack();
            specialAttackSasuke.Attack();
        }
        if(IsBloking())
        {
            if(enableAttack)
            {
                if(Input.GetKeyDown(normalAttackKeyCode))
                {
                    down_Skill_Sasuke.DownNormalAttack();
                    enableAttack = false;
                }
                else if(Input.GetKeyDown(heavyAttackKeyCode))
                {
                    down_Skill_Sasuke.DownHeavyAttack();
                    enableAttack = false;
                }
                else if(Input.GetKeyDown(specialAttackKeyCode))
                {
                    down_Skill_Sasuke.DownSpecialAttack();
                    enableAttack = false;
                }
            }
        }
        if(Input.GetKey(upArrowKeyCode))
        {
            if(Input.GetKeyDown(normalAttackKeyCode) && enableAttack)
            {
                up_Skill_Sasuke.UpNormalAttack();
                enableAttack = false;
            }
            else if(Input.GetKeyDown(specialAttackKeyCode) && enableAttack)
            {
                up_Skill_Sasuke.UpSpecialAttack();
                enableAttack = false;
            }
        }
    }
}
