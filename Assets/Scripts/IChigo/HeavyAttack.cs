using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class HeavyAttack : MonoBehaviour,InterfaceSkill
{
    [HideInInspector] public static HeavyAttack instant;
    private LegPlayer legPlayer;
    [HideInInspector] public PlayerMovement playerMovement;
    private AudioSource audioSource;
    public AudioClip heavyAttackAudio;
    public float coolDownTime { get; set; } = 2f;
    public int damage { get; set; } = 30;
    [HideInInspector] public KeyCode KeyCode { get; set; }
    [HideInInspector] public KeyCode keyCodeDir;
    [Header("Vi tri spawn nang luong")]
    public GameObject heavySkillObj;
    public GameObject heavySkillPos;
    [Header("Chieu di chuyen cua nang luong")]
    [HideInInspector] public bool isForward = false;
    [HideInInspector] public bool isUpForward = false;
    [HideInInspector] public bool isDownForward = false;
    private bool enableAttack = true;
    
    void Awake()
    {
        instant = this;
        KeyCode = (this.gameObject.CompareTag("P1")) ? KeyCode.U : KeyCode.Keypad4;
        keyCodeDir = (this.gameObject.CompareTag("P1")) ? KeyCode.W : KeyCode.UpArrow;
        audioSource = this.gameObject.GetComponent<AudioSource>();
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
    public void PlayHeavyAttackAudio()
    {
        if(heavyAttackAudio != null)
        {
            audioSource.PlayOneShot(heavyAttackAudio);
        }
    }
    

}
