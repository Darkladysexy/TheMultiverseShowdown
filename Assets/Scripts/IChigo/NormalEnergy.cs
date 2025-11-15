using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class NormalEnergy : MonoBehaviour
{
    private float force = 1;
    void Awake()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        if (collision.gameObject.CompareTag("P1") || collision.gameObject.CompareTag("P2"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            Animator enemyAnimator = collision.gameObject.GetComponent<Animator>();

            enemyAnimator.SetTrigger("TakeDamageFall");
            Vector3 vt = (-this.gameObject.transform.position + collision.gameObject.transform.position).normalized;
            playerHealth.TakeDamage(HeavyAttack.instant.damage, force, vt, true);

            GameManager.instant.PauseGame(collision.gameObject.transform.position);
            CameraManager.instant.StartShake(0.1f, 0.1f,this.transform);

        }
        Destroy(this.gameObject);
    }
}
