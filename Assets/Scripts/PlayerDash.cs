using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public float coolDownTime { get ; set;}
    [HideInInspector] public KeyCode keyCode { get; set; }
    public float force = 10f; // Luc day khi dash
    private bool enableDash = true; // Kiem tra player co the dash hay khong
    private int playerLayer; // Layer cua player
    private int dashingLayer; // Layer cua player khi dash

    void Start()
    {
        playerMovement = this.GetComponent<PlayerMovement>();
        playerLayer = LayerMask.NameToLayer("Player");
        dashingLayer = LayerMask.NameToLayer("DashingPlayer");

        coolDownTime = 2f;
        if (this.gameObject.tag == "P1") keyCode = KeyCode.L;
        else keyCode = KeyCode.Keypad3;
    }

    void Update()
    {
        Dash();
    }
    
    public void Dash()
    {
        // === SỬA LỖI TẠI ĐÂY ===
        // Thêm "!playerMovement.isStun"
        if(Input.GetKeyDown(keyCode) && enableDash && !playerMovement.isStun)
        // ======================
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