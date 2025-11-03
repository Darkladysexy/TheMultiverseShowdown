using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendDamageNormalAttack : MonoBehaviour
{
    private PlayerAttack playerAttack;
    private Collider2D hurboxCollider;
    private string tagEnemy;
    private GameObject parent;
    public int attackComboStep;
    private float force = 2f;
    void Awake()
    {
        parent = transform.parent.gameObject;

        playerAttack = parent.GetComponent<PlayerAttack>();

        tagEnemy = (parent.CompareTag("P1")) ? "P2" : "P1";
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnEnable()
    {
        // Bo loc de loc ra cac object can thiey
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        contactFilter2D.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter2D.useTriggers = true;
        // Ket qua khi loc ra cac va cham
        hurboxCollider = this.GetComponent<Collider2D>();
        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCollider(hurboxCollider, contactFilter2D, results);

        int damage = playerAttack.GetDamageForComboStep(attackComboStep);
        if (damage <= 0) return;

        foreach (Collider2D collision in results)
        {
            if (collision.gameObject.CompareTag(tagEnemy))
            {
                PlayerHealth enemyHealth = collision.gameObject.GetComponent<PlayerHealth>();
                Animator enemyAnimator = collision.gameObject.GetComponent<Animator>();
                Rigidbody2D enemyRB = collision.gameObject.GetComponent<Rigidbody2D>();

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
                    enemyHealth.TakeDamage(damage, force, vector3);
                    Debug.Log("Gây " + damage + " sát thương cho " + collision.name);
                }
            }
        }
    }
}
