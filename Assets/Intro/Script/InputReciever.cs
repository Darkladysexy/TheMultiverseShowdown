using UnityEngine;
using UnityEngine.InputSystem;

public class InputReciever : MonoBehaviour
{
    private GameObject p1;
    private GameObject p2;
    private void Start()
    {
        p1 = GameObject.FindGameObjectWithTag("P1");
        p2 = GameObject.FindGameObjectWithTag("P2");
    }
    public void StopAllInput()
    {
        p1.GetComponent<PlayerMovement>().isStun = true;
        p2.GetComponent<PlayerMovement>().isStun = true;
    }

    public void StartAllInput()
    {
        p1.GetComponent<PlayerMovement>().isStun = false;
        p2.GetComponent<PlayerMovement>().isStun = false;
    }
}
