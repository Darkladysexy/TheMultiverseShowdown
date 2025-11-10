using UnityEngine;
public class KakashiUpLightAttack : MonoBehaviour, InterfaceSkill
{
    public float coolDownTime { get; set; } = 6f;
    public int damage { get; set; } = 25;
    public KeyCode KeyCode { get; set; } // Không dùng
    public void Attack() { Debug.Log("Thực hiện chiêu: W + U (Dịch chuyển)"); }
    public void StartSkill() { }
    public void EndSkill() { }
    public void CoolDown() { }
}