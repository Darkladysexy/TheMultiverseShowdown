using Unity.VisualScripting;
using UnityEngine;

public class LegPlayer : MonoBehaviour
{
    public static LegPlayer instant;
    public bool isGrounded = false;
    void Awake()
    {
        instant = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") isGrounded = true;
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") isGrounded = false;
    }
}
