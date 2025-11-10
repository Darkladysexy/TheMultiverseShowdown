using UnityEngine;

public interface ITakeDamage
{
    // THÊM "bool isHeavyHit" vào cuối
    public void TakeDamage(int damage, float force, Vector3 dirForce, bool isHeavyHit); // Nhan sat thuong khi bi tan cong trung
}