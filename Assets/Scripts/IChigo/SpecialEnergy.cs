using UnityEngine;

public class SpecialEnergy : MonoBehaviour
{
    private Rigidbody2D rb;
    private float force = 1f; // Luc day nang luong
    private PlayerMovement playerMovement;
    void Awake()
    {
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        playerMovement = SpeacialAttack.instant.playerMovement;
        // Tan cong sang phai
        if (playerMovement.isFacingRight) rb.AddForce(new Vector2(1, 0) * 0.001f, ForceMode2D.Impulse);
        // Tan cong sang trai
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            rb.AddForce(new Vector2(-1, 0) * 0.001f, ForceMode2D.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// Khi trung player thi gay sat thuong
    /// Huy di khi co bat cu va cham nao
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("P1") || collision.gameObject.CompareTag("P2"))
        {
            PlayerHealth enemyHealth = collision.gameObject.GetComponent<PlayerHealth>();
            Animator enemyAnimator = collision.gameObject.GetComponent<Animator>();

            Vector3 vt3 = (-SpeacialAttack.instant.gameObject.transform.position + collision.gameObject.transform.position).normalized;
            enemyAnimator.SetTrigger("TakeDamageFall");
            enemyHealth.TakeDamage(SpeacialAttack.instant.damage, force, vt3);

            GameManager.instant.PauseGame(collision.gameObject.transform.position);
            CameraManager.instant.StartShake(0.1f, 0.1f,this.transform);
        }
        Destroy(this.gameObject);
    }

    public void DestroyObj()
    {
        Destroy(this.gameObject);
    }
}
