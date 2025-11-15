using UnityEngine;

public class SpecialEnergy : MonoBehaviour
{
    private float force = 1f; // Luc day nang luong
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
    /// Khi trung player thi gay sat thuong
    /// Huy di khi co bat cu va cham nao
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("P1") || collision.gameObject.CompareTag("P2"))
        {
            PlayerHealth enemyHealth = collision.gameObject.GetComponent<PlayerHealth>();

            Vector3 vt3 = (this.gameObject.transform.position + collision.gameObject.transform.position).normalized;
            // enemyAnimator.SetTrigger("TakeDamageFall"); // Không cần nữa, PlayerHealth tự xử lý
            
            // SỬA DÒNG NÀY: Thêm 'true' vì đây là đòn 'Fall'
            enemyHealth.TakeDamage(SpeacialAttack.instant.damage, force, vt3, true);

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