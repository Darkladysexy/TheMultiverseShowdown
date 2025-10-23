using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float coolDownTime { get ; set;}
    public KeyCode keyCode { get; set; }
    public float force = 10f;
    private bool enableDash = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coolDownTime = 1f;
        if (this.gameObject.tag == "P1") keyCode = KeyCode.L;
        else keyCode = KeyCode.Alpha3;
    }

    // Update is called once per frame
    void Update()
    {
        Dash();
        Debug.Log(enableDash);
    }
    
    public void Dash()
    {
        if(Input.GetKeyDown(keyCode) && enableDash)
        {
            PlayerMovement.instant.animator.SetTrigger("Dash");
            if (PlayerMovement.instant.isFacingRight) PlayerMovement.instant.rb.AddForce(Vector2.right * force, ForceMode2D.Impulse);
            else PlayerMovement.instant.rb.AddForce(Vector2.left * force, ForceMode2D.Impulse);
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

    public void StartSkill()
    {
        enableDash = false;
    }


}
