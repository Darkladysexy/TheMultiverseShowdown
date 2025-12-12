using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraZoom : MonoBehaviour
{
    private Transform player1;
    private Transform player2;

    [Header("Cài đặt")]
    public float minZoom = 5f;     // Mức zoom gần nhất (không cho zoom gần hơn)
    public float padding = 3f;     // Khoảng đệm xung quanh player
    public float zoomSmoothTime = 0.2f;     // Thời gian làm mượt zoom
    public Vector2 maxOffset = new Vector2(5f, 3f); // Camera không đi quá offset so với initialPosition (x,y)
    public float moveSmoothTime = 0.15f; // Thời gian làm mượt khi di chuyển camera

    private Camera cam;
    private float zoomVelocity = 0f;         // Biến tạm cho SmoothDamp
    private Vector3 moveVelocity = Vector3.zero; // Biến tạm cho SmoothDamp khi di chuyển
    public Vector3 initialPosition;         // Vị trí camera cố định
    [HideInInspector] public bool isShaking = false; // Ẩn biến này khỏi Inspector
    void Start()
    {
        cam = GetComponent<Camera>();
        // Lưu lại vị trí cố định ban đầu của camera
        initialPosition = transform.position;
        player1 = GameObject.FindGameObjectWithTag("P1").transform;
        player2 = GameObject.FindGameObjectWithTag("P2").transform;
    }

    void LateUpdate()
    {
        if (player1 == null || player2 == null || isShaking)
        {
            return;
        }
        // if (!isShaking)
        // {
        //     transform.position = initialPosition;
        // }
        // --- 1. DI CHUYỂN CAMERA THEO NHÂN VẬT ---
        // Tính midpoint giữa 2 player và dịch camera về hướng đó,
        // nhưng giới hạn không cho camera vượt quá một khoảng (maxOffset) so với initialPosition.
        Vector3 midpoint = (player1.position + player2.position) * 0.5f;
        Vector3 desiredPos = new Vector3(midpoint.x, midpoint.y, initialPosition.z);

        // Clamp theo giới hạn x,y so với initialPosition
        desiredPos.x = Mathf.Clamp(desiredPos.x, initialPosition.x - maxOffset.x, initialPosition.x + maxOffset.x);
        desiredPos.y = Mathf.Clamp(desiredPos.y, initialPosition.y - maxOffset.y, initialPosition.y + maxOffset.y);

        // Di chuyển mượt tới vị trí mong muốn
        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref moveVelocity, moveSmoothTime);

        // --- 2. XỬ LÝ ZOOM CAMERA (KHOẢNG CÁCH) ---
        
        // --- Tính khoảng cách từ TÂM CAMERA đến 2 player ---
        // Chúng ta cần tìm player NÀO ở xa tâm camera nhất
        
        // Lấy vị trí 2 player so với midpoint (ngang)
        float p1_distX = Mathf.Abs(player1.position.x - midpoint.x);
        float p2_distX = Mathf.Abs(player2.position.x - midpoint.x);

        // Lấy vị trí 2 player so với midpoint (dọc)
        float p1_distY = Mathf.Abs(player1.position.y - midpoint.y);
        float p2_distY = Mathf.Abs(player2.position.y - midpoint.y);

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