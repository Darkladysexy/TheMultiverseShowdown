using Unity.VisualScripting;
using UnityEngine;

public class LegPlayer : MonoBehaviour
{
    [HideInInspector] public bool isGrounded = false;
    [HideInInspector] public PlayerAudio playerAudio;
    void Awake()
    {
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAudio = this.gameObject.GetComponentInParent<PlayerAudio>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    // khi va cham voi mat dat thi isGrounded = true
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            if (playerAudio != null)
            {
                playerAudio.PlayFallAudio();
            }
        }
    }
    // khi thoat khoai va cham voi mat dat thi isGrounded = false
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") isGrounded = false;
    }
}
