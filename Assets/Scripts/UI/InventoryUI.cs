using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public PlayerUpgrades player;
    public GameObject slotPrefab;
    public Transform slotParent;

    private List<GameObject> slots = new List<GameObject>();

    public void Refresh()
    {
        // limpa UI antiga
        foreach (var s in slots)
        {
            Destroy(s);
        }
        slots.Clear();

        // recria slots
        foreach (var item in player.inventory)
        {
            InventorySlot slot = Instantiate(slotPrefab, slotParent)
                .GetComponent<InventorySlot>();

            slot.Set(item.sprite);

            slots.Add(slot.gameObject);
        }
    }
}

