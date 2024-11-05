using UnityEngine;
using UnityEngine.EventSystems;

public class TraiDropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;
        if (droppedItem != null)
        {
            //アイテムをトレーの位置に配置
            droppedItem.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            //納品完了処理を追加する
            Debug.Log("納品完了！");
        }
    }
}