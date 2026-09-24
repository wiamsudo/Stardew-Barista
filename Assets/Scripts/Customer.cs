using UnityEngine;
using UnityEngine.InputSystem;

public class Customer : MonoBehaviour
{
    public Item[] products;
    public SpriteRenderer orderIcon;

    Item order;
    Inventory playerNearby;
    int served;

    void Start()
    {
        NewOrder();
    }

    void Update()
    {
        if (playerNearby == null) return;

        if (Keyboard.current.qKey.wasPressedThisFrame || ArduinoInput.Click)
        {
            if (playerNearby.Remove(order))
            {
                served++;
                Debug.Log("Customers served: " + served);
                NewOrder();
            }
        }
    }

    void NewOrder()
    {
        order = products[Random.Range(0, products.Length)];
        orderIcon.sprite = order.icon;
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
