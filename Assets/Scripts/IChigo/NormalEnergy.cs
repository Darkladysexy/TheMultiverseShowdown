using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class NormalEnergy : MonoBehaviour
{
    private Rigidbody2D rb;
    private readonly Vector2 LEFTUP = new Vector2Int(-1, 1);
    private readonly Vector2 RIGHTUP = new Vector2Int(1, 1);
    private readonly Vector2 LEFTDOWN = new Vector2Int(-1, -1);
    private readonly Vector2 RIGHTDOWN = new Vector2Int(1, -1);
    private Vector2 direction = new Vector2Int(1, 1);
    private PlayerMovement playerMovement;
    private float force = 1;
    void Awake()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        playerMovement = HeavyAttack.instant.playerMovement;
        // Danh ngang
        if (HeavyAttack.instant.isForward)
        {
            // sang phai
            if (playerMovement.isFacingRight)
            {
                transform.rotation = Quaternion.Euler(0, 0, 45);
                direction = Vector2.right;
            }
            // sang trai
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 45);
                direction = Vector2.left;
            }
        }
        // Danh xuong
        else if (HeavyAttack.instant.isDownForward)
        {
            // Sang phai
            if (playerMovement.isFacingRight) direction = RIGHTDOWN;
            // Sang trai
            else
            {
                direction = LEFTDOWN;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
        // Danh len
        else if (HeavyAttack.instant.isUpForward)
        {
            // Sang phai
            if (playerMovement.isFacingRight)
            {
                direction = RIGHTUP;
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            // Sang trai
            else
            {
                direction = LEFTUP;
                transform.rotation = Quaternion.Euler(0, 180, 90);
            }
        }
        if(rb != null)
            rb.AddForce(direction * 0.0005f, ForceMode2D.Impulse);
        HeavyAttack.instant.isForward = false;
        HeavyAttack.instant.isUpForward = false;
        HeavyAttack.instant.isDownForward = false;
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
            playerHealth.TakeDamage(HeavyAttack.instant.damage, force, vt);

            GameManager.instant.PauseGame(collision.gameObject.transform.position);
            CameraManager.instant.StartShake(0.1f, 0.1f,this.transform);

        }
        Destroy(this.gameObject);
    }
}
