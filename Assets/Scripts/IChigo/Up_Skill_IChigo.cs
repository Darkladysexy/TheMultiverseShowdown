using UnityEngine;

public class Up_Skill_IChigo : MonoBehaviour
{
    /*
        - Up normal attack: w/uprrow + normal attck
        - Up heavy attack: w/uprrow + heavy attack
        - Up special attack: w/uprrow + special attack
    */
    private Animator animator;
    public GameObject specialSkillObj;
    public GameObject specialSkillPos;
    private PlayerMovement playerMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpNormalAttack()
    {
        animator.SetTrigger("UpNormalSkillAttack");
        playerMovement.StartStun();
    }
    public void UpHeavyAttack()
    {
        
    }
    public void UpSpecialAttack()
    {
        animator.SetTrigger("UpSpecialSkillAttack");
        playerMovement.StartStun();
    }
    public void SpawnUpSpecialEnergy()
    {
        GameObject specialEnergy = Instantiate(specialSkillObj, specialSkillPos.gameObject.transform.position, Quaternion.identity);
        specialEnergy.transform.rotation = Quaternion.Euler(0, 0, 90);
        specialEnergy.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * 0.001f, ForceMode2D.Impulse);

    }
}
