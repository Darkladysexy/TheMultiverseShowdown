using Unity.VisualScripting;
using UnityEngine;

public class HeavyAttackSasuke : MonoBehaviour, InterfaceSkill
{
    public float coolDownTime { get; set; }
    public int damage { get; set; }
    public KeyCode KeyCode { get; set; }
    private KeyCode keyCodeDir { get; set; }
    private float force;
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        rb = this.GetComponent<Rigidbody2D>();
        KeyCode = (this.gameObject.CompareTag("P1")) ? KeyCode.U : KeyCode.Keypad4;
        keyCodeDir = (this.gameObject.CompareTag("P1")) ? KeyCode.W : KeyCode.UpArrow;
        force = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        if (Input.GetKey(keyCodeDir) && Input.GetKeyDown(KeyCode) && LegPlayer.instant.isGrounded)
        {
            SasukeSkill.instant.animator.SetTrigger("HeavyAttackUpforward");
            if (playerMovement.isFacingRight)
                rb.AddForce(new Vector2(1, 1) * force, ForceMode2D.Impulse);
            else
                rb.AddForce(new Vector2(-1, 1) * force, ForceMode2D.Impulse);
        }
        else if (!LegPlayer.instant.isGrounded && Input.GetKeyDown(KeyCode))
        {
            SasukeSkill.instant.animator.SetTrigger("HeavyAttackDownforward");
            rb.AddForce(new Vector2(0, -1) * force, ForceMode2D.Impulse);
        }
        else if (LegPlayer.instant.isGrounded && !Input.GetKeyDown(keyCodeDir) && Input.GetKeyDown(KeyCode))
        {
            SasukeSkill.instant.animator.SetTrigger("HeavyAttackforward");            
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
        
    }

    public void EndSkill()
    {
        
    }

    public void StartSkill()
    {
        
    }


}
