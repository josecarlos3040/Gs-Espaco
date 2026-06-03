using UnityEngine;

public class Planet : MonoBehaviour
{

    [SerializeField] UIPlanetSlider slider;
    public PlayerUpgrades upgrades;
    public Transform orbitCenter;
    public bool isScanned;

    public Sprite item;
    public string itemName;

    void Start()
    {
        isScanned = false;
    }

    // planeta "entrega" o item
    public InventoryItem GetItem()
    {
        return new InventoryItem
        {
            sprite = item,
            itemName = itemName
        };
    }

    // interação simples por trigger
    private void OnTriggerEnter(Collider other)
    {
        PlayerUpgrades player = other.GetComponent<PlayerUpgrades>();

        if (player != null)
        {
            player.InteractWithPlanet(this);
        }
    }
}