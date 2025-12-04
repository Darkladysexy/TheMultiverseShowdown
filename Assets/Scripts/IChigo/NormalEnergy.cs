using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class NormalEnergy : MonoBehaviour
{
    private float force = 1;
    private string tagEnemy;
    private GameObject parent;
    private PlayerStamina playerStamina;
    private int damage;
    void Awake()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parent = GameObject.FindWithTag(this.gameObject.tag);
        tagEnemy = (parent.CompareTag("P1")) ? "P2" : "P1";
        playerStamina = parent.GetComponent<PlayerStamina>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// Neu va cham voi player thi gay sat thuong dong thoi tao hieu ung rung camera
    /// Huy game object khi co bat cu va cham nao
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tagEnemy))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            Animator enemyAnimator = collision.gameObject.GetComponent<Animator>();

            enemyAnimator.SetTrigger("TakeDamageFall");
            Vector3 vt = (-this.gameObject.transform.position + collision.gameObject.transform.position).normalized;
            playerHealth.TakeDamage(HeavyAttack.instant.damage, force, vt, true);
            playerStamina.IncreaseStamina(HeavyAttack.instant.damage);

            GameManager.instant.PauseGame(collision.gameObject.transform.position);
            CameraManager.instant.StartShake(0.1f, 0.1f,this.transform);

        }
        Destroy(this.gameObject);
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
