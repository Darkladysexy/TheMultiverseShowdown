using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private Animator animator;
    private KeyCode keyCodeAttack;

    // === THÊM DÒNG NÀY ===
    private KeyCode keyCodeUp; // Phím W (hoặc UpArrow)

    private int comboStep = 0;
    private float lastAttackTime = 0f;
    public float comboResetTime = 0.5f;
    private bool isAttacking = false;

    public int normalAttack1_Damage = 10;
    public int normalAttack2_Damage = 15;
    public int normalAttack3_Damage = 20;

    public GameObject normalAttack1_HurtBox;
    public GameObject normalAttack2_HurtBox;
    public GameObject normalAttack3_HurtBox;

    public bool isNormalAttack1 = false;
    public bool isNormalAttack2 = false;
    public bool isNormalAttack3 = false;
    
    private LegPlayer legPlayer;
    private PlayerBlock playerBlock;

    void Awake()
    {
        
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        playerBlock = GetComponent<PlayerBlock>();
        foreach(Transform child in this.gameObject.transform)
        {
            if(child.gameObject.name == "Leg")
            {
                legPlayer = child.gameObject.GetComponent<LegPlayer>();
            }
        }
        keyCodeAttack = gameObject.CompareTag("P1") ? KeyCode.J : KeyCode.Keypad1;

        // === THÊM DÒNG NÀY ĐỂ GÁN PHÍM W ===
        keyCodeUp = gameObject.CompareTag("P1") ? KeyCode.W : KeyCode.UpArrow;

        if (normalAttack1_HurtBox != null) normalAttack1_HurtBox.SetActive(false);
        if (normalAttack2_HurtBox != null) normalAttack2_HurtBox.SetActive(false);
        if (normalAttack3_HurtBox != null) normalAttack3_HurtBox.SetActive(false);
    }

    void Update()
    {
        Debug.Log(comboStep);
        if (Time.time - lastAttackTime > comboResetTime && !isAttacking)
        {
            comboStep = 0;
        }

        bool isGrounded = legPlayer != null ? legPlayer.isGrounded : false;

        // === SỬA LẠI DÒNG IF NÀY ===
        bool isUpHeld = Input.GetKey(keyCodeUp); // Kiểm tra xem có đang giữ W không

        // Thêm điều kiện "!isUpHeld"
        if (!playerBlock.isBlocking && Input.GetKeyDown(keyCodeAttack) && isGrounded && !isUpHeld)
        {
            if (!isAttacking)
            {
                HandleNormalAttack();
            }
        }
    }

    private void HandleNormalAttack()
    {
        isAttacking = true;
        comboStep++;

        if (comboStep == 1)
        {
            animator.SetTrigger("LightAttack1");
            isNormalAttack1 = true;
        }
        else if (comboStep == 2)
        {
            animator.SetTrigger("LightAttack2");
            isNormalAttack2 = true;
        }
        else if (comboStep == 3)
        {
            animator.SetTrigger("LightAttack3");
            isNormalAttack3 = true;
            comboStep = 0;
        }
    }

    public void AttackFinished()
    {
        isAttacking = false;
        lastAttackTime = Time.time;
        isNormalAttack1 = false;
        isNormalAttack2 = false;
        isNormalAttack3 = false;

        if (normalAttack1_HurtBox != null) normalAttack1_HurtBox.SetActive(false);
        if (normalAttack2_HurtBox != null) normalAttack2_HurtBox.SetActive(false);
        if (normalAttack3_HurtBox != null) normalAttack3_HurtBox.SetActive(false);
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
        if (normalAttack1_HurtBox != null) normalAttack1_HurtBox.SetActive(true);
    }

    public void EndNormalAttack1()
    {
        if (normalAttack1_HurtBox != null) normalAttack1_HurtBox.SetActive(false);
    }

    public void StartNormalAttack2()
    {
        if (normalAttack2_HurtBox != null) normalAttack2_HurtBox.SetActive(true);
    }

    public void EndNormalAttack2()
    {
        if (normalAttack2_HurtBox != null) normalAttack2_HurtBox.SetActive(false);
    }

    public void StartNormalAttack3()
    {
        if (normalAttack3_HurtBox != null) normalAttack3_HurtBox.SetActive(true);
    }

    public void EndNormalAttack3()
    {
        if (normalAttack3_HurtBox != null) normalAttack3_HurtBox.SetActive(false);
    }
}