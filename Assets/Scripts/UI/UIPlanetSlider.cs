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
    [SerializeField] StoreManager storeManager;

    [SerializeField] Slider planetSliderUI;

    [SerializeField] RawImage planetPreviewUI;
    [SerializeField] Transform previewRoot;

    Planet lastPlanet;

    GameObject currentPreview;

    bool initialized;

    void Start()
    {
        planetSliderUI.gameObject.SetActive(false);
        planetPreviewUI.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (planet.isScanned)
        {
            HidePreview();
            return;
        }

        if (playerMove.planet == null)
        {
            HidePreview();
            return;
        }

        Planet currentPlanet = playerMove.planet.GetComponentInParent<Planet>();

        if (currentPlanet != planet)
        {
            return;
        }
        // Verifica se o player está em órbita e se o planeta atual do player é este planeta
        bool orbitingThisPlanet = playerMove.isOrbit && playerMove.planet != null && playerMove.planet.GetComponentInParent<Planet>() == planet;

        if (orbitingThisPlanet)
        {
            if (currentPlanet != lastPlanet)
            {
                initialized = false;
                lastPlanet = currentPlanet;
            }

            if (!initialized)
            {
                Debug.Log("RESETOU O SLIDER");

                initialized = true;
                SetSliderValue();

                ShowPlanetPreview();
                planetPreviewUI.gameObject.SetActive(true);
            }

            planetSliderUI.gameObject.SetActive(true);
            
            planetSliderUI.value = Mathf.Max(0, planetSliderUI.value - Time.deltaTime);

            if (planetSliderUI.value <= 0)
            {
                if (planet.CompareTag("Moon"))
                {
                    playerMove.scannedMoon = true;
                    if (storeManager.moonFuelMax)
                    {
                        playerMove.fuelMoonComplete = true;
                        storeManager.sellMoonButton.interactable = true;
                    }

                    else { return; }

                    if (storeManager.moonSellMax)
                    {
                        playerMove.sellMoonComplete = true;
                        storeManager.fuelMoonButton.interactable = true;
                    }
                    else { return; }

                }
                
                if (planet.CompareTag("Moon"))
                {

                    HidePreview();
                    return;
                }
                else { playerUpg.ReceivePlanetItem(planet);
                    planet.isScanned = true;}
                
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
                planetSliderUI.gameObject.SetActive(false);
                planetPreviewUI.gameObject.SetActive(false);
            }
        }
        else
        {
            planetSliderUI.gameObject.SetActive(false);
            planetPreviewUI.gameObject.SetActive(false);
            HidePreview();


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
        planetSliderUI.value = planetSliderUI.maxValue;
        if (currentPreview != null)
            Destroy(currentPreview);

        currentPreview = Instantiate(
            planet.previewModel,
            previewRoot
        );

        currentPreview.transform.localPosition = Vector3.zero;
        currentPreview.transform.localRotation = Quaternion.identity;

        SetLayerRecursively(
            currentPreview,
            LayerMask.NameToLayer("PlanetPreview")
        );

        if (planet.CompareTag("Moon"))
        {
            currentPreview.transform.localPosition = new Vector3(0, 0, 20);
        }
        else
        {
            currentPreview.transform.localPosition = new Vector3(0, 0, -15);
        }
    }

    void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }

    void HidePreview()
    {
        if (currentPreview != null)
        {
            Destroy(currentPreview);
            currentPreview = null;
        }

        initialized = false;
    }
}