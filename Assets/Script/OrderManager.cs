using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OrderManager : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("All possible items that can be ordered.")]
    public List<Sprite> possibleItems; // 全ての注文可能なアイテムのリスト

    [Tooltip("List of customer prefabs to spawn.")]
    public List<Customer> customerPrefabs; // 生成可能な顧客プレハブのリスト

    [Header("Spawn Area")]
    [Tooltip("Top-left corner of the customer spawn area.")]
    public Transform spawnAreaTopLeft; // スポーン範囲の左上座標

    [Tooltip("Bottom-right corner of the customer spawn area.")]
    public Transform spawnAreaBottomRight; // スポーン範囲の右下座標

    private Customer currentCustomer; // 現在表示されている顧客オブジェクト

    /// <summary>
    /// Called at the start of the game to initialize orders and spawn the first customer.
    /// </summary>
    public void InitializeOrders()
    {
        SpawnCustomer(); // ゲーム開始時に顧客を生成
    }

    /// <summary>
    /// Spawns a new customer at a random position within the defined spawn area.
    /// </summary>
    public void SpawnCustomer()
    {
        // 既存の顧客がいる場合は削除する
        if (currentCustomer != null)
        {
            Destroy(currentCustomer.gameObject);
        }

        // ランダムな位置を計算して新しい顧客をスポーン
        Vector3 randomPosition = GetRandomSpawnPosition();
        Customer newCustomer = InstantiateRandomCustomer(randomPosition);

        // 顧客ごとにランダムな注文を生成し、セットアップ
        List<Sprite> customerOrder = GenerateOrder();
        newCustomer.SetupCustomer(newCustomer.customerImage.sprite, customerOrder);

        // TrayDropZoneコンポーネントにOrderManagerの参照を設定
        SetupTrayDropZone(newCustomer);

        // 現在の顧客を更新
        currentCustomer = newCustomer;
    }

    /// <summary>
    /// Generates a random position within the spawn area.
    /// </summary>
    /// <returns>A random Vector3 position within the spawn area.</returns>
    private Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(
            Random.Range(spawnAreaTopLeft.position.x, spawnAreaBottomRight.position.x),
            Random.Range(spawnAreaBottomRight.position.y, spawnAreaTopLeft.position.y),
            0
        );
    }

    /// <summary>
    /// Instantiates a random customer prefab at the specified position.
    /// </summary>
    /// <param name="position">The position to spawn the customer.</param>
    /// <returns>The instantiated Customer object.</returns>
    private Customer InstantiateRandomCustomer(Vector3 position)
    {
        // ランダムな顧客プレハブを選択し、指定されたランダム位置にスポーン
        Customer randomCustomerPrefab = customerPrefabs[Random.Range(0, customerPrefabs.Count)];
        Customer newCustomer = Instantiate(randomCustomerPrefab, position, Quaternion.identity);
        return newCustomer;
    }

    /// <summary>
    /// Sets up the TrayDropZone with a reference to this OrderManager.
    /// </summary>
    /// <param name="customer">The customer object to setup.</param>
    private void SetupTrayDropZone(Customer customer)
    {
        TrayDropZone trayDropZone = customer.GetComponentInChildren<TrayDropZone>();
        if (trayDropZone != null)
        {
            trayDropZone.SetOrderManager(this);
        }
    }

    /// <summary>
    /// Generates a random order for the customer.
    /// </summary>
    /// <returns>A list of Sprite representing the customer's order.</returns>
    private List<Sprite> GenerateOrder()
    {
        List<Sprite> order = new List<Sprite>();

        // 顧客に必要な注文アイテムを1つ選ぶ（必要に応じて注文数を増やせる）
        Sprite randomItem = possibleItems[Random.Range(0, possibleItems.Count)];
        order.Add(randomItem);

        return order;
    }

    /// <summary>
    /// Checks if the given item is part of the current customer's order.
    /// If the order is complete, spawns a new customer.
    /// </summary>
    /// <param name="itemSprite">The Sprite of the item being checked.</param>
    /// <returns>True if the item is part of the order, false otherwise.</returns>
    public bool CheckOrder(Sprite itemSprite)
    {
        if (currentCustomer != null && currentCustomer.orderItems.Contains(itemSprite))
        {
            currentCustomer.orderItems.Remove(itemSprite);
            currentCustomer.DisplayOrder(); // 顧客の注文リストを更新

            // すべての注文アイテムが完了した場合、新しい顧客を生成
            if (currentCustomer.orderItems.Count == 0)
            {
                SpawnCustomer();
            }
            return true;
        }
        return false;
    }
}
