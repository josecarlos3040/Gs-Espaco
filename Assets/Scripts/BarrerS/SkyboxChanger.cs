using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    [SerializeField] Material spaceSkybox;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RenderSettings.skybox = spaceSkybox;
            SoundManager.Instance.StopSFX();

            // Atualiza a iluminação ambiente
            //DynamicGI.UpdateEnvironment();
        }
    }
}