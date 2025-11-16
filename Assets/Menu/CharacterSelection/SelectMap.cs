using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMap : MonoBehaviour
{
    public GameObject[] Maps;
    public GameObject Selections;
    private int selectedMap;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Selections.SetActive(false);

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            previousMap();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            nextMap();
        }
    }
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
        selectedMap -= 1;
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
