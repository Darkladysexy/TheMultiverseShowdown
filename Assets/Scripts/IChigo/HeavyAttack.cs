using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class HeavyAttack : MonoBehaviour,InterfaceSkill
{
    public static HeavyAttack instant;
    public float coolDownTime { get; set; }
    public int damage { get ; set; }
    public KeyCode KeyCode { get; set; }
    public KeyCode keyCodeDir;
    public GameObject heavySkillObj;
    public GameObject heavySkillPos;
    public bool isForward = false;
    public bool isUpForward = false;
    public bool isDownForward = false;
    void Awake()
    {
        instant = this;
        KeyCode = (this.gameObject.CompareTag("P1")) ? KeyCode.U : KeyCode.Alpha4;
        keyCodeDir = (this.gameObject.CompareTag("P1")) ? KeyCode.W : KeyCode.UpArrow;
    }

    public void CoolDown()
    {
        
    }

    public void EndSkill()
    {
        IChigoSkill.instant.animator.SetBool("HeavyAttack", false);
        Instantiate(heavySkillObj, heavySkillPos.gameObject.transform.position, Quaternion.identity);
    }

    public void StartSkill()
    {
        
    }

    public void Attack()
    {
            // Kiểm tra "Đánh lên"
        if (Input.GetKeyDown(KeyCode) && Input.GetKey(keyCodeDir) && LegPlayer.instant.isGrounded && !isForward && !isUpForward && !isDownForward)
        {
            IChigoSkill.instant.animator.SetBool("HeavyAttack", true);
            isUpForward = true;
        }
        // Kiểm tra "Đánh ngang"
        else if (Input.GetKeyDown(KeyCode) && LegPlayer.instant.isGrounded && !isForward && !isUpForward && !isDownForward)
        {
            IChigoSkill.instant.animator.SetBool("HeavyAttack", true);
            isForward = true;
        }
        // Kiểm tra "Đánh xuống"
        else if (Input.GetKeyDown(KeyCode) && !Input.GetKey(keyCodeDir) && !LegPlayer.instant.isGrounded && !isForward && !isUpForward && !isDownForward)
        {
            IChigoSkill.instant.animator.SetBool("HeavyAttack", true);
            isDownForward = true;
        }
    }
    
}
