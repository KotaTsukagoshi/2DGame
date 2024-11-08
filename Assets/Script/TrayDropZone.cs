using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrayDropZone : MonoBehaviour, IDropHandler
{
    public OrderManager orderManager;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;
        if (droppedItem == null)
        {
            Debug.LogError("Dropped item is null");
            return;
        }

        Image itemImage = droppedItem.GetComponent<Image>();
        if (itemImage == null)
        {
            Debug.LogError("Dropped item does not have an Image component");
            return;
        }

        if (orderManager == null)
        {
            Debug.LogError("OrderManager reference is missing");
            return;
        }

        if (orderManager.CheckOrder(itemImage.sprite))
        {
            Debug.Log("納品完了！");
        }
        else
        {
            Debug.Log("不正なアイテムです");
        }
    }


    public void SetOrderManager(OrderManager manager)
    {
        orderManager = manager;
    }
}
