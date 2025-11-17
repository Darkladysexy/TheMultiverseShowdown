using UnityEngine;

public class StunBehaviour : StateMachineBehaviour
{
    // public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     PlayerMovement playerMovement = animator.gameObject.GetComponentInParent<PlayerMovement>();
    //     playerMovement.StartStun();
    // }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerMovement playerMovement = animator.gameObject.GetComponentInParent<PlayerMovement>();
        playerMovement.EndStun();
    }
}
