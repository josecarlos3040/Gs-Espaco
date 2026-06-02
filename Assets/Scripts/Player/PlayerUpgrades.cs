using Unity.VisualScripting;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] PlayerMove playerMove;
    [SerializeField] PlayerFuel playerFuel;
    [SerializeField] UIButtonsManager buttonsManager;

    [Header("Upgrades(Tem em outros codigos tbm)")]
    public float money;
    public float rewardMultiplier = 1f;
    public float scanerTime = 1f;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
