using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraZoom : MonoBehaviour
{
    public Transform player1;
    public Transform player2;

    [Header("Cài đặt")]
    public float minZoom = 5f;     // Mức zoom gần nhất (không cho zoom gần hơn)
    public float padding = 3f;     // Khoảng đệm xung quanh player
    public float zoomSmoothTime = 0.2f;     // Thời gian làm mượt zoom

    private Camera cam;
    private float zoomVelocity = 0f;         // Biến tạm cho SmoothDamp
    private Vector3 initialPosition;         // Vị trí camera cố định

    void Start()
    {
        cam = GetComponent<Camera>();
        // Lưu lại vị trí cố định ban đầu của camera
        initialPosition = transform.position; 
    }

    void LateUpdate()
    {
        if (player1 == null || player2 == null)
        {
            return;
        }

        // --- 1. GIỮ VỊ TRÍ CAMERA CỐ ĐỊNH ---
        // Đảm bảo camera không bao giờ di chuyển
        transform.position = initialPosition;

        // --- 2. XỬ LÝ ZOOM CAMERA (KHOẢNG CÁCH) ---
        
        // --- Tính khoảng cách từ TÂM CAMERA đến 2 player ---
        // Chúng ta cần tìm player NÀO ở xa tâm camera nhất
        
        // Lấy vị trí 2 player so với tâm camera (ngang)
        float p1_distX = Mathf.Abs(player1.position.x - initialPosition.x);
        float p2_distX = Mathf.Abs(player2.position.x - initialPosition.x);
        
        // Lấy vị trí 2 player so với tâm camera (dọc)
        float p1_distY = Mathf.Abs(player1.position.y - initialPosition.y);
        float p2_distY = Mathf.Abs(player2.position.y - initialPosition.y);

        // Khoảng cách ngang xa nhất cần zoom
        float distanceX = Mathf.Max(p1_distX, p2_distX) * 2 + padding;
        
        // Khoảng cách dọc xa nhất cần zoom
        float distanceY = Mathf.Max(p1_distY, p2_distY) * 2 + padding;

        // Tính kích thước camera cần thiết (giống như trước)
        float requiredSizeX = distanceX / (2 * cam.aspect);
        float requiredSizeY = distanceY / 2;
        
        float targetSize = Mathf.Max(requiredSizeX, requiredSizeY);
        targetSize = Mathf.Max(targetSize, minZoom);

        // Zoom camera một cách mượt mà
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, targetSize, ref zoomVelocity, zoomSmoothTime);
    }
}