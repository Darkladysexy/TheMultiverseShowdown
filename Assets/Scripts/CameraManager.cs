using UnityEngine;
using System.Collections; // Cần thêm thư viện này để dùng Coroutine

public class CameraManager : MonoBehaviour
{
    public static CameraManager instant;
    public GameObject myCamera;
    public Transform transformCamera;

    private Vector3 originalPosition; // Dùng để lưu vị trí camera ban đầu
    private Vector3 originalScale;

    void Awake()
    {
        instant = this;
    }

    void Start()
    {
        transformCamera = myCamera.transform;
        // Lưu lại vị trí gốc của camera khi bắt đầu game
        // originalPosition = transformCamera.position;
    }
    void Update()
    {
        
    }

    // Hàm này của bạn dùng để di chuyển camera đến 1 vị trí cố định MỚI
    public void ChangePostionCamera(Vector3 position)
    {
        myCamera.transform.position = position;
        // originalPosition = position; // Cập nhật lại vị trí gốc mới
    }

    // --- CODE MỚI ĐỂ RUNG CAMERA ---

    /// <summary>
    /// Bắt đầu rung camera
    /// </summary>
    /// <param name="duration">Rung trong bao lâu (ví dụ: 0.15f)</param>
    /// <param name="magnitude">Rung mạnh hay yếu (ví dụ: 0.2f)</param>
    public void StartShake(float duration, float magnitude, Transform transform)
    {
        // Bắt đầu Coroutine Shake
        StartCoroutine(Shake(duration, magnitude,transform));
    }

    private IEnumerator Shake(float duration, float magnitude, Transform transform)
    {
        float elapsed = 0.0f;

        // Lưu lại vị trí camera ngay TRƯỚC khi rung
        // (Có thể camera đang di chuyển theo player, nên ta lấy vị trí hiện tại)
        Vector3 startPosition = transform.position;
        transformCamera.position = new Vector3(transform.position.x, transform.position.y, -10);

        while (elapsed < duration)
        {
            // Tạo ra một vị trí rung ngẫu nhiên
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            // Áp dụng vị trí rung (chỉ rung X, Y)
            transformCamera.position = new Vector3(startPosition.x + x, startPosition.y + y, -10);

            elapsed += Time.deltaTime;

            yield return null; // Chờ đến frame tiếp theo
        }

        // Hết thời gian rung, trả camera về vị trí ban đầu
        transformCamera.position = new Vector3(0, 0, -10);
    }
}