using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instant;
    void Awake()
    {
        instant = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PauseGame(Vector3 position)
    {
        StartCoroutine(PauseGameEnumerator(position));
    }
    
    private IEnumerator PauseGameEnumerator(Vector3 position)
    {
        Time.timeScale = 0f;
        CameraManager.instant.ChangePostionCamera(position);
        yield return new WaitForSecondsRealtime(0.3f);
        CameraManager.instant.ChangePostionCamera(new Vector3(0,0,-10));
        Time.timeScale = 1f;
    }
}
