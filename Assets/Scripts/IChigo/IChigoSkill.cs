using UnityEngine;

public class IChigoSkill : MonoBehaviour
{
    private int actionLayerIndex;
    private Animator animator;
    private bool isJumpAttack = false;
    public GameObject iSkillObj;
    public GameObject iSkillPos;
    public GameObject oSkillObj;
    public GameObject oSkillPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = this.GetComponent<Animator>();
        actionLayerIndex = animator.GetLayerIndex("Attack Layer");
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(actionLayerIndex);
        bool isReadyForAction = stateInfo.IsTag("NoAction");
        if (isReadyForAction)
        {
            JumpAttack();
            OSkill();
        }
    }
    public void JumpAttack()
    {
        if (PlayerMovement.instant.rb.linearVelocity.y > 0.1 && Input.GetKeyDown(KeyCode.I) && !isJumpAttack)
        {
            animator.SetBool("JumpAttack", true);
            isJumpAttack = true;
        }
    }
    public void EndJumpAttack()
    {
        animator.SetBool("JumpAttack", false);
        isJumpAttack = false;
        Instantiate(iSkillObj, iSkillPos.gameObject.transform.position, Quaternion.identity);
    }
    public void OSkill()
    {
        if (Input.GetKeyDown(KeyCode.O) && LegPlayer.instant.isGrounded)
        {
            animator.SetTrigger("OSkill");
        }
    }
    
    public void EndOSkill()
    {
        Instantiate(oSkillObj, oSkillPos.gameObject.transform.position, Quaternion.identity);
    }
}
