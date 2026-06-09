using UnityEngine;

public class AudioButton : MonoBehaviour
{
    [SerializeField] GameObject audioSettings;
    [SerializeField] bool isOpen = false;
    private void Start()
    {
        audioSettings.SetActive(isOpen);
    }
    public void OpenAndClose()
    {
        if(isOpen == false)
        {
            audioSettings.SetActive(true);
            isOpen = true;
        }
        else if(isOpen == true)
        {
            audioSettings.SetActive(false);
            isOpen = false;
            
        }
    }
}
