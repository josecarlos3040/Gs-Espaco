using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIButtonsManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] PlayerFuel playerFuel;
    [SerializeField] PlayerMove playerMove;

    [SerializeField] GameObject startButton;
    [SerializeField] GameObject gameOverButton;

    [SerializeField] GameObject player;

    [SerializeField] GameObject cineCamera;
    [SerializeField] CinemachineSplineDolly dolly;


    void Start()
    {
        dolly.AutomaticDolly.Enabled = false;
    }
    public void StartGame()
    {
        playerFuel.fuelSlider.maxValue = playerFuel.maxFuel;
        playerFuel.fuelSlider.value = playerFuel.maxFuel;

        playerFuel.inGame = true;

        startButton.SetActive(false);


        dolly.AutomaticDolly.Enabled = true;
    }

    public void GameOver()
    {
        playerFuel.inGame = false;
        
        startButton.SetActive(true);

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
