using UnityEngine;

// Phải kế thừa từ StateMachineBehaviour
public class DashStateBehaviour : StateMachineBehaviour
{
    private int playerLayer;
    private int dashingLayer;

    // OnStateEnter được gọi khi animation "Dash" bắt đầu
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Lấy layer ID
        playerLayer = LayerMask.NameToLayer("Player");
        dashingLayer = LayerMask.NameToLayer("DashingPlayer");

        // Đổi layer của player và TẤT CẢ object con sang "DashingPlayer"
        animator.gameObject.layer = dashingLayer;
        foreach (Transform child in animator.gameObject.transform)
        {
            child.gameObject.layer = dashingLayer;
        }
    }

    // OnStateExit được gọi khi animation "Dash" kết thúc HOẶC bị ngắt ngang
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Đảm bảo trả layer về "Player"
        animator.gameObject.layer = playerLayer;
        foreach (Transform child in animator.gameObject.transform)
        {
            child.gameObject.layer = playerLayer;
        }
    }
}