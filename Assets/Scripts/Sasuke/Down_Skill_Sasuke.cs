using System.Reflection.Emit;
using UnityEngine;

public class Down_Skill_Sasuke : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;
    private string enemyTag;
    public GameObject downSpecialSkillObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        enemyTag = (this.gameObject.CompareTag("P1")) ? "P2" : "P1";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DownNormalAttack()
    {
        animator.SetTrigger("DownNormalAttack");
        playerMovement.StartStun();
    }
    public void DownHeavyAttack()
    {
        animator.SetTrigger("DownHeavyAttack");
        playerMovement.StartStun();
        rb.linearVelocity = new Vector2(0, 0);
        if(playerMovement.isFacingRight) rb.AddForce(new Vector2(1,1) * 3.5f, ForceMode2D.Impulse);
        else rb.AddForce(new Vector2(-1,1) * 3.5f, ForceMode2D.Impulse);
    }
    public void DownSpecialAttack()
    {
        animator.SetTrigger("DownSpecialAttack");
        playerMovement.StartStun();
    }
    private void SpawnDownSpecialSkill()
    {
        GameObject enemy = GameObject.FindWithTag(enemyTag);
        if(enemy != null) Instantiate(downSpecialSkillObject,enemy.transform.position,Quaternion.identity);
    }
    
}
