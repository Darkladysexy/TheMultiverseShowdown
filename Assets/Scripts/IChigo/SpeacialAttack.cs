using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class SpeacialAttack : MonoBehaviour,InterfaceSkill
{
    // Singletan cua SpecialAttackIchigo
    [HideInInspector] public static SpeacialAttack instant;
    private LegPlayer legPlayer;
    private Animator animator;
    [HideInInspector] public PlayerMovement playerMovement;
    private AudioSource audioSource;
    public AudioClip specialAttackAudio;
    private KeyCode upKeyCode;
    [Header("Thoi gian hoi chieu va sat thuong")]
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
        upKeyCode = (this.gameObject.CompareTag("P1")) ? KeyCode.W : KeyCode.UpArrow;
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        audioSource = this.gameObject.GetComponent<AudioSource>();
        animator = this.gameObject.GetComponent<Animator>();
        
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
        if (legPlayer.isGrounded && !Input.GetKey(upKeyCode) && Input.GetKeyDown(KeyCode) && !isSpecialAttack)
        {
            animator.SetTrigger("SpecialAttack");
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
        GameObject specialEnergy = Instantiate(specialSkillObj, specialSkillPos.gameObject.transform.position, Quaternion.identity);
        // Tan cong sang phai
        if (playerMovement.isFacingRight) specialEnergy.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * 0.001f, ForceMode2D.Impulse);
        // Tan cong sang trai
        else
        {
            specialEnergy.transform.rotation = Quaternion.Euler(0, 180, 0);
            specialEnergy.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0) * 0.001f, ForceMode2D.Impulse);
        }
    }

    public void StartSkill()
    {

    }
    public void PlayAudio()
    {
        if(specialAttackAudio != null)
        {
            audioSource.PlayOneShot(specialAttackAudio);
        }
    }

    
}
