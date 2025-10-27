using UnityEngine;

public class SpecialAttackSasuke : MonoBehaviour,InterfaceSkill
{
    public float coolDownTime { get; set; }
    public int damage { get ; set; }
    public KeyCode KeyCode { get; set; }
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        KeyCode = (this.gameObject.CompareTag("P1")) ? KeyCode.I : KeyCode.Alpha5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode) && LegPlayer.instant.isGrounded)
        {
            SasukeSkill.instant.animator.SetTrigger("SpecialAttack");
        }
    }

    public void CoolDown()
    {
        
    }

    public void EndSkill()
    {
        
    }

    public void StartSkill()
    {
        if (PlayerMovement.instant.isFacingRight)
            rb.AddForce(Vector2.right * 5f, ForceMode2D.Impulse);
        else
            rb.AddForce(Vector2.left * 5f, ForceMode2D.Impulse);
    }


}
