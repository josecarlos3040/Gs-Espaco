using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using static Unity.Cinemachine.SplineAutoDolly;
using static UnityEngine.Rendering.DebugUI;

public class UIButtonsManager : MonoBehaviour
{
    [SerializeField] public float moneyReward;
    [Header("Components")]
    [SerializeField] BarrerShip barrerShip;
    [SerializeField] BarrerCamera barrerCamera;
    [SerializeField] Material earthSkybox;

    [SerializeField] PlayerFuel playerFuel;
    [SerializeField] PlayerMove playerMove;
    [SerializeField] PlayerUpgrades playerUpg;

    [SerializeField] GameObject startButton;
    [SerializeField] GameObject gameOverButton;
    [SerializeField] GameObject particleLaunch;

    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] GameObject scanner;
    [SerializeField] CometSpawner cometSpawner;
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

    [SerializeField] GameObject cineCamera;
    [SerializeField] CinemachineSplineDolly dolly;


    void Start()
    {
        dolly.AutomaticDolly.Enabled = false;

        scanner.SetActive(false);

        store.SetActive(false);
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

        if (playerMove.scannedMoon)
        {
            changeButton.SetActive(true);
            backButton.SetActive(true);

        }
        else
        {
            changeButton.SetActive(false);
            backButton.SetActive(false);
        }
        if(storeWeb == 1)
        {
            backButton.SetActive(false);
        }
        else if(storeWeb == 3)
        {
            changeButton.SetActive(false);
        }
    }
    public void StartGame()
    {
        playerFuel.inGame = true;
        dolly.AutomaticDolly.Enabled = true;
        particleLaunch.SetActive(true);
        playerMove.passedCloud = false;

        scanner.SetActive(true);
        sellButton.SetActive(false);

        storeButton.SetActive(false);
        store.SetActive(false);

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

        var auto = dolly.AutomaticDolly;
        player.transform.position = new Vector3(-5.0314002f, 10.1700001f, 135.429993f);
        player.transform.rotation = Quaternion.identity;

        cometSpawner.DestroyAllComets();

        barrerShip.propulsor.SetActive(true);
        barrerShip.trail.SetActive(false);

        barrerCamera.cameraSpace.SetActive(false);
        barrerCamera.cameraDolly.SetActive(true);

        storeButton.SetActive(true);

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
        }

        else if (storeWeb == 2)
        {
            storeWeb++;

            shipUpgrades.SetActive(false);
            moonUpgrades.SetActive(false);

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
        }
        else if(storeWeb == 3)
        {
            storeWeb--;

            shipUpgrades.SetActive(false);
            moonUpgrades.SetActive(true);
        }
    }
}
