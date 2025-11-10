using UnityEngine;
public class KakashiUpHeavyAttack : MonoBehaviour, InterfaceSkill
{
    public float coolDownTime { get; set; } = 7f;
    public int damage { get; set; } = 30;
    public KeyCode KeyCode { get; set; } // Không dùng
    public void Attack() { Debug.Log("Thực hiện chiêu: W + I (Bắn rồng)"); }
    public void StartSkill() { }
    public void EndSkill() { }
    public void CoolDown() { }
}