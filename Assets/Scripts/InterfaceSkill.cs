using UnityEngine;

public interface InterfaceSkill
{
    public float coolDownTime { get; set; } // thoi gian cool down
    public int damage { get; set; } // sat thuong gay ra cua skill
    public KeyCode KeyCode { get; set; } // Phim de thuc hien skill
    public void StartSkill();
    public void EndSkill();
    public void CoolDown();
    public void Attack();

}
