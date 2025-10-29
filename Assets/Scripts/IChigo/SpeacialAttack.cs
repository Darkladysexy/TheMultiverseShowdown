using UnityEngine;

public class SpeacialAttack : MonoBehaviour,InterfaceSkill
{
    public static SpeacialAttack instant;
    public float coolDownTime { get; set; }
    public int damage { get; set; }
    public KeyCode KeyCode { get; set; }
    public GameObject specialSkillObj;
    public GameObject specialSkillPos;

    void Awake()
    {
        instant = this;
        KeyCode = (this.gameObject.CompareTag("P1")) ? KeyCode.I : KeyCode.Keypad5;
    }

    public void Attack()
    {
        if (LegPlayer.instant.isGrounded && Input.GetKeyDown(KeyCode))
        {
            IChigoSkill.instant.animator.SetTrigger("SpecialAttack");
        }
    }

    public void CoolDown()
    {
        
    }

    public void EndSkill()
    {
        Instantiate(specialSkillObj, specialSkillPos.gameObject.transform.position, Quaternion.identity);
    }

    public void StartSkill()
    {
        
    }

    
}
