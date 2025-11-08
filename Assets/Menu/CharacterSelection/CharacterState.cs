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
    public GameObject maps;
    public TextMeshProUGUI textMeshPro;
    public GameObject buttons;
    private bool IsPlayer1;
    
    void Start()
    {
        IsPlayer1 = true;
        buttons.SetActive(false);
        textMeshPro.text = "Player 1 choose your character";
    }
    public void getCharacter(int index)
    {
        if (IsPlayer1)
        {
            CharacterData.prefabs1 = prefabs[index];
            Debug.Log(prefabs[index]);
        }
        else
        {
            CharacterData.prefabs2 = prefabs[index];
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
            maps.SetActive(true);
        }
        buttons.SetActive(false);
    }
}
