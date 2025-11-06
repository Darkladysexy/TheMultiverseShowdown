using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMap : MonoBehaviour
{
    public GameObject[] Maps;
    private int selectedMap;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void nextMap()
    {
        Maps[selectedMap].SetActive(false);
        selectedMap += 1;
        if (selectedMap > Maps.Length - 1)
        {
            selectedMap = 0;
        }
        Maps[selectedMap].SetActive(true);

    }
    public void previousMap()
    {
        Maps[selectedMap].SetActive(false);
        selectedMap += 1;
        if (selectedMap < 0)
        {
            selectedMap = Maps.Length - 1;
        }
        Maps[selectedMap].SetActive(true);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Map" + (selectedMap+1) );
    }
}
