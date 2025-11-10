using UnityEngine;
public class KakashiDownHeavyAttack : MonoBehaviour, InterfaceSkill
{
    public float coolDownTime { get; set; } = 10f;
    public int damage { get; set; } = 50;
    public KeyCode KeyCode { get; set; } // Không dùng
    public void Attack() { Debug.Log("Thực hiện chiêu: S + I (Chiêu đặc biệt)"); }
    public void StartSkill() { }
    public void EndSkill() { }
    public void CoolDown() { }
}