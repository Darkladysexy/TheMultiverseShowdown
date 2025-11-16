using UnityEngine;
using UnityEngine.InputSystem;

public class InputReciever : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public void StopAllInput()
    {
        playerMovement.isStun = true;
    }

    public void StartAllInput()
    {
        playerMovement.isStun = false;
    }
}
