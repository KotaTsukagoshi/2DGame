using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Customer : MonoBehaviour
{
    public Image customerImage; // 顧客の画像
    public List<Sprite> orderItems; // 顧客ごとの注文アイテムリスト
    public List<Image> orderSlots; // UI上で注文を表示するスロット
    public TrayDropZone trayDropZone; // ドロップトレイの参照


    // 顧客の注文を表示
    public void DisplayOrder()
    {
        for (int i = 0; i < orderSlots.Count; i++)
        {
            if (i < orderItems.Count)
            {
                orderSlots[i].sprite = orderItems[i];
                orderSlots[i].enabled = true;
            }
            else
            {
                orderSlots[i].enabled = false;
            }
        }
    }

    // 顧客画像と注文を設定
    public void SetupCustomer(Sprite newImage, List<Sprite> newOrderItems)
    {
        customerImage.sprite = newImage;
        orderItems = newOrderItems;
        DisplayOrder();
    }
}
