using UnityEngine;

public class KakashiDebugger : MonoBehaviour
{
    private int frameCount = 0;
    
    void Update()
    {
        frameCount++;
        
        // Log position mỗi giây
        if (frameCount % 60 == 0)
        {
            Debug.Log($"[KAKASHI] Frame {frameCount}: Position: {transform.position}, Active: {gameObject.activeSelf}, Health: {GetComponent<PlayerHealth>()?.health}");
        }
        
        // Cảnh báo nếu rơi quá thấp
        if (transform.position.y < -10)
        {
            Debug.LogError($"[KAKASHI] RƠI QUÁ THẤP! Y = {transform.position.y}");
        }
    }
    
    void OnDestroy()
    {
        Debug.LogError("====== KAKASHI BỊ DESTROY! ======");
        Debug.LogError($"Frame: {frameCount}");
        Debug.LogError($"Position khi destroy: {transform.position}");
        
        // In toàn bộ call stack
        System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace(true);
        Debug.LogError($"Full Stack Trace:\n{stackTrace}");
    }
    
    void OnDisable()
    {
        Debug.LogWarning($"====== KAKASHI BỊ DISABLE! Frame: {frameCount} ======");
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"[KAKASHI] Collision ENTER với: {collision.gameObject.name}, Tag: {collision.gameObject.tag}, Layer: {LayerMask.LayerToName(collision.gameObject.layer)}");
        
        // Kiểm tra object va chạm có script destroy không
        MonoBehaviour[] scripts = collision.gameObject.GetComponents<MonoBehaviour>();
        foreach (var script in scripts)
        {
            Debug.Log($"  - Script trên {collision.gameObject.name}: {script.GetType().Name}");
        }
    }
    
    void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log($"[KAKASHI] Collision EXIT với: {collision.gameObject.name}");
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"[KAKASHI] Trigger ENTER với: {collision.gameObject.name}, Tag: {collision.gameObject.tag}");
    }
}
