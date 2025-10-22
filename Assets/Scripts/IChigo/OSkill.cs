using UnityEngine;

public class OSkill : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        if (PlayerMovement.instant.gameObject.transform.rotation.y >= 0) rb.AddForce(new Vector2(1, 0) * 0.001f, ForceMode2D.Impulse);
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            rb.AddForce(new Vector2(-1, 0) * 0.001f, ForceMode2D.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void DestroyObj()
    {
        Destroy(this.gameObject);
    }
}
