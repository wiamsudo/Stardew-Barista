using UnityEngine;
using UnityEngine.InputSystem;

public class Station : MonoBehaviour
{
    public Item item;

    Inventory playerNearby;

    void Update()
    {
        if (playerNearby == null) return;

        if (Keyboard.current.qKey.wasPressedThisFrame || ArduinoInput.Click)
            playerNearby.Add(item);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Inventory inventory = other.GetComponent<Inventory>();

        if (inventory != null)
            playerNearby = inventory;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Inventory>() != null)
            playerNearby = null;
    }
}