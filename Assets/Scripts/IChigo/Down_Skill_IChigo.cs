using UnityEngine;

public class Down_Skill_IChigo : MonoBehaviour
{
    /*
        - Down normal attack: block + normal
        - Down heavy attack: block + heavy
        - Down special attack: block + special
    */
    private Animator animator;
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    [Header("Vị trí Spawn và Object")]
    public GameObject specialSkillObj;
    public GameObject specialSkillPos;
    [Header("Collider các kĩ năng")]
    public GameObject downNormalAttackCollider;
    public GameObject downHeavyAttackCollider;
    [Header("Sát thương các kĩ năng")]
    public int downNormalAttackDamage = 15;
    public int downHeavyAttackDamage = 25;
    public int downSpecialDamage = 40;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Down_Normal_Attack()
    {
        playerMovement.StartStun();
        animator.SetTrigger("DownNormalAttack");
    }
    public void Down_Heay_Attack()
    {
        playerMovement.StartStun();
        animator.SetTrigger("DownHeavyAttack");
        if(playerMovement.isFacingRight) rb.AddForce(Vector2.right * 5f, ForceMode2D.Impulse);
        else rb.AddForce(Vector2.left * 5f, ForceMode2D.Impulse);
    }
    public void Down_Special_Attack()
    {
        playerMovement.StartStun();
        animator.SetTrigger("DownSpecialAttack");
    }
    public void SpawnSpecialEnergy()
    {
        GameObject specialEnergy = Instantiate(specialSkillObj, specialSkillPos.gameObject.transform.position, Quaternion.identity);
        specialEnergy.tag = this.gameObject.tag;
        SpecialEnergy specialEnergyScript = specialEnergy.GetComponent<SpecialEnergy>();
        specialEnergyScript.SetDamage(downSpecialDamage);
        // Tan cong sang phai
        if (playerMovement.isFacingRight) specialEnergy.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * 0.001f, ForceMode2D.Impulse);
        // Tan cong sang trai
        else
        {
            specialEnergy.transform.rotation = Quaternion.Euler(0, 180, 0);
            specialEnergy.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0) * 0.001f, ForceMode2D.Impulse);
        }
    }
    public void EnableDownNormalAttackCollider()
    {
        downNormalAttackCollider.SetActive(true);
    }
    public void DisableDownNormalAttackCollider()
    {
        downNormalAttackCollider.SetActive(false);
    }
    public void EnableDownHeavyAttackCollider()
    {
        downHeavyAttackCollider.SetActive(true);
    }
    public void DisableDownHeavyAttackCollider()
    {
        downHeavyAttackCollider.SetActive(false);
    }
}
