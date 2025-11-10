using UnityEngine;
public class KakashiUpNormalAttack : MonoBehaviour, InterfaceSkill
{
    public float coolDownTime { get; set; } = 2f;
    public int damage { get; set; } = 12;
    public KeyCode KeyCode { get; set; } // Không dùng
    public void Attack() { Debug.Log("Thực hiện chiêu: W + J (Up Normal)"); }
    public void StartSkill() { }
    public void EndSkill() { }
    public void CoolDown() { }
}