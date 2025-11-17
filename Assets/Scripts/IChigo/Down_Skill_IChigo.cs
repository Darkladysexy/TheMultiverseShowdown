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
    public GameObject specialSkillObj;
    public GameObject specialSkillPos;
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
        // Tan cong sang phai
        if (playerMovement.isFacingRight) specialEnergy.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * 0.001f, ForceMode2D.Impulse);
        // Tan cong sang trai
        else
        {
            specialEnergy.transform.rotation = Quaternion.Euler(0, 180, 0);
            specialEnergy.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0) * 0.001f, ForceMode2D.Impulse);
        }
    }
}
