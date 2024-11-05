using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class OrderManager : MonoBehaviour
{
    public List<Sprite> itemSprites; //注文に使われるアイテムのリスト
    public List<Image> orderImages;　//UI上の注文アイテム表示用にImageリスト
    private List<Sprite> currentOrder = new List<Sprite>();


    void Start()
    {
        GenerateOrder();
    }

    public void GenerateOrder()
    {
        currentOrder.Clear();
        // itemSpritesがnullまたは空でないか確認
        if (itemSprites == null || itemSprites.Count == 0)
        {
            Debug.LogError("itemSpritesが設定されていないか、空です。");
            return;
        }

        for (int i = 0; i < orderImages.Count; i++)
        {
            if (i < itemSprites.Count) // アイテムが十分にあるか確認
            {
                //ランダムにアイテムを選んで注文リストに追加
                Sprite randomItem = itemSprites[Random.Range(0, itemSprites.Count)];
                currentOrder.Add(randomItem);
                orderImages[i].sprite = randomItem;
                orderImages[i].enabled = true;
            }
            else
            {
                Debug.LogWarning("アイテムが不足してます。");
            }
        }
    }

    public bool CheckOrder(Sprite deliveredItem)
    {
        return currentOrder.Contains(deliveredItem);
    }
}
