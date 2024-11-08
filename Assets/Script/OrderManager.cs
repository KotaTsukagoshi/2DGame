using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OrderManager : MonoBehaviour
{
    public List<Sprite> possibleItems; // 全ての可能なアイテムのリスト
    public List<Customer> customerPrefabs; // 生成可能な顧客のプレハブリスト
    public Transform customerSpawnPoint; // 顧客が表示される場所

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
            Destroy(currentCustomer.gameObject);
        }

        Customer newCustomer = Instantiate(
            customerPrefabs[Random.Range(0, customerPrefabs.Count)],
            customerSpawnPoint.position,
            Quaternion.identity,
            customerSpawnPoint
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
        for (int i = 0; i < 3; i++) // 3つの注文アイテムを設定（任意で変更可能）
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
