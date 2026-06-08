using Unity.VisualScripting;
using UnityEngine;

public class Moon : MonoBehaviour
{
    [SerializeField] Planet planet;
    [SerializeField] StoreManager storeManager;
    [SerializeField] PlayerMove playerMove;

    [SerializeField] GameObject baseMoon;
    [SerializeField] GameObject antenna;

    void Start()
    {
        baseMoon.SetActive(false);
        antenna.SetActive(false);
    }

    private void Update()
    {
        if (storeManager.moonSellMax)
        {
            planet.previewModel = antenna;
        }
        if (storeManager.moonFuelMax)
        {
            planet.previewModel = baseMoon;
        }


        if (playerMove.fuelMoonComplete)
        {
            baseMoon.SetActive(true);
        }
        if (playerMove.sellMoonComplete)
        {
            antenna.SetActive(true);
        }
    }
}
