using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    public Image itemIcon;

    public void Set(Sprite sprite)
    {
        itemIcon.sprite = sprite;
    }
}