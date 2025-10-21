using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        NormalAttack1();
    }
    public void NormalAttack1()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("NormalAttack1");
        }
    }
    public void NormalAttack2()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("NormalAttack2");
        }
        else animator.SetTrigger("Exit");
    }
    public void NormalAttack3()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("NormalAttack3");
        }
        else animator.SetTrigger("Exit");
    }
}
