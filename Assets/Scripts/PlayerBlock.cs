using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerMovement playerMovement;
    // private PlayerHealth playerHealth;
    private LegPlayer legPlayer;
    public bool isBlocking = false;
    private KeyCode keyCode;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keyCode = (this.gameObject.CompareTag("P1")) ? KeyCode.S : KeyCode.DownArrow;
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        // playerHealth = this.gameObject.GetComponent<PlayerHealth>();
        foreach(Transform child in this.gameObject.transform)
        {
            if(child.gameObject.name == "Leg")
            {
                legPlayer = child.gameObject.GetComponent<LegPlayer>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleBlocking();
        animator.SetBool("IsBlocking", isBlocking);
    }
    public void StartBlocking()
    {
        if (isBlocking) return; 
        isBlocking = true;
        
    }

    public void StopBlocking()
    {
        if (!isBlocking) return;
        isBlocking = false;
    }
    public void HandleBlocking()
    {
        // Nếu giữ 'S', trên mặt đất, VÀ không nhấn J/U/I trong frame này
        if (legPlayer.isGrounded && Input.GetKey(keyCode))
        {
            playerMovement.StartStun();
            StartBlocking();
        }
        else
        {
            playerMovement.EndStun();
            StopBlocking();
        }
    }
}
