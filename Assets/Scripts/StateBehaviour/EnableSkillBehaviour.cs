using UnityEngine;

public class EnableSkillBehaviour : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SkillManager skillManager = animator.gameObject.GetComponent<SkillManager>();
        skillManager.SetEnableAttack(true);
    }
}
