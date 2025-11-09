using UnityEngine;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    private Slider slider;
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
    }
    public void getVolume()
    {
        StaticData.vol = slider.value;
    }
}
