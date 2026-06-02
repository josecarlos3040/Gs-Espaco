using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class UIButtonsManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] PlayerFuel playerFuel;
    [SerializeField] PlayerMove playerMove;
    [SerializeField] PlayerUpgrades playerUpg;

    [SerializeField] GameObject startButton;
    [SerializeField] GameObject gameOverButton;

    [SerializeField] TextMeshProUGUI moneyText;

    [SerializeField] GameObject player;

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
    }
    public void StartGame()
    {
        playerFuel.inGame = true;
        dolly.AutomaticDolly.Enabled = true;
    } 

    public void GameOver()
    {
        playerFuel.inGame = false;
        playerFuel.gameOver = false;

        player.transform.position = new Vector3(0, -20, 0);
        player.transform.rotation = Quaternion.identity;
        playerMove.rb.useGravity = true;
        playerMove.isOrbit = false;

        playerMove.planet = null;
        playerMove.rb.linearVelocity = Vector3.zero;


        dolly.AutomaticDolly.Enabled = false;
        dolly.CameraPosition = 0f;

    }
}
