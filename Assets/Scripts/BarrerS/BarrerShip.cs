using UnityEngine;

public class BarrerShip : MonoBehaviour
{
    public GameObject propulsor;
    public GameObject trail;

    [SerializeField] PlayerMove playerMove;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            propulsor.SetActive(false);
            trail.SetActive(true);

            Instantiate(playerMove.LockOutFirePrefab, playerMove.transform);
            // Atualiza a iluminação ambiente
            //DynamicGI.UpdateEnvironment();
        }
    }
}
