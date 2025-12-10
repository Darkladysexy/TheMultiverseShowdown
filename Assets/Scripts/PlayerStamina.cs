using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] public int maxStamina = 100;
    [SerializeField] public int currentStamina = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int getStamina()
    {
        return currentStamina;
    }   
    public void IncreaseStamina(int amount)
    {
        currentStamina += amount;
        if (currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
        }
    }
    public void UseStamina(int amount)
    {
        currentStamina -= amount;
        Debug.Log("Stamina used: " + amount + ", Current Stamina: " + currentStamina);
    }
}
