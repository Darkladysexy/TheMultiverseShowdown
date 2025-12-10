using System.Reflection.Emit;
using UnityEngine;

public class Down_Skill_Sasuke : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;
    private string enemyTag;
    [Header("Collider các kĩ năng")]
    public GameObject downNormalAttackCollider;
    public GameObject downHeavyAttackCollider;
    [Header("Sát thương các kĩ năng")]
    public int downNormalAttackDamage = 15;
    public int downHeavyAttackDamage = 25;
    public int downSpecialAttackDamage = 40;
    [Header("Down Special Skill Object")]
    public GameObject downSpecialSkillObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        enemyTag = (this.gameObject.CompareTag("P1")) ? "P2" : "P1";

        downNormalAttackCollider.GetComponent<SendDamageCloseAttack>().SetDamage(downNormalAttackDamage);
        downHeavyAttackCollider.GetComponent<SendDamageCloseAttack>().SetDamage(downHeavyAttackDamage);
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
        if(enemy != null) 
        {
            GameObject downSpecial = Instantiate(downSpecialSkillObject,enemy.transform.position,Quaternion.identity);
            downSpecial.GetComponentInChildren<DownSpecialSasukeSkill>().SetDamage(downSpecialAttackDamage);
            downSpecial.GetComponentInChildren<DownSpecialSasukeSkill>().parent = this.gameObject;
        }
    }
    public void EnableDownNormalAttackCollider()
    {
        downNormalAttackCollider.SetActive(true);
    }
    public void DisableDownNormalAttackCollider()
    {
        downNormalAttackCollider.SetActive(false);
    }
    public void EnableDownHeavyAttackCollider()
    {
        downHeavyAttackCollider.SetActive(true);
    }
    public void DisableDownHeavyAttackCollider()
    {
        downHeavyAttackCollider.SetActive(false);
    }
    
}
