using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float coolDownTime { get ; set;}
    public KeyCode keyCode { get; set; }
    public float force = 10f;
    private bool enableDash = true;
    private PlayerMovement playerMovement;

    private int playerLayer;
    private int dashingLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coolDownTime = 2f;
        if (this.gameObject.tag == "P1") keyCode = KeyCode.L;
        else keyCode = KeyCode.Keypad3;
        playerMovement = this.GetComponent<PlayerMovement>();

        playerLayer = LayerMask.NameToLayer("Player");
        dashingLayer = LayerMask.NameToLayer("DashingPlayer");
    }

    // Update is called once per frame
    void Update()
    {
        Dash();
    }
    
    public void Dash()
    {
        if(Input.GetKeyDown(keyCode) && enableDash)
        {
            enableDash = false;
            playerMovement.animator.SetTrigger("Dash");
            if (playerMovement.isFacingRight) playerMovement.rb.AddForce(Vector2.right * force, ForceMode2D.Impulse);
            else playerMovement.rb.AddForce(Vector2.left * force, ForceMode2D.Impulse);
        }
    }

    public void CoolDownDash()
    {
        this.gameObject.layer = dashingLayer;
        foreach (Transform child in this.gameObject.transform)
        {
            child.gameObject.layer = dashingLayer;
        }

        StartCoroutine(Counter(coolDownTime));

        this.gameObject.layer = playerLayer;
        foreach (Transform child in this.gameObject.transform)
        {
            child.gameObject.layer = playerLayer;
        }

    }
    private IEnumerator Counter(float timeDelay)
    {   
        

        yield return new WaitForSeconds(timeDelay);
   
        enableDash = true;
    }
}
