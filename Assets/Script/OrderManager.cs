using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OrderManager : MonoBehaviour
{
    public List<Sprite> possibleItems; // 全ての可能なアイテムのリスト
    public List<Customer> customerPrefabs; // 生成可能な顧客のプレハブリスト
    public Transform customerSpawnPoint; // 顧客が表示される場所

    // カスタマーのスポーン範囲を指定するためのフィールド
    public Transform spawnAreaTopLeft;    // スポーン範囲の左上
    public Transform spawnAreaBottomRight; // スポーン範囲の右下

    private Customer currentCustomer;

    void Start()
    {
        SpawnCustomer();
    }

    // 新しい顧客を生成
    public void SpawnCustomer()
    {
        if (currentCustomer != null)
        {
            Destroy(currentCustomer.gameObject); // 現在の顧客を削除
        }

        // ランダムな位置を計算
        Vector3 randomPosition = new Vector3(
            Random.Range(spawnAreaTopLeft.position.x, spawnAreaBottomRight.position.x),
            Random.Range(spawnAreaBottomRight.position.y, spawnAreaTopLeft.position.y),
            0 // Z位置は通常2Dゲームで0に設定
        );

        // ランダムな顧客プレハブを選択し、指定範囲内のランダムな位置にスポーン
        Customer newCustomer = Instantiate(
            customerPrefabs[Random.Range(0, customerPrefabs.Count)],
            randomPosition,
            Quaternion.identity
        );

        List<Sprite> customerOrder = GenerateOrder();
        newCustomer.SetupCustomer(newCustomer.customerImage.sprite, customerOrder);

        // TrayDropZoneへのOrderManager参照を設定
        TrayDropZone trayDropZone = newCustomer.GetComponentInChildren<TrayDropZone>();
        if (trayDropZone != null)
        {
            trayDropZone.SetOrderManager(this); // ここでOrderManagerを設定
        }

        currentCustomer = newCustomer;
    }


    // ランダムに注文を生成
    private List<Sprite> GenerateOrder()
    {
        List<Sprite> order = new List<Sprite>();
        for (int i = 0; i < 3; i++)
        {
            Sprite randomItem = possibleItems[Random.Range(0, possibleItems.Count)];
            order.Add(randomItem);
        }
        return order;
    }

    // アイテムが正しく納品されたか確認
    public bool CheckOrder(Sprite itemSprite)
    {
        if (currentCustomer.orderItems.Contains(itemSprite))
        {
            currentCustomer.orderItems.Remove(itemSprite);
            currentCustomer.DisplayOrder(); // 表示更新

            // 全て納品されたら新しい顧客を表示
            if (currentCustomer.orderItems.Count == 0)
            {
                SpawnCustomer();
            }
            return true;
        }
        return false;
    }
}
