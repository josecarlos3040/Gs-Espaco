using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] PlayerMove playerMove;
    [SerializeField] PlayerFuel playerFuel;
    [SerializeField] UIButtonsManager buttonsManager;
    [SerializeField] Planet planet;
    [SerializeField] InventoryUI inventoryUI;

    [Header("Upgrades(Tem em outros codigos tbm)")]
    public float money;
    public float rewardMultiplier = 1f;
    public float scanerTime = 1f;

    [Header("Inventory")]
    public List<InventoryItem> inventory = new List<InventoryItem>();
    public int maxInventory;

    public void InteractWithPlanet(Planet planet)
    {
        AddItem(planet.GetItem());

    }
    public void ReceivePlanetItem(Planet planet)
    {
        AddItem(planet.GetItem());
    }

    public void AddItem(InventoryItem item)
    {
        if (inventory == null)
        {
            inventory = new List<InventoryItem>();
        }

        if (inventory.Count >= maxInventory)
        {
            Debug.Log("Inventário cheio!");
            return;
        }

        inventory.Add(item);

        if (inventoryUI != null)
            inventoryUI.Refresh();
        Debug.Log("ITEM ADICIONADO: " + inventory[inventory.Count - 1].itemName);
    }



}
