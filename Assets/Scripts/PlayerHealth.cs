using UnityEngine;

public class PlayerHealth : MonoBehaviour, ITakeDamage
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
