using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LoadCharacter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject player1 = CharacterData.prefabs1;
    private GameObject player2 = CharacterData.prefabs2;
    public Transform spawnPoint;
    public bool isSpawn1;

    void Start()
    {
       SpawnCharacters();

    }
    private void SpawnCharacters()
    {
        if (isSpawn1)
        {
            player1.tag = "P1";
            Instantiate(player1, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            player2.tag = "P2";
            Instantiate(player2, spawnPoint.position, Quaternion.identity);
        }
    }
   
}
