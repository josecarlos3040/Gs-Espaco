using TMPro;
using UnityEngine;

public class QuestsManager : MonoBehaviour
{

    [SerializeField] GameObject normalQuests;
    [SerializeField] GameObject specialQuests;

    [SerializeField] TextMeshProUGUI goToMoon;



    [SerializeField] TextMeshProUGUI buildMoon1;
    [SerializeField] TextMeshProUGUI buildMoon2;

    [SerializeField] TextMeshProUGUI goToMars;

    [SerializeField] TextMeshProUGUI buildMars1;
    [SerializeField] TextMeshProUGUI buildMars2;
    [SerializeField] TextMeshProUGUI buildMars3;

    


    [Header("Scripts")]
    [SerializeField] PlayerMove playerMove;
    private void Start()
    {
        goToMars.gameObject.SetActive(false);

        buildMoon1.gameObject.SetActive(false);
        buildMoon2.gameObject.SetActive(false);

        buildMars1.gameObject.SetActive(false);
        buildMars2.gameObject.SetActive(false);
        buildMars3.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (playerMove.scannedMoon)
        {
            goToMoon.color = new Color32(157, 111, 26, 255);

            buildMoon1.gameObject.SetActive(true);
            buildMoon2.gameObject.SetActive(true);

        }
        if (playerMove.fuelMoonComplete)
        {
            buildMoon1.color = new Color32(157, 111, 26, 255);
        }
        if (playerMove.sellMoonComplete)
        {
            buildMoon2.color = new Color32(157, 111, 26, 255);
        }

        if (playerMove.scannedMars)
        {
            goToMars.color = new Color32(157, 111, 26, 255);
            buildMars1.gameObject.SetActive(true);
            buildMars2.gameObject.SetActive(true);
        }

        if (playerMove.fuelMarsComplete)
        {
            buildMars1.color = new Color32(157, 111, 26, 255);
        }
        if (playerMove.sellMarsComplete)
        {
            buildMars2.color = new Color32(157, 111, 26, 255);
        }
        if (playerMove.finalMarsComplete)
        {
            buildMars2.color = new Color32(157, 111, 26, 255);
        }


        if (playerMove.fuelMarsComplete && playerMove.sellMarsComplete)
        {
            normalQuests.SetActive(false);
            specialQuests.SetActive(true);
        }

        if (playerMove.fuelMoonComplete && playerMove.sellMoonComplete)
        {
            goToMars.gameObject.SetActive(true);
        }

    }
}
