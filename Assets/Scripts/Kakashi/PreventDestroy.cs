using UnityEngine;

public class PreventDestroy : MonoBehaviour
{
    void OnDestroy()
    {
        Debug.LogError("====== AI ĐÓ ĐANG CỐ DESTROY KAKASHI! ======");
        Debug.LogError($"Stack Trace:\n{System.Environment.StackTrace}");
    }
}
