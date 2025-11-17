using Unity.VisualScripting;
using UnityEngine;

public class AttackResetBehaviour : StateMachineBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Lấy script PlayerAttack từ đối tượng cha (Player)
        PlayerAttack playerAttack = animator.gameObject.GetComponent<PlayerAttack>();
        if (playerAttack != null)
            playerAttack.AttackFinished();
    }
}

