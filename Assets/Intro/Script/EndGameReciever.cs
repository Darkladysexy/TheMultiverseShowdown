using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class EndGameReciever : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject p1;
    private GameObject p2;    
    public GameObject P1;
    public GameObject P2;
    void Start()
    {
        p1 = GameObject.FindGameObjectWithTag("P1");
        p2 = GameObject.FindGameObjectWithTag("P2");
    }
    void Update()
    {
        BackToMenu();
    }
    public void PlayerWin()
    {
        if(p1.GetComponent<PlayerHealth>().currentHealth <= 0)
        {
            P2.SetActive(true);
            p2.GetComponent<PlayerMovement>().isStun = true   ;
        }else
        {
            P1.SetActive(true);
            p1.GetComponent<PlayerMovement>().isStun = true   ;
        }
    }
    public void BackToMenu()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
