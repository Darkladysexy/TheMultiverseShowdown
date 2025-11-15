using UnityEngine;

public class SkillResetBehaviour : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject parent = animator.gameObject;
        foreach(Transform child in parent.transform)
        {
            if(child.gameObject.CompareTag("Skill")) child.gameObject.SetActive(false);
        }
    }
}
