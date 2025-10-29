using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendDamageNormalAttack : MonoBehaviour
{
    private string tagGameObject;
    private string tagEnemy;
    private GameObject parent;
    private PlayerAttack playerAttack;
    public int attackComboStep;
    private Collider2D hurboxCollider;
    private float force = 0.05f;
    void Awake()
    {
        tagGameObject = transform.parent.gameObject.tag;
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
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        contactFilter2D.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter2D.useTriggers = true;

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
                    enemyHealth.TakeDamage(damage);
                    if (attackComboStep == 3)
                    {
                        enemyAnimator.SetTrigger("TakeDamageFall");
                        StartCoroutine(PauseGame());
                        CameraManager.instant.StartShake(0.1f, 0.1f,this.transform);
                    }
                    else enemyAnimator.SetTrigger("TakeDamage");
                    Vector3 vector3 = (collision.gameObject.transform.position - this.gameObject.transform.position).normalized;
                    enemyRB.AddForce(vector3, ForceMode2D.Impulse);
                    Debug.Log("Gây " + damage + " sát thương cho " + collision.name);
                }
            }
        }
    }

    private IEnumerator PauseGame()
    {
        Time.timeScale = 0f;
        CameraManager.instant.ChangePostionCamera(this.transform.position);
        yield return new WaitForSecondsRealtime(0.2f);
        CameraManager.instant.ChangePostionCamera(CameraManager.instant.transformCamera.position);
        Time.timeScale = 1f;
    }

    // void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if(collision.gameObject.CompareTag(tagEnemy))
    //     {
    //         int damage = playerAttack.GetDamageForComboStep(attackComboStep);

    //         if(damage!=0)
    //         {
    //             PlayerHealth enemyHealth = collision.gameObject.GetComponent<PlayerHealth>();
    //             enemyHealth.TakeDamage(damage);
    //             Debug.Log(damage);
    //         }
    //     }
    // }
}
