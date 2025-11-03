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
    private LegPlayer legPlayer;
    private KeyCode keyCodeAttack;
    private int comboStep = 0;
    private float lastAttackTime = 0f;
    public int normalAttack1_Damage;
    public int normalAttack2_Damage;
    public int normalAttack3_Damage;
    public GameObject normalAttack1_HurtBox;
    public GameObject normalAttack2_HurtBox;
    public GameObject normalAttack3_HurtBox;
    public bool isNormalAttack1 = false;
    public bool isNormalAttack2 = false;
    public bool isNormalAttack3 = false;

    
    // 1.5 giây để nhấn đòn tiếp theo, nếu không combo sẽ reset
    public float comboResetTime = 0.5f; 
    
    // Cờ này ngăn người chơi spam nút đánh
    private bool isAttacking = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = this.GetComponent<Animator>();
        foreach(Transform child in this.gameObject.transform)
        {
            if(child.gameObject.name == "Leg")
            {
                legPlayer = child.gameObject.GetComponent<LegPlayer>();
            }
        }
        keyCodeAttack = (this.gameObject.CompareTag("P1")) ? KeyCode.J : KeyCode.Keypad1;

        normalAttack1_HurtBox.SetActive(false);
        normalAttack2_HurtBox.SetActive(false);
        normalAttack3_HurtBox.SetActive(false);

        normalAttack1_Damage = 10;
        normalAttack2_Damage = 15;
        normalAttack3_Damage = 20;
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
        if (Input.GetKeyDown(keyCodeAttack) && legPlayer.isGrounded)
        {
            // Chỉ cho phép đánh nếu đòn trước đã kết thúc
            if (!isAttacking)
            {
                HandleAttack();
            }
        }
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
            isNormalAttack1 = true;
        }
        else if (comboStep == 2)
        {
            animator.SetTrigger("LightAttack2"); // Kích hoạt animation Attack2
            isNormalAttack2 = true;
        }
        else if (comboStep == 3)
        {
            animator.SetTrigger("LightAttack3"); // Kích hoạt animation Attack3
            isNormalAttack3 = true;
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
        isNormalAttack1 = false;
        isNormalAttack2 = false;
        isNormalAttack3 = false;
    }
    public int GetDamageForComboStep(int step)
    {
        if (step == 1) return normalAttack1_Damage;
        if (step == 2) return normalAttack2_Damage;
        if (step == 3) return normalAttack3_Damage;
        
        return 0; 
    }
    public void StartNormalAttack1()
    {
        normalAttack1_HurtBox.SetActive(true);
    }
    public void EndNormalAttack1()
    {
        normalAttack1_HurtBox.SetActive(false);
    }
    public void StartNormalAttack2()
    {
        normalAttack2_HurtBox.SetActive(true);
    }
    public void EndNormalAttack2()
    {
        normalAttack2_HurtBox.SetActive(false);
    }
    public void StartNormalAttack3()
    {
        normalAttack3_HurtBox.SetActive(true);
    }
    public void EndNormalAttack3()
    {
        normalAttack3_HurtBox.SetActive(false);
    }
    
}
