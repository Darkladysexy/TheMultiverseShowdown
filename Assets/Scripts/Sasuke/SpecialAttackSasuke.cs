using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class SpecialAttackSasuke : MonoBehaviour,InterfaceSkill
{
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    private LegPlayer legPlayer;
    private PlayableDirector skillTimeLine;
    private AudioSource audioSource;
    public float coolDownTime { get; set; } = 4f;
    public int damage { get; set; } = 40;
    [HideInInspector] public KeyCode KeyCode { get; set; }
    private KeyCode upKeyCode;
    private bool enableAttack = true;
    [Header("Collider cua cac don danh")]
    public GameObject specialAttack1_HurtBox;
    public GameObject specialAttack2_HurtBox;
    [Header("Audio Clip")]
    public AudioClip specialAttackAudio1;
    public AudioClip specialAttackAudio2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        rb = this.GetComponent<Rigidbody2D>();
        skillTimeLine = this.GetComponent<PlayableDirector>();
        audioSource = this.gameObject.GetComponent<AudioSource>();

        foreach(Transform child in this.gameObject.transform)
        {
            if(child.gameObject.name == "Leg")
            {
                legPlayer = child.gameObject.GetComponent<LegPlayer>();
            }
        }

        KeyCode = (this.gameObject.CompareTag("P1")) ? KeyCode.I : KeyCode.Keypad5;
        upKeyCode = (this.gameObject.CompareTag("P1")) ? KeyCode.W : KeyCode.UpArrow;
        // Tat cac collider ban dau la false
        specialAttack1_HurtBox.SetActive(false);
        specialAttack2_HurtBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// Neu nguoi choi bam phim skill, dang o tren mat dat, va da cooldown xong
    /// 
    /// </summary>
    public void Attack()
    {
        if (!Input.GetKey(upKeyCode) && Input.GetKeyDown(KeyCode) && legPlayer.isGrounded && enableAttack)
        {
            SasukeSkill.instant.animator.SetTrigger("SpecialAttack");
            enableAttack = false;
        }
    }
    /// <summary>
    /// Duoc ngay khi thuc hien skill bang Animation event
    /// </summary>
    public void CoolDown()
    {
        StartCoroutine(CoolDownCount(coolDownTime));
    }
    private IEnumerator CoolDownCount(float time)
    {
        yield return new WaitForSeconds(time);
        enableAttack = true;
    }
    public void EndSkill()
    {
        specialAttack1_HurtBox.SetActive(false);
        specialAttack2_HurtBox.SetActive(false);
    }
    /// <summary>
    /// Tao luc day khi Sasuke thuc hien skill
    /// Duoc goi trong animation event
    /// </summary>
    public void StartSkill()
    {
        if (playerMovement.isFacingRight)
            rb.AddForce(Vector2.right * 5f, ForceMode2D.Impulse);
        else
            rb.AddForce(Vector2.left * 5f, ForceMode2D.Impulse);
    }
    public void StartSpecialAttack1()
    {
        specialAttack1_HurtBox.SetActive(true);
    }
    public void StartSpecialAttack2()
    {
        specialAttack2_HurtBox.SetActive(true);

    }
    public void PlayProjectTile()
    {
        // 1. DỪNG CẢ SCENE LẠI
        Time.timeScale = 0f;
        // 2. BẬT TIMELINE (Timeline này sẽ phớt lờ lệnh dừng)
        if (skillTimeLine != null)
        {
            skillTimeLine.Play();
        }
    }
    public void ResumeGameTime()
    {
        Time.timeScale = 1f;
    }
    public void PlaySpecialAudio1()
    {
        if (specialAttackAudio1 != null)
        {
            audioSource.PlayOneShot(specialAttackAudio1);
        }
    }
    public void PlaySpecialAudio2()
    {
        if(specialAttackAudio2 != null)
        {
            audioSource.PlayOneShot(specialAttackAudio2);
        }
    }
    
}
