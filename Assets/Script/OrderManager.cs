using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OrderManager : MonoBehaviour
{
    // itemSprites: ゲーム内で利用可能な全てのアイテムのスプライトリスト
    public List<Sprite> itemSprites;

    // orderImages: 注文アイテムを表示するためのUI Imageリスト
    public List<Image> orderImages;

    // currentOrder: 現在の注文として必要なアイテムのスプライトリスト
    private List<Sprite> currentOrder = new List<Sprite>();

    void Start()
    {
        // ゲーム開始時に最初の注文を生成
        GenerateOrder();
    }

    // 現在の注文を生成し、UIにランダムにアイテムを表示するメソッド
    public void GenerateOrder()
    {
        // 前の注文をクリアし、新しい注文アイテムリストを作成
        currentOrder.Clear();

        // orderImagesの数だけランダムなアイテムを選んで、注文リストとUIに設定
        for (int i = 0; i < orderImages.Count; i++)
        {
            // itemSpritesからランダムにアイテムを選択し、currentOrderとorderImagesに追加
            Sprite randomItem = itemSprites[Random.Range(0, itemSprites.Count)];
            currentOrder.Add(randomItem);
            orderImages[i].sprite = randomItem;
            orderImages[i].enabled = true; // UI要素を表示する
        }
    }

    // プレイヤーがトレーに乗せたアイテムが注文リストにあるかを確認するメソッド
    // itemSprite: プレイヤーが納品しようとしたアイテムのスプライト
    // 戻り値: 注文リストに含まれていればtrue、含まれていなければfalse
    public bool CheckOrder(Sprite itemSprite)
    {
        return currentOrder.Contains(itemSprite);
    }
}
