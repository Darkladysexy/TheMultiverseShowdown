using UnityEngine;
using UnityEngine.UI;

public class SetImageSc : MonoBehaviour
{
    public Image image1;
    public Image image2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image1.sprite = CharacterData.sprite1;
        image2.sprite = CharacterData.sprite2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
