using UnityEngine;

public class BarrerCamera : MonoBehaviour
{
    public GameObject cameraDolly;
    public GameObject cameraSpace;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraDolly.SetActive(false);
            cameraSpace.SetActive(true);

            // Atualiza a iluminação ambiente
            //DynamicGI.UpdateEnvironment();
        }
    }
}
