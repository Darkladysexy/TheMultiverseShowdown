using UnityEngine;

public class Down_Skill_IChigo : MonoBehaviour
{
    /*
        - Down normal attack: block + normal
        - Down heavy attack: block + heavy
        - Down special attack: block + special
    */
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
    public void Down_Normal_Attack()
    {
        animator.SetTrigger("DownNormalAttack");
    }
    public void Down_Heay_Attack()
    {
        
    }
    public void Down_Special_Attack()
    {
        
    }
}
