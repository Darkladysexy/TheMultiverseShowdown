using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class HPLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject p1;
    private GameObject p2;
    public Slider hp1;
    public Slider hp2;
    private int health1;
    private int health2;
    void Start()
    {
        p1 = GameObject.FindGameObjectWithTag("P1");
        p2 = GameObject.FindGameObjectWithTag("P2");
 
        hp1.value =1;
        hp2.value =1;
    }

    // Update is called once per frame
    void Update()
    {
        health1 = p1.GetComponent<PlayerHealth>().health;
        health2 = p2.GetComponent<PlayerHealth>().health;

        hp1.value =(float) health1/100;
        hp2.value =(float) health2/100;
    }
}
