using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class HeavyAttack : MonoBehaviour,InterfaceSkill
{
    public static HeavyAttack instant;
    public float coolDownTime { get; set; } = 2f;
    public int damage { get; set; } = 30;
    public KeyCode KeyCode { get; set; }
    public KeyCode keyCodeDir;
    public GameObject heavySkillObj;
    public GameObject heavySkillPos;
    public bool isForward = false;
    public bool isUpForward = false;
    public bool isDownForward = false;
    private bool enableAttack = true;
    private LegPlayer legPlayer;
    public PlayerMovement playerMovement;
    
    void Awake()
    {
        instant = this;
        // damage = 30;
        KeyCode = (this.gameObject.CompareTag("P1")) ? KeyCode.U : KeyCode.Keypad4;
        keyCodeDir = (this.gameObject.CompareTag("P1")) ? KeyCode.W : KeyCode.UpArrow;
        foreach (Transform child in this.gameObject.transform)
        {
            if (child.gameObject.name == "Leg")
            {
                legPlayer = child.gameObject.GetComponent<LegPlayer>();
            }
        }
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        // coolDownTime = 2f;
    }

    public void CoolDown()
    {
        StartCoroutine(CoolDownCount(coolDownTime));
    }

    private IEnumerator CoolDownCount(float time)
    {
        enableAttack = false;
        yield return new WaitForSeconds(time);
        enableAttack = true;
        isForward = false;
        isUpForward = false;
        isDownForward = false;
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
        if (Input.GetKeyDown(KeyCode) && Input.GetKey(keyCodeDir) && legPlayer.isGrounded && enableAttack)
        {
            IChigoSkill.instant.animator.SetBool("HeavyAttack", true);
            isUpForward = true;
        }
        // Kiểm tra "Đánh ngang"
        else if (Input.GetKeyDown(KeyCode) && legPlayer.isGrounded && enableAttack)
        {
            IChigoSkill.instant.animator.SetBool("HeavyAttack", true);
            isForward = true;
        }
        // Kiểm tra "Đánh xuống"
        else if (Input.GetKeyDown(KeyCode) && !Input.GetKey(keyCodeDir) && !legPlayer.isGrounded && enableAttack)
        {
            IChigoSkill.instant.animator.SetBool("HeavyAttack", true);
            isDownForward = true;
        }
    }
    

}
