using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class HeavyAttackSasuke : MonoBehaviour, InterfaceSkill
{
    public float coolDownTime { get; set; } = 2f;
    public int damage { get; set; } = 20;
    public KeyCode KeyCode { get; set; }
    private KeyCode keyCodeDir { get; set; }
    private float force;
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    private LegPlayer legPlayer;
    public GameObject heavyAttackForward_HurtBox;
    public GameObject heavyAttackDownForward_HurtBox;
    public GameObject heavyAttackUpForward_HurtBox;
    private bool enableAttack = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        damage = 30;
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        rb = this.GetComponent<Rigidbody2D>();
        KeyCode = (this.gameObject.CompareTag("P1")) ? KeyCode.U : KeyCode.Keypad4;
        keyCodeDir = (this.gameObject.CompareTag("P1")) ? KeyCode.W : KeyCode.UpArrow;
        force = 5f;

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
        if (Input.GetKey(keyCodeDir) && Input.GetKeyDown(KeyCode) && legPlayer.isGrounded && enableAttack)
        {
            SasukeSkill.instant.animator.SetTrigger("HeavyAttackUpforward");
            enableAttack = false;
            if (playerMovement.isFacingRight)
                rb.AddForce(new Vector2(1, 1) * force, ForceMode2D.Impulse);
            else
                rb.AddForce(new Vector2(-1, 1) * force, ForceMode2D.Impulse);
        }
        else if (!legPlayer.isGrounded && Input.GetKeyDown(KeyCode) && enableAttack)
        {
            SasukeSkill.instant.animator.SetTrigger("HeavyAttackDownforward");
            enableAttack = false;
            rb.AddForce(new Vector2(0, -1) * force, ForceMode2D.Impulse);
        }
        else if (legPlayer.isGrounded && !Input.GetKeyDown(keyCodeDir) && Input.GetKeyDown(KeyCode) && enableAttack)
        {
            SasukeSkill.instant.animator.SetTrigger("HeavyAttackforward"); 
            enableAttack = false;
        }
    }
    
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
