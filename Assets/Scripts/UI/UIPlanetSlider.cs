using UnityEngine;
using UnityEngine.UI;

public class UIPlanetSlider : MonoBehaviour
{
    [Header("Runtime and money")]
    [SerializeField] float planetCompletionTime;
    [SerializeField] float moneyReward;

    [Header("Components")]
    [SerializeField] PlayerMove playerMove;
    [SerializeField] PlayerUpgrades playerUpg;
    [SerializeField] Planet planet;

    [SerializeField] Slider planetSliderUI;

    bool initialized;

    void Start()
    {
        planetSliderUI.gameObject.SetActive(false);
    }

    void Update()
    {
        bool orbitingThisPlanet = playerMove.isOrbit & playerMove.planet == transform;

        if (planet.isScanned)
            return;

        if (orbitingThisPlanet)
        {
            if (!initialized)
            {
                initialized = true;
                SetSliderValue();
            }

            planetSliderUI.gameObject.SetActive(true);

            planetSliderUI.value = Mathf.Max(0, planetSliderUI.value - Time.deltaTime);

            if (planetSliderUI.value <= 0)
            {
                planet.isScanned = true;
                playerUpg.money += moneyReward;

                planetSliderUI.gameObject.SetActive(false);
            }
        }
        else
        {
            planetSliderUI.gameObject.SetActive(false);
        }
    }

    public void SetSliderValue()
    {
        planetCompletionTime = playerMove.orbitRadius * playerUpg.scanerTime;

        moneyReward = planetCompletionTime * playerUpg.rewardMultiplier;

        planetSliderUI.maxValue = planetCompletionTime;
        planetSliderUI.value = planetCompletionTime;
    }
}