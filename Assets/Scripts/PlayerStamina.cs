using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] private int maxStamina = 100;
    [SerializeField] public int stamina = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseStamina(int amount)
    {
        stamina += amount;
        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }
    }
}
