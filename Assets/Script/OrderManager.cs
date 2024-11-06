using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OrderManager : MonoBehaviour
{
    // 納品に必要なアイテムの候補リスト（可能なすべてのアイテム）
    public List<Sprite> possibleItems;

    // OrderPanel 内の注文スロット（UIのImage要素）
    public List<Image> orderSlots;

    // 現在の注文アイテムリスト（プレイヤーが納品すべきアイテム）
    private List<Sprite> currentOrderItems = new List<Sprite>();

    void Start()
    {
        // ゲーム開始時に最初の注文を生成
        GenerateOrder();
    }

    // 現在の注文を生成し、注文スロットに表示するメソッド
    // ランダムにアイテムを選んで注文リストを作成
    public void GenerateOrder()
    {
        // 現在の注文リストをクリア
        currentOrderItems.Clear();

        // 注文スロットの数だけ繰り返し、ランダムにアイテムを選んで設定
        for (int i = 0; i < orderSlots.Count; i++)
        {
            // 納品候補リストからランダムにアイテムを選択
            Sprite randomItem = possibleItems[Random.Range(0, possibleItems.Count)];

            // 現在の注文アイテムリストに追加
            currentOrderItems.Add(randomItem);

            // 注文スロットにアイテムのスプライトを設定し表示
            orderSlots[i].sprite = randomItem;
            orderSlots[i].enabled = true;
        }
    }

    // プレイヤーが納品しようとしたアイテムが注文リストにあるかを確認するメソッド
    // itemSprite: プレイヤーが納品したアイテムのスプライト
    // 戻り値: アイテムが注文リストに含まれていればtrue、それ以外はfalse
    public bool CheckOrder(Sprite itemSprite)
    {
        // 注文リストにアイテムがあれば、それをリストから削除
        if (currentOrderItems.Contains(itemSprite))
        {
            // 正しいアイテムが納品された場合
            currentOrderItems.Remove(itemSprite);

            // 注文パネルを更新（表示されるアイテムを減らす）
            UpdateOrderPanel();

            return true;
        }

        // 間違ったアイテムが納品された場合
        return false;
    }

    // 注文リストを更新するメソッド
    // まだ納品されていないアイテムを注文スロットに再設定し、納品済みアイテムを非表示にする
    private void UpdateOrderPanel()
    {
        // 注文スロットの数だけ繰り返す
        for (int i = 0; i < orderSlots.Count; i++)
        {
            // 注文リストのアイテムがまだ残っている場合は、それを表示
            if (i < currentOrderItems.Count)
            {
                orderSlots[i].sprite = currentOrderItems[i];
                orderSlots[i].enabled = true;
            }
            else
            {
                // 注文リストにアイテムが残っていない場合は、スロットを非表示にする
                orderSlots[i].enabled = false;
            }

            GenerateOrder();

        }
    }
}
