using System.Collections;
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
    void Awake()
    {
        hp1.value = 1f;
        hp2.value = 1f;
    }
    void Start()
    {
        p1 = GameObject.FindGameObjectWithTag("P1");
        p2 = GameObject.FindGameObjectWithTag("P2");

        if (p1 != null)
            player1Health = p1.GetComponent<PlayerHealth>();

        if (p2 != null)
            player2Health = p2.GetComponent<PlayerHealth>();

        // Subscribe once to the health-change events so UI updates when health changes.
        if (player1Health != null)
            player1Health.OnChangeHealth += UpdatePlayer1HealthUI;

        if (player2Health != null)
            player2Health.OnChangeHealth += UpdatePlayer2HealthUI;
    }
    // No Update() required — UI updates via events from PlayerHealth.
    private void UpdatePlayer1HealthUI(int currentHealth, int maxHealth)
    {
        hp1.value = (float)currentHealth / maxHealth;
    }
    private void UpdatePlayer2HealthUI(int currentHealth, int maxHealth)
    {
        hp2.value = (float)currentHealth / maxHealth;
    }
    // removed unused coroutine

    void OnDestroy()
    {
        // Unsubscribe from events to prevent memory leaks.
        if (player1Health != null)
            player1Health.OnChangeHealth -= UpdatePlayer1HealthUI;

        if (player2Health != null)
            player2Health.OnChangeHealth -= UpdatePlayer2HealthUI;
    }
}
