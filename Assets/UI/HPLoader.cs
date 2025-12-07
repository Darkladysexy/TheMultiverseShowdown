using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class HPLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject p1;
    private GameObject p2;
    private PlayerHealth player1Health;
    private PlayerHealth player2Health;
    public Slider hp1;
    public Slider hp2;
    void Start()
    {
        p1 = GameObject.FindGameObjectWithTag("P1");
        p2 = GameObject.FindGameObjectWithTag("P2");
        player1Health = p1.GetComponent<PlayerHealth>();
        player2Health = p2.GetComponent<PlayerHealth>();
 
        hp1.value =1;
        hp2.value =1;

        player1Health.OnChangeHealth+= UpdatePlayer1HealthUI;
        player2Health.OnChangeHealth+= UpdatePlayer2HealthUI;
    }

    // Update is called once per frame
    void Update()
    {


    }
    private void UpdatePlayer1HealthUI(int currentHealth, int maxHealth)
    {
        hp1.value = (float)currentHealth / maxHealth;
    }
    private void UpdatePlayer2HealthUI(int currentHealth, int maxHealth)
    {
        hp2.value = (float)currentHealth / maxHealth;
    }

    // void OnDestroy()
    // {
    //     if (player1Health != null)
    //         player1Health.OnChangeHealth -= UpdatePlayer1HealthUI;

    //     if (player2Health != null)
    //         player2Health.OnChangeHealth -= UpdatePlayer2HealthUI;
    // }
}
