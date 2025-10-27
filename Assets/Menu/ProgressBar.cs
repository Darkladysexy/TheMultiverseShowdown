using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] public Slider slider;
    [SerializeField] public int level;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(loading(level));
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator loading(int level)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(level);
        while (!asyncOperation.isDone)
        {
            slider.value = asyncOperation.progress;
            yield return null;
        }
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
