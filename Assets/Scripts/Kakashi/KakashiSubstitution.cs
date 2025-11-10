using UnityEngine;
// nút o dùng khi đang bị dính combo ko thoát ra được, khi ấn sẽ biến ra 1 khúc cây thay thế cho player và player sẽ dịch chuyển ra sau
public class KakashiSubstitution : MonoBehaviour
{
    // TODO: Thêm Cooldown
    public float coolDown = 15f;
    private float lastSubTime = -15f;

    public void AttemptSubstitution()
    {
        // Kiểm tra cooldown
        if (Time.time < lastSubTime + coolDown)
        {
            Debug.Log("Thế thân đang hồi chiêu!");
            return;
        }

        // TODO: Kiểm tra xem có đang bị dính đòn (stun) không
        // PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        // if (!playerMovement.isStun)
        // {
        //     Debug.Log("Chỉ có thể thế thân khi đang bị đánh!");
        //     return;
        // }

        // Nếu vượt qua kiểm tra
        lastSubTime = Time.time;
        Debug.Log("Thực hiện chiêu: O (Thế thân)!");
        
        // TODO:
        // 1. Tạo hiệu ứng khúc cây tại vị trí hiện tại
        // 2. Dịch chuyển player ra sau lưng đối thủ
    }
}