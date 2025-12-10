using System;
using System.Linq;
using NUnit.Framework;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CharacterState : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject[] prefabs;
    public Sprite[] sprites;
    public AudioClip[] clips;
    private AudioSource source;
    public GameObject maps;
    public TextMeshProUGUI textMeshPro;
    public GameObject buttons;
    public Image image1;
    public Image image2;
    private bool IsPlayer1;
    void Start()
    {
        IsPlayer1 = true;
        buttons.SetActive(false);
        textMeshPro.text = "Player 1 choose your character";
        source = gameObject.GetComponent<AudioSource>();
    }
    void Update()
    {
       
    }
    public void getCharacter(int index)
    {
        source.Stop();
        source.PlayOneShot(clips[index]);
        if (IsPlayer1)
        {
            CharacterData.prefabs1 = prefabs[index];
            CharacterData.sprite1 = sprites[index];
            image1.sprite = sprites[index] ;
            image1.color = Color.white;
            Debug.Log(prefabs[index]);
        }
        else
        {
            CharacterData.prefabs2 = prefabs[index];
            CharacterData.sprite2 = sprites[index];
            image2.sprite = sprites[index] ;
            image2.color = Color.white;
            Debug.Log(prefabs[index]);
        }
        buttons.SetActive(true);
    }

    public bool changePlayerTurn()
    {
        return false;
    }

    public void confirmSelection()
    {
        if (IsPlayer1)
        {
            IsPlayer1 = changePlayerTurn();
            textMeshPro.text = "Player 2 choose your character";
        }
        else
        {
            textMeshPro.text = "Please choose your map";
            maps.SetActive(true);
        }
        buttons.SetActive(false);
    }
}
