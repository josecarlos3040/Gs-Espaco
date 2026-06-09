using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
    [SerializeField] Button colonyMars;



    public Button fuelMoonButton;
    public Button sellMoonButton;

    public Button fuelMarsButton;
    public Button sellMarsButton;

    [Header("Buy Prices")]
    [SerializeField] float speedPrice;
    [SerializeField] float fuelPrice;
    [SerializeField] float scanSpeedPrice;
    [SerializeField] float rewardMultPrice;
    [SerializeField] float inventoryPrice;

    [SerializeField] float moonFuelPrice;
    [SerializeField] float moonSellPrice;

    [SerializeField] float marsFuelPrice;
    [SerializeField] float marsSellPrice;
    [SerializeField] float marsColonyPrice;

    [SerializeField] TextMeshProUGUI speedPriceText;
    [SerializeField] TextMeshProUGUI fuelPriceText;
    [SerializeField] TextMeshProUGUI scanSpeedText;
    [SerializeField] TextMeshProUGUI rewardMultText;
    [SerializeField] TextMeshProUGUI inventoryText;

    [SerializeField] TextMeshProUGUI moonFuelText;
    [SerializeField] TextMeshProUGUI moonSellText;

    [SerializeField] TextMeshProUGUI marsFuelText;
    [SerializeField] TextMeshProUGUI marsSellText;
    [SerializeField] TextMeshProUGUI marsColonyText;


    [Header("Max Upgrades")]
    [SerializeField] float speedMax = 3;
    [SerializeField] float fuelMax = 3;
    [SerializeField] float scanSpeedMax = 4;
    [SerializeField] float rewardMultMax = 3;
    [SerializeField] float inventoryMax = 3;

    public bool moonFuelMax = false;
    public bool moonSellMax = false;

    public bool marsFuelMax = false;
    public bool marsSellMax = false;
    public bool marsColonyMax = false;

    [Header("Actual Upgrades")]
    [SerializeField] float speedActual = 1;
    [SerializeField] float fuelActual = 1;
    [SerializeField] public float scanSpeedActual = 1;
    [SerializeField] float rewardMultActual = 1;
    [SerializeField] float inventoryActual = 1;

    [SerializeField] Texture2D handCursor;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void Start()
    {
        colonyMars.gameObject.SetActive(false);
    }
    void Update()
    {
        speedPriceText.text = "$"+speedPrice.ToString("F2");
        fuelPriceText.text = "$" + fuelPrice.ToString("F2");
        scanSpeedText.text = "$" + scanSpeedPrice.ToString("F2");
        rewardMultText.text = "$" + rewardMultPrice.ToString("F2");
        inventoryText.text = "$" + inventoryPrice.ToString("F2");
        moonFuelText.text = "$" + moonFuelPrice.ToString("F2");
        moonSellText.text = "$" + moonSellPrice.ToString("F2");
        marsFuelText.text = "$" + marsFuelPrice.ToString("F2");
        marsSellText.text = "$" + marsSellPrice.ToString("F2");
        marsColonyText.text = "$" + marsColonyPrice.ToString("F2");




        if (speedActual >= speedMax)
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
        if (playerMove.fuelMoonComplete)
        {
            fuelMoonButton.interactable = false;
        }
        if (playerMove.sellMoonComplete)
        {
            sellMoonButton.interactable = false;
        }

        if (marsFuelMax)
        {
            fuelMarsButton.interactable = false;
        }
        if (marsSellMax)
        {
            sellMarsButton.interactable = false;
        }
        if (playerMove.fuelMarsComplete)
        {
            fuelMarsButton.interactable = false;
        }
        if (playerMove.sellMarsComplete)
        {
            sellMarsButton.interactable = false;
        }

        if(playerMove.fuelMarsComplete && playerMove.sellMarsComplete)
        {
            colonyMars.gameObject.SetActive(true);
        }
    }
    public void Speed()
    {
        if (playerUpgrades.money >= speedPrice)
        {
            playerUpgrades.money -= speedPrice;
            speedActual++;
            playerMove.speed += 3;
            SoundManager.Instance.PlaySFX(SoundManager.Instance.click);
        }
    }

    public void Fuel()
    {
        if (playerUpgrades.money >= fuelPrice)
        {
            playerUpgrades.money -= fuelPrice;
            fuelActual++;
            playerFuel.maxFuel += 10;
            SoundManager.Instance.PlaySFX(SoundManager.Instance.click);
        }
    }

    public void Scan()
    {
        if (playerUpgrades.money >= scanSpeedPrice)
        {
            playerUpgrades.money -= scanSpeedPrice;
            scanSpeedActual++;
            playerUpgrades.scanerTime -= 0.1f;
            SoundManager.Instance.PlaySFX(SoundManager.Instance.click);
        }
    }

    public void Reward()
    {
        if (playerUpgrades.money >= rewardMultPrice)
        {
            playerUpgrades.money -= rewardMultPrice;
            rewardMultActual++;
            buttonsManager.moneyReward += 5;
            SoundManager.Instance.PlaySFX(SoundManager.Instance.click);
        }
    }

    public void Inventory()
    {
        if (playerUpgrades.money >= inventoryPrice)
        {
            playerUpgrades.money -= inventoryPrice;
            inventoryActual++;
            playerUpgrades.maxInventory += 4;
            SoundManager.Instance.PlaySFX(SoundManager.Instance.click);
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
            SoundManager.Instance.PlaySFX(SoundManager.Instance.click);
        }
    }
    public void SeelMoon()
    {
        if (playerUpgrades.money >= moonFuelPrice)
        {
            playerUpgrades.money -= moonFuelPrice;
            moonSellMax = true;
            fuelMoonButton.interactable = false;
            SoundManager.Instance.PlaySFX(SoundManager.Instance.click);
        }
    }

    public void FuelMars()
    {
        if (playerUpgrades.money >= marsFuelPrice)
        {
            playerUpgrades.money -= marsFuelPrice;
            marsFuelMax = true;
            sellMarsButton.interactable = false;
            SoundManager.Instance.PlaySFX(SoundManager.Instance.click);
        }
    }
    public void SeelMars()
    {
        if (playerUpgrades.money >= marsFuelPrice)
        {
            playerUpgrades.money -= marsFuelPrice;
            marsSellMax = true;
            fuelMarsButton.interactable = false;
            SoundManager.Instance.PlaySFX(SoundManager.Instance.click);
        }
    }

    public void ColonyMars()
    {
        if (playerUpgrades.money >= marsColonyPrice)
        {
            playerUpgrades.money -= marsColonyPrice;
            marsColonyMax = true;
            colonyMars.interactable = false;
            SoundManager.Instance.PlaySFX(SoundManager.Instance.click);
        }
    }
}
