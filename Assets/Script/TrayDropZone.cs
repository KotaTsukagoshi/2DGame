using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrayDropZone : MonoBehaviour, IDropHandler
{
    public OrderManager orderManager;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;
        if (droppedItem != null)
        {
            Image itemImage = droppedItem.GetComponent<Image>();
            if (itemImage != null)
            {
                if (orderManager.CheckOrder(itemImage.sprite))
                {
                    Debug.Log("納品完了！");
                }
                else
                {
                    Debug.Log("不正なアイテムです");
                }
            }
        }
    }

    public void SetOrderManager(OrderManager manager)
    {
        orderManager = manager;
    }
}
