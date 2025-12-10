using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public static GameManager instant;
    public PlayableDirector introScene;
    public PlayableDirector endGameScene;
    void Awake()
    {
        instant = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        introScene.Play();
        endGameScene.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("P1").GetComponent<PlayerHealth>().currentHealth <=0 
            || GameObject.FindGameObjectWithTag("P2").GetComponent<PlayerHealth>().currentHealth <= 0)
        {
            endGameScene.Play();
        }
    }

    public void PauseGame(Vector3 position)
    {
        StartCoroutine(PauseGameEnumerator(position));
    }
    /// <summary>
    /// Dung scene tam thoi
    /// </summary>
    /// <param name="position"> vi tri dung cua camera </param>
    /// <returns></returns>
    private IEnumerator PauseGameEnumerator(Vector3 position)
    {
        Time.timeScale = 0f;
        CameraManager.instant.ChangePostionCamera(position);
        yield return new WaitForSecondsRealtime(0.3f);
        CameraManager.instant.ChangePostionCamera(new Vector3(0,0,-10));
        Time.timeScale = 1f;
    }
}
