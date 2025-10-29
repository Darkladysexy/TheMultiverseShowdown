using UnityEngine;

public class PlayerHealth : MonoBehaviour, ITakeDamage
{
    public int health = 100;
    private Rigidbody2D rb;

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
