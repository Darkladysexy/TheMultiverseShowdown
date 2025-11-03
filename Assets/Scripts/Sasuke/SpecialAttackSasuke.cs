using System.Collections;
using UnityEngine;

public class SpecialAttackSasuke : MonoBehaviour,InterfaceSkill
{
    public float coolDownTime { get; set; } = 4f;
    public int damage { get; set; } = 40;
    [HideInInspector] public KeyCode KeyCode { get; set; }
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    private LegPlayer legPlayer;
    private bool enableAttack = true;
    [Header("Collider cua cac don danh")]
    public GameObject specialAttack1_HurtBox;
    public GameObject specialAttack2_HurtBox;
    public GameObject projectTiles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        rb = this.GetComponent<Rigidbody2D>();
        foreach(Transform child in this.gameObject.transform)
        {
            if(child.gameObject.name == "Leg")
            {
                legPlayer = child.gameObject.GetComponent<LegPlayer>();
            }
        }

        KeyCode = (this.gameObject.CompareTag("P1")) ? KeyCode.I : KeyCode.Keypad5;
        // Tat cac collider ban dau la false
        specialAttack1_HurtBox.SetActive(false);
        specialAttack2_HurtBox.SetActive(false);
        projectTiles.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode) && legPlayer.isGrounded && enableAttack)
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
    public void EnableProjectTiles()
    {
        projectTiles.SetActive(true);
    }
    public void DisableProjectTiles()
    {
        projectTiles.SetActive(false);
    }
}
