using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

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
    
}
