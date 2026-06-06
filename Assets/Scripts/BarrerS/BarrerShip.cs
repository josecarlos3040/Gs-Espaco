using UnityEngine;

public class BarrerShip : MonoBehaviour
{
    public GameObject propulsor;
    public GameObject trail; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            propulsor.SetActive(false);
            trail.SetActive(true);

            // Atualiza a iluminação ambiente
            //DynamicGI.UpdateEnvironment();
        }
    }
}
