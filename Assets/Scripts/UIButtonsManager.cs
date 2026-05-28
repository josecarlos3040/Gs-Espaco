using UnityEngine;
using UnityEngine.UI;

public class UIButtonsManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] PlayerFuel playerFuel;
    [SerializeField] PlayerMove playerMove;

    [SerializeField] GameObject startButton;
    [SerializeField] GameObject gameOverButton;

    [SerializeField] GameObject player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        playerFuel.fuelSlider.maxValue = playerFuel.maxFuel;
        playerFuel.fuelSlider.value = playerFuel.maxFuel;

        playerFuel.inGame = true;

        startButton.SetActive(false);
    }

    public void GameOver()
    {
        playerFuel.inGame = false;
        startButton.SetActive(true);

        player.transform.position = new Vector3(0, -20, 0);
        player.transform.rotation = Quaternion.identity;
    }

}
