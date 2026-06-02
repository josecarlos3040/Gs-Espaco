using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] UIPlanetSlider slider;

    public bool isScanned;

    void Start()
    {
        isScanned = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
