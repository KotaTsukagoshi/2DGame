using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrayDropZone : MonoBehaviour, IDropHandler
{
    // orderManager: 注文アイテムを管理する OrderManager への参照
    public OrderManager orderManager;

    // ドラッグされているアイテムがこのドロップゾーンにドロップされたときに呼ばれるメソッド
    // eventData: ドロップされたアイテムのイベントデータ
    public void OnDrop(PointerEventData eventData)
    {
        // ドロップされたゲームオブジェクトを取得
        GameObject droppedItem = eventData.pointerDrag;

        // ドロップされたアイテムが存在するか確認
        if (droppedItem != null)
        {
            // ドロップされたアイテムの Image コンポーネントを取得
            Image itemImage = droppedItem.GetComponent<Image>();

            // アイテムが Image コンポーネントを持っているか、orderManager が設定されているかを確認
            if (itemImage != null && orderManager != null)
            {
                // アイテムのスプライトが注文リスト内に存在するかチェック
                if (orderManager.CheckOrder(itemImage.sprite))
                {
                    Debug.Log("納品完了！");
                    Destroy(droppedItem); // 正しいアイテムが納品された場合、アイテムを削除
                }
                else
                {
                    Debug.Log("間違ったアイテムが納品されました！");
                }
            }
            else
            {
                Debug.LogWarning("アイテムにImageコンポーネントが存在しないか、OrderManagerが未設定です");
            }
        }
    }
}
