using UnityEngine;

public class IChigoSkillManager : SkillManager
{
    private HeavyAttack heavyAttack;
    private SpeacialAttack speacialAttack;
    private Down_Skill_IChigo down_Skill_IChigo;
    private Up_Skill_IChigo up_Skill_IChigo;
    private PlayerStamina playerStamina;
    private PlayerHealth playerHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialize();
        heavyAttack = this.gameObject.GetComponent<HeavyAttack>();
        speacialAttack = this.gameObject.GetComponent<SpeacialAttack>();
        down_Skill_IChigo = this.gameObject.GetComponent<Down_Skill_IChigo>();
        up_Skill_IChigo = this.gameObject.GetComponent<Up_Skill_IChigo>();
        playerStamina = this.gameObject.GetComponent<PlayerStamina>();
        playerHealth = this.gameObject.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        SkillActive();
    }
    public override void SkillActive()
    {
        if(playerHealth.isDead) return;
        if(NoAction())
        {
            if(playerStamina.currentStamina >= heavyAttack.damage) 
            {
                heavyAttack.Attack();
                // playerStamina.UseStamina(heavyAttack.damage);
            }
            if(playerStamina.currentStamina >= speacialAttack.damage) 
            {
                speacialAttack.Attack();
                // playerStamina.UseStamina(speacialAttack.damage);
            }
        }
        if(IsBloking())
        {
            if(Input.GetKeyDown(normalAttackKeyCode) && enableAttack && playerStamina.currentStamina >= down_Skill_IChigo.downNormalAttackDamage)
            {
                down_Skill_IChigo.Down_Normal_Attack();
                playerStamina.UseStamina(down_Skill_IChigo.downNormalAttackDamage);
                enableAttack = false;
            } 
            else if(Input.GetKeyDown(heavyAttackKeyCode) && enableAttack && playerStamina.currentStamina >= down_Skill_IChigo.downHeavyAttackDamage) 
            {
                down_Skill_IChigo.Down_Heay_Attack();
                playerStamina.UseStamina(down_Skill_IChigo.downHeavyAttackDamage);
                enableAttack = false;
            }
            else if(Input.GetKeyDown(specialAttackKeyCode) && enableAttack && playerStamina.currentStamina == playerStamina.maxStamina)
            {
                down_Skill_IChigo.Down_Special_Attack();
                playerStamina.UseStamina(down_Skill_IChigo.downSpecialDamage);
                enableAttack = false;
            } 
        }
        else if(Input.GetKey(upArrowKeyCode))
        {
            if(Input.GetKeyDown(normalAttackKeyCode) && enableAttack && playerStamina.currentStamina >= up_Skill_IChigo.upNormalAttackDamage)
            {
                up_Skill_IChigo.UpNormalAttack();
                playerStamina.UseStamina(up_Skill_IChigo.upNormalAttackDamage);
                enableAttack = false;
            }
            else if(Input.GetKeyDown(specialAttackKeyCode) && enableAttack && playerStamina.currentStamina >= up_Skill_IChigo.upSpecialAttackDamage)
            {
                up_Skill_IChigo.UpSpecialAttack();
                playerStamina.UseStamina(up_Skill_IChigo.upSpecialAttackDamage);
                enableAttack = false;
            }
        }
    }
}
