using UnityEngine;
using UnityEngine.UI;

public class PlayerFuel : MonoBehaviour
{
    [SerializeField] public float maxFuel;
    [SerializeField] public bool inGame = false;
    [SerializeField] public bool gameOver;

    [Header("Components")]
    [SerializeField] PlayerMove playerMove;
    public Slider fuelSlider;
    

    private void FixedUpdate()
    {
        if (playerMove.isOrbit)
        {
            fuelSlider.value -= Time.deltaTime;
        }
    }


}
