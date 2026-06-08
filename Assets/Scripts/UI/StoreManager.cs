using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] PlayerMove playerMove;
    [SerializeField] PlayerFuel playerFuel;
    [SerializeField] PlayerUpgrades playerUpgrades;

    [SerializeField] UIButtonsManager buttonsManager;

    [Header("Buttons")]
    [SerializeField] Button speedButton;
    [SerializeField] Button fuelButton;
    [SerializeField] Button scanButton;
    [SerializeField] Button rewardMultButton;
    [SerializeField] Button inventoryButton;

    public Button fuelMoonButton;
    public Button sellMoonButton;

    [Header("Buy Prices")]
    [SerializeField] float speedPrice;
    [SerializeField] float fuelPrice;
    [SerializeField] float scanSpeedPrice;
    [SerializeField] float rewardMultPrice;
    [SerializeField] float inventoryPrice;

    [SerializeField] float moonFuelPrice;
    [SerializeField] float moonSellPrice;

    [Header("Max Upgrades")]
    [SerializeField] float speedMax = 3;
    [SerializeField] float fuelMax = 3;
    [SerializeField] float scanSpeedMax = 4;
    [SerializeField] float rewardMultMax = 3;
    [SerializeField] float inventoryMax = 3;

    public bool moonFuelMax = false;
    public bool moonSellMax = false;

    [Header("Actual Upgrades")]
    [SerializeField] float speedActual = 1;
    [SerializeField] float fuelActual = 1;
    [SerializeField] float scanSpeedActual = 1;
    [SerializeField] float rewardMultActual = 1;
    [SerializeField] float inventoryActual = 1;
    void Update()
    {
        if(speedActual >= speedMax)
        {
            speedButton.interactable = false;
        }
        if(fuelActual >= fuelMax)
        {
            fuelButton.interactable = false;
        }
        if (scanSpeedActual >= scanSpeedMax)
        {
            scanButton.interactable = false;
        }
        if (rewardMultActual >= rewardMultMax)
        {
            rewardMultButton.interactable = false;
        }
        if (inventoryActual >= inventoryMax)
        {
            inventoryButton.interactable = false;
        }

        if (moonFuelMax)
        {
            fuelMoonButton.interactable = false;
        }
        if (moonSellMax)
        {
            sellMoonButton.interactable = false;
        }
    }
    public void Speed()
    {
        if (playerUpgrades.money >= speedPrice)
        {
            playerUpgrades.money -= speedPrice;
            speedActual++;
            playerMove.speed += 3;
        }
    }

    public void Fuel()
    {
        if (playerUpgrades.money >= fuelPrice)
        {
            playerUpgrades.money -= fuelPrice;
            fuelActual++;
            playerFuel.maxFuel += 10;
        }
    }

    public void Scan()
    {
        if (playerUpgrades.money >= scanSpeedPrice)
        {
            playerUpgrades.money -= scanSpeedPrice;
            scanSpeedActual++;
            playerUpgrades.scanerTime -= 0.1f;
        }
    }

    public void Reward()
    {
        if (playerUpgrades.money >= rewardMultPrice)
        {
            playerUpgrades.money -= rewardMultPrice;
            rewardMultActual++;
            buttonsManager.moneyReward += 5;
        }
    }

    public void Inventory()
    {
        if (playerUpgrades.money >= inventoryPrice)
        {
            playerUpgrades.money -= inventoryPrice;
            inventoryActual++;
            playerUpgrades.maxInventory += 2;
        }
    }

    // Moon Upgrades

    public void FuelMoon()
    {
        if (playerUpgrades.money >= moonFuelPrice)
        {
            playerUpgrades.money -= moonFuelPrice;
            moonFuelMax = true;
            sellMoonButton.interactable = false;
        }
    }
    public void SeelMoon()
    {
        if (playerUpgrades.money >= moonFuelPrice)
        {
            playerUpgrades.money -= moonFuelPrice;
            moonSellMax = true;
            fuelMoonButton.interactable = false;
        }
    }
}
