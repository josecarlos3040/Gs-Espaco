using UnityEngine;

public class PlanetPreview : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 30f;

    void Update()
    {
        transform.Rotate(
            0,
            rotationSpeed * Time.deltaTime,
            0
        );
    }
}