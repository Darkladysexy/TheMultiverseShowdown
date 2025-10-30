using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendDamageHeavyAttackSasuke : MonoBehaviour
{
    private string tagEnemy;
    private GameObject parent;
    private HeavyAttackSasuke heavyAttackSasuke;
    private Collider2D hurboxCollider;
    private float force = 1.5f;
    void Awake()
    {
        parent = transform.parent.gameObject;

        heavyAttackSasuke = parent.GetComponent<HeavyAttackSasuke>();

        tagEnemy = (parent.CompareTag("P1")) ? "P2" : "P1";
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnEnable()
    {
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        contactFilter2D.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter2D.useTriggers = true;

        hurboxCollider = this.GetComponent<Collider2D>();
        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCollider(hurboxCollider, contactFilter2D, results);

        int damage = heavyAttackSasuke.damage;

        foreach (Collider2D collision in results)
        {
            if (collision.gameObject.CompareTag(tagEnemy))
            {
                PlayerHealth enemyHealth = collision.gameObject.GetComponent<PlayerHealth>();
                Animator enemyAnimator = collision.gameObject.GetComponent<Animator>();
                if (enemyHealth != null)
                {
                    
                    // StartCoroutine(PauseGame());
                    GameManager.instant.PauseGame(this.transform.position);
                    CameraManager.instant.StartShake(0.1f, 0.1f,this.transform);
                    enemyAnimator.SetTrigger("TakeDamageFall");
                    
                    Vector3 vector3 = (collision.gameObject.transform.position - this.gameObject.transform.position).normalized;
                    enemyHealth.TakeDamage(damage, force, vector3);
                    Debug.Log("Gây " + damage + " sát thương cho " + collision.name);
                }
            }
        }
    }

}
