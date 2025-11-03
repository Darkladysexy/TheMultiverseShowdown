using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class HeavyAttackSasuke : MonoBehaviour, InterfaceSkill
{
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    private LegPlayer legPlayer;
    public float coolDownTime { get; set; } = 2f;
    public int damage { get; set; } = 30;
    [HideInInspector] public KeyCode KeyCode { get; set; }
    private KeyCode keyCodeDir { get; set; }
    private float force;
    [Header("Collider cua cac don danh")]
    public GameObject heavyAttackForward_HurtBox;
    public GameObject heavyAttackDownForward_HurtBox;
    public GameObject heavyAttackUpForward_HurtBox;
    // Kiem tra nguoi choi co the su dung Heavy Attack hay khong
    private bool enableAttack = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        rb = this.GetComponent<Rigidbody2D>();
        KeyCode = (this.gameObject.CompareTag("P1")) ? KeyCode.U : KeyCode.Keypad4;
        keyCodeDir = (this.gameObject.CompareTag("P1")) ? KeyCode.W : KeyCode.UpArrow;
        force = 5f;
        // Tat tat ca cac collider don danh khi bat dau game
        heavyAttackForward_HurtBox.SetActive(false);
        heavyAttackUpForward_HurtBox.SetActive(false);
        heavyAttackDownForward_HurtBox.SetActive(false);

        foreach(Transform child in this.gameObject.transform)
        {
            if(child.gameObject.name == "Leg")
            {
                legPlayer = child.gameObject.GetComponent<LegPlayer>();
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        // Danh len trem
        if (Input.GetKey(keyCodeDir) && Input.GetKeyDown(KeyCode) && legPlayer.isGrounded && enableAttack)
        {
            SasukeSkill.instant.animator.SetTrigger("HeavyAttackUpforward");
            enableAttack = false;
            if (playerMovement.isFacingRight)
                rb.AddForce(new Vector2(1, 1) * force, ForceMode2D.Impulse);
            else
                rb.AddForce(new Vector2(-1, 1) * force, ForceMode2D.Impulse);
        }
        // Danh xuong duoi
        else if (!legPlayer.isGrounded && Input.GetKeyDown(KeyCode) && enableAttack)
        {
            SasukeSkill.instant.animator.SetTrigger("HeavyAttackDownforward");
            enableAttack = false;
            rb.AddForce(new Vector2(0, -1) * force, ForceMode2D.Impulse);
        }
        // Danh sang ngang
        else if (legPlayer.isGrounded && !Input.GetKeyDown(keyCodeDir) && Input.GetKeyDown(KeyCode) && enableAttack)
        {
            SasukeSkill.instant.animator.SetTrigger("HeavyAttackforward");
            enableAttack = false;
        }
    }
    /// <summary>
    /// Tao luc day khi danh xuong duoi
    /// </summary>
    public void ForceForward()
    {
        if (playerMovement.isFacingRight)
                rb.AddForce(new Vector2(1, 0) * force, ForceMode2D.Impulse);
            else
                rb.AddForce(new Vector2(-1, 0) * force, ForceMode2D.Impulse);
    }

    public void CoolDown()
    {
        StartCoroutine(CoolDownCount(coolDownTime));
    }
    private IEnumerator CoolDownCount(float time)
    {
        yield return new WaitForSeconds(time);
        enableAttack = true;
    }
    // Khi ket thuc don danh cac collider duoc tat di
    public void EndSkill()
    {
        heavyAttackForward_HurtBox.SetActive(false);
        heavyAttackUpForward_HurtBox.SetActive(false);
        heavyAttackDownForward_HurtBox.SetActive(false);
    }

    public void StartSkill()
    {

    }
    public void StartHeavyAttackForward()
    {
        heavyAttackForward_HurtBox.SetActive(true);
    }
    public void StartHeavyUpAttackForward()
    {
        heavyAttackUpForward_HurtBox.SetActive(true);
    }
    public void StartHeavyDownAttackForward()
    {
        heavyAttackDownForward_HurtBox.SetActive(true);
    }
}
