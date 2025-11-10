using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    // Thời gian tồn tại của prefab này (tính bằng giây)
    public float lifetime = 2f; // Hãy đặt thời gian này khớp với thời lượng animation nổ

    void Start()
    {
        // Hủy GameObject này sau [lifetime] giây
        Destroy(gameObject, lifetime);
    }
}