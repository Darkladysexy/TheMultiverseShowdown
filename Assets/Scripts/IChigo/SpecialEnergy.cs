using UnityEngine;

public class SpecialEnergy : MonoBehaviour
{
    private float force = 1f; // Luc day nang luong
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
    /// Khi trung player thi gay sat thuong
    /// Huy di khi co bat cu va cham nao
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(tagEnemy))
        {
            PlayerHealth enemyHealth = collision.gameObject.GetComponent<PlayerHealth>();

            Vector3 vt3 = (this.gameObject.transform.position + collision.gameObject.transform.position).normalized;
            // enemyAnimator.SetTrigger("TakeDamageFall"); // Không cần nữa, PlayerHealth tự xử lý
            Debug.Log(damage);
            // SỬA DÒNG NÀY: Thêm 'true' vì đây là đòn 'Fall'
            playerStamina.IncreaseStamina(damage);
            enemyHealth.TakeDamage(damage, force, vt3, true);
            GameManager.instant.PauseGame(collision.gameObject.transform.position);
            CameraManager.instant.StartShake(0.1f, 0.1f,this.transform);
        }
        Destroy(this.gameObject);
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public void DestroyObj()
    {
        Destroy(this.gameObject);
    }
}