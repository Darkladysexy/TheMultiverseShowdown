using UnityEngine;

public class Up_Skill_Sasuke : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;
    [Header("Collider các kĩ năng")]
    public GameObject upNormalAttack1Collider; 
    public GameObject upNormalAttack2Collider; 
    public GameObject upNormalAttack3Collider;
    public GameObject upSpecialAttackCollider;
    [Header("Sát thương các kĩ năng")]
    public int upNormalAttackDamage = 20;
    public int upSpecialAttackDamage = 35;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();

        upNormalAttack1Collider.GetComponent<SendDamageCloseAttack>().SetDamage(upNormalAttackDamage);
        upNormalAttack2Collider.GetComponent<SendDamageCloseAttack>().SetDamage(upNormalAttackDamage);
        upNormalAttack3Collider.GetComponent<SendDamageCloseAttack>().SetDamage(upNormalAttackDamage);
        upSpecialAttackCollider.GetComponent<SendDamageCloseAttack>().SetDamage(upSpecialAttackDamage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpNormalAttack()
    {
        animator.SetTrigger("UpNormalAttack");
        playerMovement.StartStun();
    }
    public void UpSpecialAttack()
    {
        animator.SetTrigger("UpSpecialAttack");
        playerMovement.StartStun();
    }
    public void EnableUpNormalAttack1Collider()
    {
        upNormalAttack1Collider.SetActive(true);
    }
    public void DisableUpNormalAttack1Collider()
    {
        upNormalAttack1Collider.SetActive(false);
    }
    public void EnableUpNormalAttack2Collider()
    {
        upNormalAttack2Collider.SetActive(true);
    }
    public void DisableUpNormalAttack2Collider()
    {
        upNormalAttack2Collider.SetActive(false);
    }
    public void EnableUpNormalAttack3Collider()
    {
        upNormalAttack3Collider.SetActive(true);
    }
    public void DisableUpNormalAttack3Collider()
    {
        upNormalAttack3Collider.SetActive(false);
    }
    public void EnableUpSpecialAttackCollider()
    {
        upSpecialAttackCollider.SetActive(true);
    }
    public void DisableUpSpecialAttackCollider()
    {
        upSpecialAttackCollider.SetActive(false);
    }
}
