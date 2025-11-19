using UnityEngine;

public class Up_Skill_Sasuke : MonoBehaviour
{
    private Animator animator;
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
        animator.SetTrigger("UpNormalAttack");
        playerMovement.StartStun();
    }
    public void UpHeavyAttack()
    {
        animator.SetTrigger("UpHeavyAttack");
        
    }
    public void UpSpecialAttack()
    {
        animator.SetTrigger("UpSpecialAttack");
        playerMovement.StartStun();
    }
}
