using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrayDropZone : MonoBehaviour, IDropHandler
{
    public OrderManager orderManager; // OrderManagerへの参照（顧客の注文を管理）

    // ドロップイベントが発生したときに呼ばれる
    public void OnDrop(PointerEventData eventData)
    {
        // ドロップされたアイテムを取得
        GameObject droppedItem = eventData.pointerDrag;
        if (droppedItem == null)
        {
            Debug.LogError("Dropped item is null");
            return;
        }

        // ドロップされたアイテムのImageコンポーネントを取得
        Image itemImage = droppedItem.GetComponent<Image>();
        if (itemImage == null)
        {
            Debug.LogError("Dropped item does not have an Image component");
            return;
        }

        // OrderManagerへの参照が設定されているか確認
        if (orderManager == null)
        {
            Debug.LogError("OrderManager reference is missing");
            return;
        }

        // アイテムが注文の一部かどうかをOrderManagerで確認
        if (orderManager.CheckOrder(itemImage.sprite))
        {
            Debug.Log("納品完了！");
        }
        else
        {
            Debug.Log("不正なアイテムです");
        }
    }

    // OrderManagerの参照を設定するメソッド
    public void SetOrderManager(OrderManager manager)
    {
        orderManager = manager;
    }
}
