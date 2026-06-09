using Unity.VisualScripting;
using UnityEngine;

public class Moon : MonoBehaviour
{
    [SerializeField] Planet planet;
    [SerializeField] StoreManager storeManager;
    [SerializeField] PlayerMove playerMove;

    [SerializeField] GameObject baseMoon;
    [SerializeField] GameObject antenna;
    [SerializeField] GameObject moonO;

    void Start()
    {
        baseMoon.SetActive(false);
        antenna.SetActive(false);
    }

    private void Update()
    {
        if(playerMove.fuelMoonComplete && storeManager.moonFuelMax == false)
        {
            planet.previewModel = moonO;
        }
        if (playerMove.sellMoonComplete && storeManager.moonSellMax == false)
        {
            planet.previewModel = moonO;
        }

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
