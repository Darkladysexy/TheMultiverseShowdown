using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float coolDownTime { get ; set;}
    public KeyCode keyCode { get; set; }
    public float force = 10f;
    private bool enableDash = true;
    private PlayerMovement playerMovement;
    private BoxCollider2D collder;
    private LegPlayer legPlayer;
    private int playerLayer;
    private int dashingLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coolDownTime = 1f;
        if (this.gameObject.tag == "P1") keyCode = KeyCode.L;
        else keyCode = KeyCode.Keypad3;
        playerMovement = this.GetComponent<PlayerMovement>();
        collder = this.GetComponent<BoxCollider2D>();
        legPlayer = this.GetComponent<LegPlayer>();

        playerLayer = LayerMask.NameToLayer("Player");
        dashingLayer = LayerMask.NameToLayer("DashingPlayer");
    }

    // Update is called once per frame
    void Update()
    {
        Dash();
        Debug.Log(this.gameObject.layer);
    }
    
    public void Dash()
    {
        if(Input.GetKeyDown(keyCode) && enableDash)
        {
            playerMovement.animator.SetTrigger("Dash");
            if (playerMovement.isFacingRight) playerMovement.rb.AddForce(Vector2.right * force, ForceMode2D.Impulse);
            else playerMovement.rb.AddForce(Vector2.left * force, ForceMode2D.Impulse);
        }
    }

    public void CoolDown()
    {
        StartCoroutine(Counter(coolDownTime));

    }
    private IEnumerator Counter(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);

        enableDash = true;
    }

    public void StartDash()
    {
        enableDash = false;
        collder.isTrigger = false;
        this.gameObject.layer = dashingLayer;
        foreach(Transform child in this.gameObject.transform)
        {
            child.gameObject.layer = dashingLayer;
        }
    }
    public void EndSDash()
    {
        this.gameObject.layer = playerLayer;  
        foreach(Transform child in this.gameObject.transform)
        {
            child.gameObject.layer = playerLayer;
        }
    }


}
