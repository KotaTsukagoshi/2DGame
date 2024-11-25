using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OrderManager : MonoBehaviour
{
    [Header("設定")]
    [Tooltip("注文可能なすべてのアイテムリスト")]
    public List<Sprite> possibleItems; // 全ての注文可能なアイテムのリスト

    [Tooltip("生成する顧客プレハブのリスト")]
    public List<Customer> customerPrefabs; // 生成可能な顧客プレハブのリスト

    [Header("スポーンエリア")]
    [Tooltip("顧客スポーンエリアの左上角")]
    public Transform spawnAreaTopLeft; // スポーン範囲の左上座標

    [Tooltip("顧客スポーンエリアの右下角")]
    public Transform spawnAreaBottomRight; // スポーン範囲の右下座標

    private Customer currentCustomer; // 現在表示されている顧客オブジェクト

    public TimeGauge timeGauge;  // TimeGaugeへの参照

    private float volumeScale = 1.0f;
    public AudioSource audioSource;
    public AudioClip audioClip;


    void Start()
    {
      
    }

    /// <summary>
    /// ゲーム開始時に注文を初期化し、最初の顧客を生成する。
    /// </summary>
    public void InitializeOrders()
    {
        SpawnCustomer(); // ゲーム開始時に顧客を生成
    }

    /// <summary>
    /// 指定されたスポーンエリア内のランダムな位置に新しい顧客を生成する。
    /// </summary>
    private void SpawnCustomer()
    {
        RemoveExistingCustomer();
        Vector3 spawnPosition = GetRandomSpawnPosition();
        Customer newCustomer = CreateNewCustomer(spawnPosition);

        SetupCustomerOrder(newCustomer);
        AssignOrderManagerToTray(newCustomer);

        currentCustomer = newCustomer;
    }

    /// <summary>
    /// 現在表示されている顧客がいれば削除する。
    /// </summary>
    private void RemoveExistingCustomer()
    {
        if (currentCustomer != null)
        {
            Destroy(currentCustomer.gameObject);
        }
    }

    /// <summary>
    /// スポーンエリア内のランダムな位置を生成する。
    /// </summary>
    /// <returns>スポーンエリア内のランダムなVector3座標。</returns>
    private Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(spawnAreaTopLeft.position.x, spawnAreaBottomRight.position.x);
        float y = Random.Range(spawnAreaBottomRight.position.y, spawnAreaTopLeft.position.y);

        return new Vector3(x, y, 0);
    }

    /// <summary>
    /// 指定された位置に新しい顧客を生成する。
    /// </summary>
    /// <param name="position">顧客をスポーンする位置。</param>
    /// <returns>生成されたCustomerオブジェクト。</returns>
    private Customer CreateNewCustomer(Vector3 position)
    {
        Customer customerPrefab = GetRandomCustomerPrefab();
        return Instantiate(customerPrefab, position, Quaternion.identity);
    }

    /// <summary>
    /// 利用可能なリストからランダムな顧客プレハブを選択する。
    /// </summary>
    /// <returns>ランダムなCustomerプレハブ。</returns>
    private Customer GetRandomCustomerPrefab()
    {
        int randomIndex = Random.Range(0, customerPrefabs.Count);
        return customerPrefabs[randomIndex];
    }

    /// <summary>
    /// 顧客の注文をランダムに生成し、表示を初期化する。
    /// </summary>
    /// <param name="customer">設定する顧客。</param>
    private void SetupCustomerOrder(Customer customer)
    {
        List<Sprite> order = GenerateRandomOrder();
        customer.SetupCustomer(customer.customerImage.sprite, order);
    }

    /// <summary>
    /// このOrderManagerインスタンスを顧客のTrayDropZoneに設定する。
    /// </summary>
    /// <param name="customer">設定する顧客オブジェクト。</param>
    private void AssignOrderManagerToTray(Customer customer)
    {
        TrayDropZone trayDropZone = customer.GetComponentInChildren<TrayDropZone>();
        if (trayDropZone != null)
        {
            trayDropZone.SetOrderManager(this);
        }
    }

    /// <summary>
    /// 顧客のためにランダムな注文を生成する。
    /// </summary>
    /// <returns>顧客の注文を表すSpriteのリスト。</returns>
    private List<Sprite> GenerateRandomOrder()
    {
        List<Sprite> order = new List<Sprite>();

        Sprite randomItem = possibleItems[Random.Range(0, possibleItems.Count)];
        order.Add(randomItem);

        return order;
    }

    /// <summary>
    /// 指定されたアイテムが現在の顧客の注文の一部かどうかをチェックする。
    /// 注文が完了した場合、新しい顧客を生成する。
    /// </summary>
    /// <param name="itemSprite">チェックするアイテムのSprite。</param>
    /// <returns>アイテムが注文の一部であればtrue、それ以外はfalse。</returns>
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
                // 正しい注文が完了した場合、タイムゲージを1秒回復
                timeGauge.RecoverTime(-1f);
                audioSource.PlayOneShot(audioClip, volumeScale);
            }
            return true;
        }
        return false;
    }
}
