using System.Collections.Generic;
using UnityEngine;
public enum AttackType
{
    Normal,
    Heavy,
    Special
}
public class SendDamageCloseAttack : MonoBehaviour
{
    private string tagEnemy;
    private PlayerStamina playerStamina;
    private GameObject parent;
    private Collider2D hurboxCollider;
    private bool isHeavyHit;
    private int damage = 10;
    [Header("Lực đẩy enemy")]
    public float force = 2f;
    [Header("Loại tấn công nào")]
    public AttackType attackType = AttackType.Normal;
    private List<GameObject> listAttacked = new List<GameObject>();
    void Awake()
    {
        parent = transform.parent.gameObject;


        tagEnemy = (parent.CompareTag("P1")) ? "P2" : "P1";

        if(attackType == AttackType.Heavy)
        {
            damage = 20;
            isHeavyHit = true;
        }
        else if (attackType == AttackType.Special)
        {
            damage = 30;
            isHeavyHit = true;
        }
        else
        {
            damage = 10;
            isHeavyHit = false;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStamina = parent.GetComponent<PlayerStamina>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollison();
    }
    void OnEnable()
    {
        listAttacked.Clear();
    }
    private void CheckCollison()
    {
        // Bo loc de loc ra cac object can thiey
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        contactFilter2D.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter2D.useTriggers = true;
        // Ket qua khi loc ra cac va cham
        hurboxCollider = this.GetComponent<Collider2D>();
        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCollider(hurboxCollider, contactFilter2D, results);

        Debug.Log("Bat dau");
        foreach (Collider2D collision in results)
        {
            if (collision.gameObject.CompareTag(tagEnemy))
            {
                if(listAttacked.Contains(collision.gameObject)) continue;

                PlayerHealth enemyHealth = collision.gameObject.GetComponent<PlayerHealth>();
                PlayerBlock playerBlock = collision.gameObject.GetComponent<PlayerBlock>();
                if (enemyHealth != null && playerBlock != null)
                {
                    if(!playerBlock.isBlocking)
                    {
                        Vector3 vector3 = (collision.gameObject.transform.position - this.gameObject.transform.position).normalized;
                        enemyHealth.TakeDamage(damage, force, vector3,isHeavyHit);
                        playerStamina.IncreaseStamina(damage/2);
                        listAttacked.Add(collision.gameObject);
                        Debug.Log("Gây " + damage + " sát thương cho " + collision.name);
                    }
                }
            }
            Debug.Log("Kiem tra va cham");
        }
    }
}
