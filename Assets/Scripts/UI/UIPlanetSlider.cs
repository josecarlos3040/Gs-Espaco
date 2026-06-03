using UnityEngine;
using UnityEngine.UI;

public class UIPlanetSlider : MonoBehaviour
{
    [Header("Runtime and money")]
    [SerializeField] float planetCompletionTime;
    //[SerializeField] float moneyReward;

    [Header("Components")]
    [SerializeField] PlayerMove playerMove;
    [SerializeField] public PlayerUpgrades playerUpg;
    [SerializeField] Planet planet;

    [SerializeField] Slider planetSliderUI;

    bool initialized;

    void Start()
    {
        planetSliderUI.gameObject.SetActive(false);
    }

    public void Update()
    {
        // Verifica se o player está em órbita e se o planeta atual do player é este planeta
        bool orbitingThisPlanet = playerMove.isOrbit && playerMove.planet != null && playerMove.planet.GetComponentInParent<Planet>() == planet;

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
                playerUpg.ReceivePlanetItem(planet);
                //playerUpg.inventory[]
                //playerUpg.money += moneyReward;

                planetSliderUI.gameObject.SetActive(false);
            }
        }
        else
        {
            planetSliderUI.gameObject.SetActive(false);
            initialized = false;
        }
    }

    public void SetSliderValue()
    {
        planetCompletionTime = playerMove.orbitRadius * playerUpg.scanerTime;

        
        //moneyReward = planetCompletionTime * playerUpg.rewardMultiplier;

        planetSliderUI.maxValue = planetCompletionTime;
        planetSliderUI.value = planetCompletionTime;
    }
}