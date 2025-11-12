using UnityEngine;

public class IChigoSkillManager : SkillManager
{
    private HeavyAttack heavyAttack;
    private SpeacialAttack speacialAttack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialize();
        heavyAttack = this.gameObject.GetComponent<HeavyAttack>();
        speacialAttack = this.gameObject.GetComponent<SpeacialAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        SkillActive();
    }
    public override void SkillActive()
    {
        if(NoAction())
        {
            heavyAttack.Attack();
            Debug.Log("Chay ne");
            speacialAttack.Attack();
        }
    }
}
