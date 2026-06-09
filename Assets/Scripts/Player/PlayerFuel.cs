using UnityEngine;
using UnityEngine.UI;

public class PlayerFuel : MonoBehaviour
{
    [SerializeField] public float maxFuel;
    [SerializeField] public bool inGame = false;
    [SerializeField] public bool gameOver;

    [SerializeField] public bool inSateliteOrbit = false;

    [Header("Components")]
    [SerializeField] PlayerMove playerMove;
    public Slider fuelSlider;
    

    private void FixedUpdate()
    {

        if (playerMove.isOrbit && !inSateliteOrbit)
        {
            fuelSlider.value -= Time.fixedDeltaTime;
        }

        if (fuelSlider.value <= 0)
        {
            playerMove.outFuel = true;
            gameOver = true;
        }
        else
        {
            playerMove.outFuel = false;
        }

        if(playerMove.outFuel == true)
        {
            fuelSlider.value = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Satelite"))
        {
            inSateliteOrbit = true;
            fuelSlider.value = maxFuel;
        }
        if (playerMove.fuelMoonComplete && other.CompareTag("Moon"))
        {
            inSateliteOrbit = true;
            fuelSlider.value = maxFuel;
        }
        if (playerMove.fuelMarsComplete && other.CompareTag("Mars"))
        {
            inSateliteOrbit = true;
            fuelSlider.value = maxFuel;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Satelite"))
        {
            inSateliteOrbit = false;
        }
        if (other.CompareTag("Moon"))
        {
            inSateliteOrbit = false;
        }
        if (other.CompareTag("Mars"))
        {
            inSateliteOrbit = false;
        }
    }

}
