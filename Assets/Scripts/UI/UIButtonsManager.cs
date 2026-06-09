using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using static Unity.Cinemachine.SplineAutoDolly;
using static UnityEngine.Rendering.DebugUI;

public class UIButtonsManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] public float moneyReward;
    public int qtdDays = 0;

    [Header("Components")]
    [SerializeField] TextMeshProUGUI textMeshDay;
    [SerializeField] TextMeshProUGUI textInventory;

    [SerializeField] public TextMeshProUGUI goBackText;
    [SerializeField] public TextMeshProUGUI goToMars;
    [SerializeField] public TextMeshProUGUI finishGame;

    [SerializeField] BarrerShip barrerShip;
    [SerializeField] BarrerCamera barrerCamera;
    [SerializeField] Material earthSkybox;

    [SerializeField] PlayerFuel playerFuel;
    [SerializeField] PlayerMove playerMove;
    [SerializeField] PlayerUpgrades playerUpg;
    [SerializeField] StoreManager storeManager;

    [SerializeField] GameObject startButton;
    [SerializeField] GameObject gameOverButton;
    [SerializeField] GameObject particleLaunch;

    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] public GameObject scanner;
    [SerializeField] CometSpawner cometSpawner;
    [SerializeField] SpaceCamera spaceCamera;
    public GameObject sellButton;
    [SerializeField] int storeWeb;
    [SerializeField] GameObject changeButton;
    [SerializeField] GameObject backButton;

    [SerializeField] GameObject player;
    [SerializeField] InventoryUI inventoryUI;

    [SerializeField] GameObject store;
    [SerializeField] GameObject storeButton;

    [SerializeField] GameObject shipUpgrades;
    [SerializeField] GameObject moonUpgrades;
    [SerializeField] GameObject marsUpgrades;

    [SerializeField] public GameObject statsPanel;
    [SerializeField] public GameObject inventoryPanel;
    [SerializeField] public GameObject quests;
    [SerializeField] public GameObject startFrame;

    [SerializeField] GameObject cineCamera;
    [SerializeField] CinemachineSplineDolly dolly;


    [SerializeField] TextMeshProUGUI statsSpeedText;
    [SerializeField] TextMeshProUGUI statsScanerText;
    [SerializeField] TextMeshProUGUI statsMoneyText;
    [SerializeField] TextMeshProUGUI statsFuelText;

    [SerializeField] Texture2D handCursor;

    [SerializeField] GameObject spawnCometsToMars;
    [SerializeField] GameObject spawnCometsAfterMars;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    void Start()
    {
        dolly.AutomaticDolly.Enabled = false;

        scanner.SetActive(false);

        store.SetActive(false);

        changeButton.SetActive(false);
        backButton.SetActive(false);
    }

    void Update()
    {
        moneyText.text = "Money: " + playerUpg.money.ToString("F2");

        if (playerFuel.inGame == false)
        {
            startButton.SetActive(true);

            playerFuel.fuelSlider.maxValue = playerFuel.maxFuel;
            playerFuel.fuelSlider.value = playerFuel.maxFuel;
        }
        else
        {
            startButton.SetActive(false);
        }

        if (playerFuel.gameOver == true)
        {
            gameOverButton.SetActive(true);
        }
        else
        {
            gameOverButton.SetActive(false);
        }

        if (playerMove.passedCloud)
        {
            var auto = dolly.AutomaticDolly;

            if (auto.Method is SplineAutoDolly.FixedSpeed fixedSpeed)
            {
                fixedSpeed.Speed = playerMove.speed / 170;
            }
        }

        if (playerMove.scannedMoon == true && storeWeb == 1)
        {
            changeButton.SetActive(true);
        }

        if(playerMove.scannedMars == false && storeWeb == 2)
        {
            changeButton.SetActive(false);
        }


        textInventory.text = "Storage: " + playerUpg.inventory.Count + "/"+ playerUpg.maxInventory;

        
        statsSpeedText.text = "Speed: " + playerMove.speed.ToString();
        statsScanerText.text = "Scaner: " + storeManager.scanSpeedActual.ToString();
        statsMoneyText.text = "Money Mult: " + moneyReward.ToString();
        statsFuelText.text = "Max Fuel: " + playerFuel.maxFuel.ToString();


        if(playerMove.fuelMoonComplete && playerMove.sellMoonComplete)
        {
            spawnCometsToMars.SetActive(true);
        }
        if(playerMove.fuelMarsComplete && playerMove.fuelMarsComplete)
        {
            spawnCometsAfterMars.SetActive(true);
        }

    }
    public void StartGame()
    {
        qtdDays++;
        textMeshDay.text = "Day: " + qtdDays.ToString();
        playerFuel.inGame = true;
        dolly.AutomaticDolly.Enabled = true;
        particleLaunch.SetActive(true);
        playerMove.passedCloud = false;

        sellButton.SetActive(false);

        storeButton.SetActive(false);
        store.SetActive(false);
        quests.SetActive(false);

        var auto = dolly.AutomaticDolly;

        cometSpawner.SpawnComets();

        if (auto.Method is SplineAutoDolly.FixedSpeed fixedSpeed)
        {
            fixedSpeed.Speed = 0.003f;
        }
    } 

    public void GameOver()
    {
        playerFuel.inGame = false;
        playerFuel.gameOver = false;
        spaceCamera.MoveToInitialPosition();

        startFrame.SetActive(true);

        var auto = dolly.AutomaticDolly;
        player.transform.position = new Vector3(-5.0314002f, 10.1700001f, 135.429993f);
        player.transform.rotation = Quaternion.identity;

        cometSpawner.DestroyAllComets();

        barrerShip.propulsor.SetActive(true);
        barrerShip.trail.SetActive(false);

        statsPanel.SetActive(true);
        inventoryPanel.SetActive(true);

        goBackText.gameObject.SetActive(false);
        
        barrerCamera.cameraSpace.SetActive(false);
        barrerCamera.cameraDolly.SetActive(true);

        storeButton.SetActive(true);
        quests.SetActive(true);

        playerMove.rb.useGravity = true;
        playerMove.isOrbit = false;
        particleLaunch.SetActive(false);
        scanner.SetActive(false);
        sellButton.SetActive(true);
        RenderSettings.skybox = earthSkybox;
        
        
        //DynamicGI.UpdateEnvironment();

        playerMove.planet = null;
        playerMove.rb.linearVelocity = Vector3.zero;


        dolly.AutomaticDolly.Enabled = false;
        dolly.CameraPosition = 0f;

    }

    public void ClearInventory()
    {
        Debug.Log("Limpando inventário...");

        foreach (var item in playerUpg.inventory)
        {
            if (item.itemName  == "1")
            {
                playerUpg.money += 1 * moneyReward;
            }
            if (item.itemName == "2")
            {
                playerUpg.money += 2 * moneyReward;
            }

            Debug.Log("Removendo item: " + item.itemName);
        }

        playerUpg.inventory.Clear();

        if (inventoryUI != null)
            inventoryUI.Refresh();
    }

    public void OpenStore()
    {
        storeButton.SetActive(false);
        store.SetActive(true);
        storeWeb = 1;
    }

    public void CloseStore()
    {
        storeButton.SetActive(true);
        store.SetActive(false);
    }

    public void ChangeWeb()
    {
        if (storeWeb == 1)
        {
            storeWeb++;

            shipUpgrades.SetActive(false);
            moonUpgrades.SetActive(true);

            backButton.SetActive(true);
        }

        else if (storeWeb == 2)
        {
            storeWeb++;

            shipUpgrades.SetActive(false);
            moonUpgrades.SetActive(false);
            marsUpgrades.SetActive(true);


            changeButton.SetActive(false);
        }
    }

    public void BackWeb()
    {
        if (storeWeb == 2)
        {
            storeWeb--;

            shipUpgrades.SetActive(true);
            moonUpgrades.SetActive(false);

            backButton.SetActive(false);
        }
        else if(storeWeb == 3)
        {
            storeWeb--;

            shipUpgrades.SetActive(false);
            moonUpgrades.SetActive(true);
            marsUpgrades.SetActive(false);
            changeButton.SetActive(true);
        }
    }
}
