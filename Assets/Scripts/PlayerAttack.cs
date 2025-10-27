using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    /*
        - isAttack = false, combostep = 1
        - khi người chơi bấm phím attack: isAttack = true;
        - Khi animation normalAttack thực hiện gọi hàm kiểm tra xem người chơi có bấm phím đánh lần nữa không, nếu có thì thì isAttack = true, comboStep +=1

    */
    private Animator animator;
    private KeyCode keyCodeAttack;
    private int comboStep = 0;
    private float lastAttackTime = 0f;
    
    // 1.5 giây để nhấn đòn tiếp theo, nếu không combo sẽ reset
    public float comboResetTime = 0.5f; 
    
    // Cờ này ngăn người chơi spam nút đánh
    private bool isAttacking = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = this.GetComponent<Animator>();
        keyCodeAttack = (this.gameObject.CompareTag("P1")) ? KeyCode.J : KeyCode.Alpha1;
    }

    // Update is called once per frame
    void Update()
    {
        // 1. Tự động reset combo nếu chờ quá lâu
        // Chỉ reset khi không đang tấn công
        if (Time.time - lastAttackTime > comboResetTime && !isAttacking)
        {
            comboStep = 0;
        }

        // 2. Kiểm tra input
        if (Input.GetKeyDown(keyCodeAttack) && LegPlayer.instant.isGrounded)
        {
            // Chỉ cho phép đánh nếu đòn trước đã kết thúc
            if (!isAttacking)
            {
                HandleAttack();
            }
        }
        Debug.Log(comboStep);
    }
    private void HandleAttack()
    {
        // Đánh dấu là "đang tấn công"
        isAttacking = true;

        // Tăng bước combo lên
        comboStep++;

        // 3. Thực hiện đòn đánh dựa trên bước combo
        if (comboStep == 1)
        {
            // Đây là đòn đầu tiên (hoặc đòn sau khi bị reset)
            animator.SetTrigger("LightAttack1"); // Kích hoạt animation Attack1
        }
        else if (comboStep == 2)
        {
            animator.SetTrigger("LightAttack2"); // Kích hoạt animation Attack2
        }
        else if (comboStep == 3)
        {
            animator.SetTrigger("LightAttack3"); // Kích hoạt animation Attack3

            // Đã hết 3 đòn, reset ngay lập tức về 0
            comboStep = 0;
        }
    }
    // 4. HÀM NÀY RẤT QUAN TRỌNG
    // Bạn cần gọi hàm này từ Animation Event
    // khi animation tấn công KẾT THÚC.
    public void AttackFinished()
    {
        lastAttackTime = Time.time;
        isAttacking = false;
        Debug.Log("Test");
    }
    
}
