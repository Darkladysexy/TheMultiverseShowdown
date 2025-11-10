using UnityEngine;

public class KakashiKunai : MonoBehaviour
{
    [SerializeField] private int damage = 8;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private float knockbackForce = 0f; // <-- ĐẶT LÀ 0
    
    private Rigidbody2D rb;
    private Vector2 direction;
    private string enemyTag = "P2";
    private bool hasBeenInitialized = false;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Destroy(gameObject);
            return;
        }
    }
    
    void Start()
    {
        if (gameObject.CompareTag("P1Projectile"))
            enemyTag = "P2";
        else if (gameObject.CompareTag("P2Projectile"))
            enemyTag = "P1";
        
        Destroy(gameObject, lifetime);
    }
    
    public void Initialize(bool facingRight)
    {
        if (hasBeenInitialized || rb == null) return;
        hasBeenInitialized = true;
        direction = facingRight ? Vector2.right : Vector2.left;
        if (!facingRight)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        rb.linearVelocity = direction * speed;
    }
    
    public void InitializeAerial(bool facingRight)
    {
        if (hasBeenInitialized || rb == null) return;
        hasBeenInitialized = true;
        if (facingRight)
        {
            direction = new Vector2(1, -1).normalized;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            direction = new Vector2(-1, -1).normalized;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        rb.linearVelocity = direction * speed;
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == gameObject) return;
        
        if (collision.CompareTag(enemyTag))
        {
            ITakeDamage takeDamage = collision.GetComponent<ITakeDamage>();
            if (takeDamage != null)
            {
                Vector3 hitDirection = (collision.transform.position - transform.position).normalized;
                // Sửa lại: Lực văng = 0, và isHeavyHit = false
                takeDamage.TakeDamage(damage, knockbackForce, hitDirection, false);
            }
            Destroy(gameObject);
        }
    }
    
    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }
    
    public int GetDamage()
    {
        return damage;
    }
}