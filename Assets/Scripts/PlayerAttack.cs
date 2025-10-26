using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private KeyCode keyCodeAttack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = this.GetComponent<Animator>();
        keyCodeAttack = (this.gameObject.CompareTag("P1")) ? KeyCode.J : KeyCode.Alpha1;
    }

    // Update is called once per frame
    void Update()
    {
        LightAttack();
    }
    public void LightAttack()
    {
        if (Input.GetKeyDown(keyCodeAttack))
        {
            animator.SetTrigger("LightAttack");
        }
    }
    
}
