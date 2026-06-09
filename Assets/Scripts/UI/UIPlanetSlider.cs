using TMPro;
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
    [SerializeField] GameObject fillObject;

    [SerializeField] RawImage planetPreviewUI;
    [SerializeField] Transform previewRoot;

    [SerializeField] UIButtonsManager buttonsManager;

    Planet lastPlanet;

    GameObject currentPreview;

    bool initialized;

    void Start()
    {
        planetSliderUI.gameObject.SetActive(false);
        planetPreviewUI.gameObject.SetActive(false);

        buttonsManager.goBackText.gameObject.SetActive(false);
    }

    public void Update()
    {
        
        if (planet.isScanned)
        {
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
            }


            planetSliderUI.value = Mathf.Max(0, planetSliderUI.value - Time.deltaTime);

            if (planetSliderUI.value <= 0)
            {
                fillObject.SetActive(false);


                if (planet.CompareTag("Moon"))
                {
                    playerMove.scannedMoon = true;

                    if(!playerMove.sellMoonComplete || !playerMove.fuelMoonComplete)
                    {
                        buttonsManager.goBackText.gameObject.SetActive(true);
                    }


                    if (storeManager.moonFuelMax)
                    {
                        playerMove.fuelMoonComplete = true;
                        storeManager.moonFuelMax = false;
                        storeManager.sellMoonButton.interactable = true;
                        
                    }

                    

                    if (storeManager.moonSellMax)
                    {
                        playerMove.sellMoonComplete = true;
                        storeManager.moonSellMax = false;
                        storeManager.fuelMoonButton.interactable = true;
                        
                    }


                    if (playerMove.sellMoonComplete && playerMove.fuelMoonComplete)
                    {
                        buttonsManager.goToMars.gameObject.SetActive(true);
                    }
                    
                }

                if (planet.CompareTag("Mars"))
                {
                    playerMove.scannedMars = true;

                    if (!playerMove.sellMarsComplete && !playerMove.fuelMarsComplete)
                    {
                        buttonsManager.goBackText.gameObject.SetActive(true);
                    }

                    if (storeManager.marsFuelMax)
                    {
                        playerMove.fuelMarsComplete = true;
                        storeManager.marsFuelMax = false;
                        storeManager.sellMarsButton.interactable = true;

                    }

                    

                    if (storeManager.marsSellMax)
                    {
                        playerMove.sellMarsComplete = true;
                        storeManager.marsSellMax = false;
                        storeManager.fuelMarsButton.interactable = true;

                    }
                    

                    if (storeManager.marsColonyMax)
                    {
                        playerMove.finalMarsComplete = true;
                    }
                    

                }

                if (planet.CompareTag("Moon"))
                {

                    HidePreview();
                    return;
                }

                if (planet.CompareTag("Mars"))
                {

                    HidePreview();
                    return;
                }
                else { playerUpg.ReceivePlanetItem(planet);
                    planet.isScanned = true;}
                
                //playerUpg.inventory[]
                //playerUpg.money += moneyReward;

                if (currentPreview != null)
                {
                    Destroy(currentPreview);
                    currentPreview = null;
                }

            
            }
        }
        else
        {
            Debug.Log("NÃO ESTÁ EM ORBITA DESSE PLANETA");
            fillObject.SetActive(true);

            HidePreview();


        }
    }

    public void SetSliderValue()
    {
        fillObject.SetActive(true);
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


        if(planet.CompareTag("Moon") && storeManager.moonFuelMax)
        {
            currentPreview.SetActive(true);
            currentPreview.transform.localPosition = new Vector3(0, 0, -4.76000023f);

            currentPreview.transform.localRotation = Quaternion.Euler(-130.264f, 0f, 0f);

            currentPreview.transform.localScale = new Vector3(90.95363f, 90.95363f, 90.95363f);
            ResetChildrenTransforms(currentPreview.transform);
        }
        else if(planet.CompareTag("Moon") && storeManager.moonSellMax)
        {
            currentPreview.SetActive(true);

            currentPreview.transform.localPosition = new Vector3(0.0633983f, -0.6686959f, -15.7071f);

            currentPreview.transform.localRotation = Quaternion.Euler(-132.003f, -0.7269897f, 92.685f);

            currentPreview.transform.localScale = new Vector3(51.01642f, 51.01642f, 51.01642f);
            ResetChildrenTransforms(currentPreview.transform);
        }

        else if (planet.CompareTag("Moon"))
        {
            currentPreview.transform.localPosition = new Vector3(-4.61852778e-13f, 0, 23.7500076f);
        }

        if (planet.CompareTag("Mars") && storeManager.marsFuelMax)
        {
            currentPreview.SetActive(true);
            currentPreview.transform.localPosition = new Vector3(0, 0, -4.76000023f);

            currentPreview.transform.localRotation = Quaternion.Euler(-130.264f, 0f, 0f);

            currentPreview.transform.localScale = new Vector3(90.95363f, 90.95363f, 90.95363f);
            ResetChildrenTransforms(currentPreview.transform);
        }
        else if (planet.CompareTag("Mars") && storeManager.marsSellMax)
        {
            currentPreview.SetActive(true);

            currentPreview.transform.localPosition = new Vector3(0.0633983f, -0.6686959f, -15.7071f);

            currentPreview.transform.localRotation = Quaternion.Euler(-132.003f, -0.7269897f, 92.685f);

            currentPreview.transform.localScale = new Vector3(51.01642f, 51.01642f, 51.01642f);
            ResetChildrenTransforms(currentPreview.transform);
        }

        else if (planet.CompareTag("Mars"))
        {
            currentPreview.transform.localPosition = new Vector3(-4.61852778e-13f, 0, 23.7500076f);
        }

        else if(planet.CompareTag("Planet"))
        {
            currentPreview.transform.localPosition = new Vector3(0, 0, -15);
        }
    }

    void ResetChildrenTransforms(Transform parent) 
    {
        foreach (Transform child in parent)
        {
            child.localPosition = Vector3.zero;
            child.localRotation = Quaternion.identity;
            child.localScale = Vector3.one;

            ResetChildrenTransforms(child);
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
            playerMove.planetSlider.gameObject.SetActive(false);
            
            Destroy(currentPreview);
            currentPreview = null;
            
        }

        initialized = false;
    }
}