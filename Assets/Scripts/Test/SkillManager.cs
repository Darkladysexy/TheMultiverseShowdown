using UnityEngine;
public abstract class SkillManager : MonoBehaviour
{
    protected KeyCode upArrowKeyCode;
    protected KeyCode downArrowKeyCode;
    protected KeyCode normalAttackKeyCode;
    protected KeyCode heavyAttackKeyCode;
    protected KeyCode specialAttackKeyCode;
    protected Animator animator;
    protected int actionLayerIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Initialize()
    {
        upArrowKeyCode = (this.gameObject.CompareTag("P1")) ? KeyCode.W : KeyCode.UpArrow;
        downArrowKeyCode = (this.gameObject.CompareTag("P1")) ? KeyCode.S : KeyCode.DownArrow;
        normalAttackKeyCode = (this.gameObject.CompareTag("P1")) ? KeyCode.J : KeyCode.Keypad1;
        heavyAttackKeyCode = (this.gameObject.CompareTag("P1")) ? KeyCode.U : KeyCode.Keypad4;
        specialAttackKeyCode = (this.gameObject.CompareTag("P1")) ? KeyCode.I : KeyCode.Keypad5;

        animator = this.GetComponent<Animator>();
        // Lay animator layer (Attack Layer)
        actionLayerIndex = animator.GetLayerIndex("Attack Layer");
    }
    protected bool NoAction()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(actionLayerIndex);
        return stateInfo.IsTag("NoAction");
    }
    protected bool IsBloking()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(actionLayerIndex);
        return stateInfo.IsName("Block");
    }
    public abstract void SkillActive();
}
