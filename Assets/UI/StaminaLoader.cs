using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StaminaLoader : MonoBehaviour
{
    
    private GameObject p1;
    private GameObject p2;
    public Slider stm1;
    public Slider stm2;
    private int stamina1;
    private int stamina2;
    void Awake()
    {
        stm1.value = 0f;
        stm2.value = 0f;
    }
    void Start()
    {
        p1 = GameObject.FindGameObjectWithTag("P1");
        p2 = GameObject.FindGameObjectWithTag("P2");
    }

    // Update is called once per frame
    void Update()
    {
        
        stamina1 = p1.GetComponent<PlayerStamina>().getStamina();
        stamina2 = p2.GetComponent<PlayerStamina>().getStamina();

        stm1.value =(float) stamina1/100;
        stm2.value =(float) stamina2/100;
    }

}
