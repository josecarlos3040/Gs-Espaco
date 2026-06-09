using TMPro;
using UnityEngine;

public class QuestsManager : MonoBehaviour
{
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
        buildMoon1.gameObject.SetActive(false);
        buildMoon2.gameObject.SetActive(false);
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
    }
}
