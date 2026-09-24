using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image[] slots;
    public Image[] icons;

    List<Item> items = new List<Item>();
    int selected;

    void Start()
    {
        Debug.Log(name + " Slots: " + slots.Length + ", Icons: " + icons.Length);
        UpdateSlots();
    }

    void Update()
    {
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame || ArduinoInput.Left)
            SelectPrevious();

        if (Keyboard.current.rightArrowKey.wasPressedThisFrame || ArduinoInput.Right)
            SelectNext();

        foreach (Image icon in icons)
            icon.transform.localScale = Vector3.MoveTowards(icon.transform.localScale, Vector3.one, Time.deltaTime * 3);
    }

    public void Add(Item item)
    {
        if (items.Count == slots.Length) return;

        items.Add(item);
        int newSlot = items.Count - 1;
        icons[newSlot].transform.localScale = Vector3.one * 1.5f;
        UpdateSlots();
    }

    public bool Remove(Item item)
    {
        bool removed = items.Remove(item);
        UpdateSlots();
        return removed;
    }

    void SelectNext()
    {
        selected++;
        if (selected >= slots.Length) selected = 0;
        UpdateSlots();
    }

    void SelectPrevious()
    {
        selected--;
        if (selected < 0) selected = slots.Length - 1;
        UpdateSlots();
    }

    void UpdateSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Count)
            {
                icons[i].sprite = items[i].icon;
                icons[i].enabled = true;
            }
            else
            {
                icons[i].enabled = false;
            }

            if (i == selected)
                slots[i].color = Color.yellow;
            else
                slots[i].color = Color.white;
        }
    }
}