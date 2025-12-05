using UnityEngine;

public class SasukeSkillManager : SkillManager
{
    private HeavyAttackSasuke heavyAttackSasuke;
    private SpecialAttackSasuke specialAttackSasuke;
    private Down_Skill_Sasuke down_Skill_Sasuke;
    private Up_Skill_Sasuke up_Skill_Sasuke;
    private PlayerStamina playerStamina;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialize();
        heavyAttackSasuke = this.gameObject.GetComponent<HeavyAttackSasuke>();
        specialAttackSasuke = this.gameObject.GetComponent<SpecialAttackSasuke>();
        down_Skill_Sasuke = this.gameObject.GetComponent<Down_Skill_Sasuke>();
        up_Skill_Sasuke = this.gameObject.GetComponent<Up_Skill_Sasuke>();
        playerStamina = this.gameObject.GetComponent<PlayerStamina>();
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
                if(Input.GetKeyDown(normalAttackKeyCode) && playerStamina.currentStamina >= down_Skill_Sasuke.downNormalAttackDamage)
                {
                    down_Skill_Sasuke.DownNormalAttack();
                    playerStamina.UseStamina(down_Skill_Sasuke.downNormalAttackDamage);
                    enableAttack = false;
                }
                else if(Input.GetKeyDown(heavyAttackKeyCode) && playerStamina.currentStamina >= down_Skill_Sasuke.downHeavyAttackDamage)
                {
                    down_Skill_Sasuke.DownHeavyAttack();
                    playerStamina.UseStamina(down_Skill_Sasuke.downHeavyAttackDamage);
                    enableAttack = false;
                }
                else if(Input.GetKeyDown(specialAttackKeyCode) && playerStamina.currentStamina == playerStamina.maxStamina)
                {
                    down_Skill_Sasuke.DownSpecialAttack();
                    playerStamina.UseStamina(playerStamina.maxStamina);
                    enableAttack = false;
                }
            }
        }
        if(Input.GetKey(upArrowKeyCode))
        {
            if(Input.GetKeyDown(normalAttackKeyCode) && enableAttack && playerStamina.currentStamina >= up_Skill_Sasuke.upNormalAttackDamage)
            {
                up_Skill_Sasuke.UpNormalAttack();
                playerStamina.UseStamina(up_Skill_Sasuke.upNormalAttackDamage);
                enableAttack = false;
            }
            else if(Input.GetKeyDown(specialAttackKeyCode) && enableAttack && playerStamina.currentStamina >= up_Skill_Sasuke.upSpecialAttackDamage)
            {
                up_Skill_Sasuke.UpSpecialAttack();
                playerStamina.UseStamina(up_Skill_Sasuke.upSpecialAttackDamage);
                enableAttack = false;
            }
        }
    }
}
