using UnityEngine;

public class NormalEnergy : MonoBehaviour
{
    private Rigidbody2D rb;
    private readonly Vector2 LEFTUP = new Vector2Int(-1, 1);
    private readonly Vector2 RIGHTUP = new Vector2Int(1, 1);
    private readonly Vector2 LEFTDOWN = new Vector2Int(-1, -1);
    private readonly Vector2 RIGHTDOWN = new Vector2Int(1, -1);
    private Vector2 direction = new Vector2Int(1, 1);
    private GameObject IChigo;
    private PlayerMovement playerMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        // Danh ngang
        if (HeavyAttack.instant.isForward)
        {
            // sang phai
            if (PlayerMovement.instant.isFacingRight)
            {
                transform.rotation = Quaternion.Euler(0, 0, 45);
                direction = Vector2.right;
            }
            // sang trai
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 45);
                direction = Vector2.left;
            }
        }
        // Danh xuong
        else if (HeavyAttack.instant.isDownForward)
        {
            if (PlayerMovement.instant.isFacingRight) direction = RIGHTDOWN;
            else
            {
                direction = LEFTDOWN;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
        else if (HeavyAttack.instant.isUpForward)
        {
            if (PlayerMovement.instant.isFacingRight)
            {
                direction = RIGHTUP;
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else
            {
                direction = LEFTUP;
                transform.rotation = Quaternion.Euler(0, 180, 90);
            }
        }
        if(rb != null)
            rb.AddForce(direction * 0.0005f, ForceMode2D.Impulse);
        HeavyAttack.instant.isForward = false;
        HeavyAttack.instant.isUpForward = false;
        HeavyAttack.instant.isDownForward = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
