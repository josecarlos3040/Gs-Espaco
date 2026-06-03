using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UIButtonsManager : MonoBehaviour
{
    [SerializeField] public float moneyReward;
    [Header("Components")]
    [SerializeField] PlayerFuel playerFuel;
    [SerializeField] PlayerMove playerMove;
    [SerializeField] PlayerUpgrades playerUpg;

    [SerializeField] GameObject startButton;
    [SerializeField] GameObject gameOverButton;
    [SerializeField] GameObject particleLaunch;

    [SerializeField] TextMeshProUGUI moneyText;

    [SerializeField] GameObject player;
    [SerializeField] InventoryUI inventoryUI;


    [SerializeField] GameObject cineCamera;
    [SerializeField] CinemachineSplineDolly dolly;


    void Start()
    {
        dolly.AutomaticDolly.Enabled = false;
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

        if(playerFuel.gameOver == true)
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
                fixedSpeed.Speed = playerMove.speed / 200;
            }
        }
    }
    public void StartGame()
    {
        playerFuel.inGame = true;
        dolly.AutomaticDolly.Enabled = true;
        particleLaunch.SetActive(true);
        playerMove.passedCloud = false;
    } 

    public void GameOver()
    {
        playerFuel.inGame = false;
        playerFuel.gameOver = false;

        player.transform.position = new Vector3(0, -20, 0);
        player.transform.rotation = Quaternion.identity;
        playerMove.rb.useGravity = true;
        playerMove.isOrbit = false;
        particleLaunch.SetActive(false);

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
}
