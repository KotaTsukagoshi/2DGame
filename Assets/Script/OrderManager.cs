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
        currentOrder.Clear();

        // 手持ちアイテムを固定順で表示
        for (int i = 0; i < orderImages.Count; i++)
        {
            // 手持ちアイテムリスト内に十分なアイテムがあるか確認
            if (i < itemSprites.Count)
            {
                Sprite item = itemSprites[i]; // 順番にアイテムを取得
                currentOrder.Add(item);
                orderImages[i].sprite = item;
                orderImages[i].enabled = true;
            }
            else
            {
                orderImages[i].enabled = false; // リストが足りない場合は空白に
            }
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
