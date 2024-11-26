using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Customer : MonoBehaviour
{
    // 顧客の見た目を表す画像コンポーネント
    public Image customerImage;

    // 顧客の注文アイテムを保持するリスト
    public List<Sprite> orderItems;

    // UI上で注文を表示するスロットのリスト（各スロットにアイテム画像が表示される）
    public List<Image> orderSlots;

    // ドロップトレイへの参照、注文アイテムのドラッグ&ドロップを管理
    public TrayDropZone trayDropZone;

    /// 顧客の注文内容をUIに表示するメソッド。
    /// orderSlotsにorderItemsを割り当てて、必要なスロットのみ表示します。
    public void DisplayOrder()
    {
        for (int i = 0; i < orderSlots.Count; i++)
        {
            // 注文アイテム数以内であれば、スロットにアイテム画像をセットして表示を有効化
            if (i < orderItems.Count)
            {
                orderSlots[i].sprite = orderItems[i];
                orderSlots[i].enabled = true;
            }
            // 注文アイテムがないスロットは非表示にする
            else
            {
                orderSlots[i].enabled = false;
            }
        }
    }

    /// 顧客の画像と注文を設定するメソッド。
    /// 新しい顧客の画像と注文リストを設定し、表示を更新します。
    /// <param name="newImage">顧客の画像</param>
    /// <param name="newOrderItems">注文アイテムのリスト</param>
    public void SetupCustomer(Sprite newImage, List<Sprite> newOrderItems)
    {
        customerImage.sprite = newImage; // 顧客の画像をセット
        orderItems = newOrderItems;      // 注文リストをセット
        DisplayOrder();                  // 設定した内容をUIに反映
    }
}
