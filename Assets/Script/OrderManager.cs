using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OrderManager : MonoBehaviour
{
    public List<Sprite> possibleItems; // 全ての可能なアイテムリスト（注文可能なアイテム）
    public List<Customer> customerPrefabs; // 生成可能な顧客プレハブのリスト

    public Transform spawnAreaTopLeft; // スポーン範囲の左上座標
    public Transform spawnAreaBottomRight; // スポーン範囲の右下座標

    private Customer currentCustomer; // 現在表示されている顧客オブジェクト

    // ゲームスタート時に呼ばれる初期化メソッド
    public void InitializeOrders()
    {
        SpawnCustomer(); // ゲーム開始時に顧客を生成
    }

    // 新しい顧客をスポーンするメソッド
    public void SpawnCustomer()
    {
        // 既存の顧客がいる場合は削除する
        if (currentCustomer != null)
        {
            Destroy(currentCustomer.gameObject);
        }

        // 顧客をスポーンするランダムな位置を計算
        Vector3 randomPosition = new Vector3(
            Random.Range(spawnAreaTopLeft.position.x, spawnAreaBottomRight.position.x),
            Random.Range(spawnAreaBottomRight.position.y, spawnAreaTopLeft.position.y),
            0
        );
        Debug.Log("Random Spawn Position: " + randomPosition);

        // ランダムな顧客プレハブを選択し、指定されたランダム位置にスポーン
        Customer newCustomer = Instantiate(
            customerPrefabs[Random.Range(0, customerPrefabs.Count)],
            randomPosition,
            Quaternion.identity
        );

        // 顧客ごとにランダムな注文を生成し、顧客をセットアップ
        List<Sprite> customerOrder = GenerateOrder();
        newCustomer.SetupCustomer(newCustomer.customerImage.sprite, customerOrder);

        // TrayDropZoneコンポーネントにOrderManagerの参照を設定
        TrayDropZone trayDropZone = newCustomer.GetComponentInChildren<TrayDropZone>();
        if (trayDropZone != null)
        {
            trayDropZone.SetOrderManager(this);
        }

        // 現在の顧客を更新
        currentCustomer = newCustomer;
    }

    private List<Sprite> GenerateOrder()
    {
        List<Sprite> order = new List<Sprite>();

        // 顧客に必要な注文アイテムを1つ選ぶ
        for (int i = 0; i < 1; i++)
        {
            Sprite randomItem = possibleItems[Random.Range(0, possibleItems.Count)];
            order.Add(randomItem);
        }
        return order;
    }

    public bool CheckOrder(Sprite itemSprite)
    {
        if (currentCustomer.orderItems.Contains(itemSprite))
        {
            currentCustomer.orderItems.Remove(itemSprite);
            currentCustomer.DisplayOrder();

            if (currentCustomer.orderItems.Count == 0)
            {
                SpawnCustomer();
            }
            return true;
        }
        return false;
    }
}
