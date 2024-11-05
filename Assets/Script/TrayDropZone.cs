using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TraiDropZone : MonoBehaviour, IDropHandler
{
    public OrderManager orderManager;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;
        if (droppedItem != null)
        {
            //ドロップされたアイテムのスプライトを取得
            Image itemImage = droppedItem.GetComponent<Image>();
            if (itemImage != null)
            {
                //注文リストと一致するか確認
                if (orderManager.CheckOrder(itemImage.sprite))
                {
                    Debug.Log("納品完了！");
                    Destroy(droppedItem);//正しく納品されたのでアイテム削除
                }
            }
            else
            {
                Debug.Log("間違ったアイテムが納品されました！");
            }
        }
    }
}