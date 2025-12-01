using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class SendDamageNormalAttack : MonoBehaviour
{
    private PlayerAttack playerAttack;
    private PlayerStamina playerStamina;
    private Collider2D hurboxCollider;
    private string tagEnemy;
    private GameObject parent;
    public int attackComboStep;
    private float force = 2f;
    int damage;
    private List<GameObject> listAttacked = new List<GameObject>();
    ContactFilter2D contactFilter2D;
    List<Collider2D> results;
    void Awake()
    {
        parent = transform.parent.gameObject;

        playerAttack = parent.GetComponent<PlayerAttack>();
        playerStamina = parent.GetComponent<PlayerStamina>();

        hurboxCollider = this.GetComponent<Collider2D>();

        tagEnemy = (parent.CompareTag("P1")) ? "P2" : "P1";
        // Debug.Log(this.gameObject.name + " " + parent.gameObject.tag);
        // damage = playerAttack.GetDamageForComboStep(attackComboStep);
        // Bo loc de loc ra cac object can thiey
        contactFilter2D = new ContactFilter2D();
        results = new List<Collider2D>();
        contactFilter2D.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter2D.useTriggers = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollison();
        
    }
    void OnEnable()
    {
        listAttacked.Clear();
        if (playerAttack != null)
        {
            damage = playerAttack.GetDamageForComboStep(attackComboStep);
            Debug.Log(damage);
        }
    }

    private void CheckCollison()
    {
        
        Debug.Log(tagEnemy);
        // Ket qua khi loc ra cac va cham
        Physics2D.OverlapCollider(hurboxCollider, contactFilter2D, results);
        
        if (damage <= 0) return;

        foreach (Collider2D collision in results)
        {
                Debug.Log("va cham voi " + collision.name);
            if (collision.gameObject.CompareTag(tagEnemy))
            {
                if(listAttacked.Contains(collision.gameObject)) continue;

                PlayerHealth enemyHealth = collision.gameObject.GetComponent<PlayerHealth>();
                Animator enemyAnimator = collision.gameObject.GetComponent<Animator>();
                // Rigidbody2D enemyRB = collision.gameObject.GetComponent<Rigidbody2D>();

                if (enemyHealth != null)
                {
                    
                    if (attackComboStep == 3)
                    {
                        enemyAnimator.SetTrigger("TakeDamageFall");
                        GameManager.instant.PauseGame(this.transform.position);
                        CameraManager.instant.StartShake(0.1f, 0.1f,this.transform);
                    }
                    else enemyAnimator.SetTrigger("TakeDamage");
                    Vector3 vector3 = (collision.gameObject.transform.position - this.gameObject.transform.position).normalized;
                    enemyHealth.TakeDamage(damage, force, vector3,false);
                    playerStamina.IncreaseStamina(damage / 2);
                    listAttacked.Add(collision.gameObject);
                    Debug.Log("Gây " + damage + " sát thương cho " + collision.name);
                }
            }
        }   
    }
    // void OnCollisionStay2D(Collision2D collision)
    // {
    //     Debug.Log("Va cham voi " + collision.gameObject.name);
    // }
}
