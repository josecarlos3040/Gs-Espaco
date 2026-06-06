using UnityEngine;
using UnityEngine.UI;

public class UIPlanetSlider : MonoBehaviour
{

    // ESSE CODIGO É RESPONSÁVEL POR GERENCIAR O SLIDER E O PREVIEW DE PROGRESSO DE SCAN DO PLANETA
    [Header("Runtime and money")]
    [SerializeField] float planetCompletionTime;
    //[SerializeField] float moneyReward;

    [Header("Components")]
    [SerializeField] PlayerMove playerMove;
    [SerializeField] public PlayerUpgrades playerUpg;
    [SerializeField] Planet planet;

    [SerializeField] Slider planetSliderUI;

    [SerializeField] RawImage planetPreviewUI;
    [SerializeField] Transform previewRoot;

    GameObject currentPreview;

    bool initialized;

    void Start()
    {
        planetSliderUI.gameObject.SetActive(false);
        planetPreviewUI.gameObject.SetActive(false);
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

                ShowPlanetPreview();
                planetPreviewUI.gameObject.SetActive(true);
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

                planetSliderUI.gameObject.SetActive(false);
                planetPreviewUI.gameObject.SetActive(false);

                if (currentPreview != null)
                {
                    Destroy(currentPreview);
                    currentPreview = null;
                }
            }
        }
        else
        {
            planetSliderUI.gameObject.SetActive(false);
            initialized = false;

            planetSliderUI.gameObject.SetActive(false);
            planetPreviewUI.gameObject.SetActive(false);

            if (currentPreview != null)
            {
                Destroy(currentPreview);
                currentPreview = null;
            }
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

    void ShowPlanetPreview()
    {
        if (currentPreview != null)
            Destroy(currentPreview);

        currentPreview = Instantiate(
            planet.previewModel,
            previewRoot.position,
            Quaternion.identity
        );

        SetLayerRecursively(
            currentPreview,
            LayerMask.NameToLayer("PlanetPreview")
        );
    }

    void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }
}