using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OrderManager : MonoBehaviour
{
    public List<Sprite> itemSprites; //アイテムのリスト
    public List<Image> orderImages;　//UI上の注文アイテム表示用にImageリスト
    private List<Sprite> currentOrder = new List<Sprite>();

    void Start()
    {
        GenerateOrder();
    }

    public void GenerateOrder()
    {
        currentOrder.Clear();

        //表示されるアイテムをランダムに選択
        for (int i = 0; i < orderImages.Count; i++)
        {
            //ランダムにアイテムを選んで注文リストに追加
            Sprite randomItem = itemSprites[Random.Range(0, itemSprites.Count)];
            currentOrder.Add(randomItem);
            orderImages[i].sprite = randomItem;
            orderImages[i].enabled = true;
        }
    }

    public bool CheckOrder(Sprite itemSprite)
    {
        return currentOrder.Contains(itemSprite);
    }
}
