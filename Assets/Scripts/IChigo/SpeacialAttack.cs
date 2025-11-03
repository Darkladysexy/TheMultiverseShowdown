using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class SpeacialAttack : MonoBehaviour,InterfaceSkill
{
    // Singletan cua SpecialAttackIchigo
    [HideInInspector] public static SpeacialAttack instant;
    private LegPlayer legPlayer;
    [HideInInspector] public PlayerMovement playerMovement;
    
    public float coolDownTime { get; set; } = 4f;
    public int damage { get; set; } = 40;
    [HideInInspector] public KeyCode KeyCode { get; set; }
    [Header("Vi tri Spawn nang luong")]
    public GameObject specialSkillObj;
    public GameObject specialSkillPos;
    // Kiem tra xem nguoi choi co the dung Special Attack khong
    private bool isSpecialAttack = false;

    void Awake()
    {
        // damage = 40;
        instant = this;
        KeyCode = (this.gameObject.CompareTag("P1")) ? KeyCode.I : KeyCode.Keypad5;
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        
        foreach(Transform child in this.gameObject.transform)
        {
            if(child.gameObject.name == "Leg")
            {
                legPlayer = child.gameObject.GetComponent<LegPlayer>();
            }
        }
    }

    public void Attack()
    {
        if (legPlayer.isGrounded && Input.GetKeyDown(KeyCode) && !isSpecialAttack)
        {
            IChigoSkill.instant.animator.SetTrigger("SpecialAttack");
        }
    }

    public void CoolDown()
    {
        StartCoroutine(CoolDownCount(coolDownTime));
    }
    
    private IEnumerator CoolDownCount(float time)
    {
        isSpecialAttack = true;
        yield return new WaitForSeconds(time);
        isSpecialAttack = false;
    }

    public void EndSkill()
    {
        Instantiate(specialSkillObj, specialSkillPos.gameObject.transform.position, Quaternion.identity);
    }

    public void StartSkill()
    {

    }

    
}
