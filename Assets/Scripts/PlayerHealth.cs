using UnityEngine;

public class PlayerHealth : MonoBehaviour, ITakeDamage
{
    public int health = 100; // Mau cua player
    private Rigidbody2D rb;
    /// <summary>
    /// khi nhan sat thuong thi health bi tru di va bi day ve sau
    /// </summary>
    /// <param name="damage">Sat thuong nhan vao</param>
    /// <param name="force">Luc day</param>
    /// <param name="dirForce">Huong bi day</param>
    public void TakeDamage(int damage, float force, Vector3 dirForce)
    {
        rb.AddForce(dirForce * force, ForceMode2D.Impulse);
        health -= damage;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
