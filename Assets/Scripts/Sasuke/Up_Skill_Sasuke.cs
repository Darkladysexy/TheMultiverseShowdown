using UnityEngine;

public class Up_Skill_Sasuke : MonoBehaviour
{
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpNormalAttack()
    {
        animator.SetTrigger("UpNormalAttack");
    }
    public void UpHeavyAttack()
    {
        animator.SetTrigger("UpHeavyAttack");
    }
    public void UpSpecialAttack()
    {
        animator.SetTrigger("UpSpecialAttack");
    }
}
