using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class HeavyAttack : MonoBehaviour,InterfaceSkill
{
    public static HeavyAttack instant;
    private Rigidbody2D rb;
    private readonly Vector2 LEFTUP = new Vector2Int(-1, 1);
    private readonly Vector2 RIGHTUP = new Vector2Int(1, 1);
    private readonly Vector2 LEFTDOWN = new Vector2Int(-1, -1);
    private readonly Vector2 RIGHTDOWN = new Vector2Int(1, -1);
    private Vector2 direction = new Vector2Int(1, 1);
    private LegPlayer legPlayer;
    [HideInInspector] public PlayerMovement playerMovement;
    private Animator animator;
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
        animator = this.gameObject.GetComponent<Animator>();
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
        animator.SetBool("HeavyAttack", false);
        GameObject normalEnergy = Instantiate(heavySkillObj, heavySkillPos.gameObject.transform.position, Quaternion.identity);
        normalEnergy.tag = this.gameObject.tag;
        // Danh ngang
        if (isForward)
        {
            // sang phai
            if (playerMovement.isFacingRight)
            {
                normalEnergy.transform.rotation = Quaternion.Euler(0, 0, 45);
                direction = Vector2.right;
            }
            // sang trai
            else
            {
                normalEnergy.transform.rotation = Quaternion.Euler(0, 180, 45);
                direction = Vector2.left;
            }
        }
        // Danh xuong
        else if (isDownForward)
        {
            // Sang phai
            if (playerMovement.isFacingRight) direction = RIGHTDOWN;
            // Sang trai
            else
            {
                direction = LEFTDOWN;
                normalEnergy.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
        // Danh len
        else if (isUpForward)
        {
            // Sang phai
            if (playerMovement.isFacingRight)
            {
                direction = RIGHTUP;
                normalEnergy.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            // Sang trai
            else
            {
                direction = LEFTUP;
                normalEnergy.transform.rotation = Quaternion.Euler(0, 180, 90);
            }
        }
        normalEnergy.GetComponent<Rigidbody2D>().AddForce(direction * 0.0005f, ForceMode2D.Impulse);
        isForward = false;
        isUpForward = false;
        isDownForward = false;
    }

    public void StartSkill()
    {
        
    }

    public void Attack()
    {
        // Kiểm tra "Đánh lên"
        if (Input.GetKeyDown(KeyCode) && Input.GetKey(keyCodeDir) && legPlayer.isGrounded && enableAttack)
        {
            animator.SetBool("HeavyAttack", true);
            isUpForward = true;
        }
        // Kiểm tra "Đánh ngang"
        else if (Input.GetKeyDown(KeyCode) && legPlayer.isGrounded && enableAttack)
        {
            animator.SetBool("HeavyAttack", true);
            isForward = true;
        }
        // Kiểm tra "Đánh xuống"
        else if (Input.GetKeyDown(KeyCode) && !Input.GetKey(keyCodeDir) && !legPlayer.isGrounded && enableAttack)
        {
            animator.SetBool("HeavyAttack", true);
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
