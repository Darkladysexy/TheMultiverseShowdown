using UnityEngine;

public interface InterfaceSkill
{
    public float coolDownTime { get; set; }
    public int damage { get; set; }
    public KeyCode keyCode { get; set; }
    public void StartSkill();
    public void EndSkill();
    public void CoolDown();
    public void ExecuteSkill();
}
