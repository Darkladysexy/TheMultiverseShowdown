using UnityEngine;

public class PlayerHealth : MonoBehaviour, ITakeDamage
{
    public int health = 100; // Mau cua player
    private Rigidbody2D rb;
    
    // --- THÊM CÁC BIẾN SAU ---
    private Animator animator;
    private PlayerMovement playerMovement;
    [HideInInspector] public bool isBlocking = false; 
    // --- KẾT THÚC THÊM BIẾN ---

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        
        // --- THÊM CÁC DÒNG SAU ---
        animator = this.gameObject.GetComponent<Animator>();
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        // --- KẾT THÚC THÊM DÒNG ---
    }

    /// <summary>
    /// khi nhan sat thuong thi health bi tru di va bi day ve sau
    /// </summary>
    /// <param name="damage">Sat thuong nhan vao</param>
    /// <param name="force">Luc day</param>
    /// <param name="dirForce">Huong bi day</param>
    /// <param name="isHeavyHit">Đây là đòn đánh ngã?</param>
    public void TakeDamage(int damage, float force, Vector3 dirForce, bool isHeavyHit) // <-- Sửa hàm này
    {
        // Nếu đang thủ (blocking) thì không làm gì cả
        if (isBlocking)
        {
            // TODO: Chơi âm thanh/hiệu ứng đỡ đòn
            rb.AddForce(dirForce * (force * 0.5f), ForceMode2D.Impulse); // Vẫn bị đẩy lùi 1 chút
            return;
        }

        rb.AddForce(dirForce * force, ForceMode2D.Impulse);
        health -= damage;

        // --- THÊM LOGIC ANIMATION VÀ STUN ---
        if (playerMovement != null)
        {
            playerMovement.StartStun(); // Bắt đầu STUN
        }

        if (animator != null)
        {
            if (isHeavyHit)
            {
                animator.SetTrigger("TakeDamageFall"); // Kích hoạt animation đánh ngã
            }
            else
            {
                animator.SetTrigger("TakeDamage"); // Kích hoạt animation thường
            }
        }
        // --- KẾT THÚC THÊM LOGIC ---
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}