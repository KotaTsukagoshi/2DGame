using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;
using TMPro;


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
    public GameManager gameManager;  // GameManagerへの参照
    public TextMeshProUGUI ScoreText; // TextをTextMeshProUGUIに変更

    void Start()
    {
        if (gameManager == null)
        {
            Debug.LogError("GameManager が設定されていません。Inspectorで設定してください。");
        }
    }

    /// ゲーム開始時に注文を初期化し、最初の顧客を生成する。
    public void InitializeOrders()
    {
        SpawnCustomer(); // ゲーム開始時に顧客を生成
    }

    /// 指定されたスポーンエリア内のランダムな位置に新しい顧客を生成する。
    private void SpawnCustomer()
    {
        RemoveExistingCustomer();
        Vector3 spawnPosition = GetRandomSpawnPosition();
        Customer newCustomer = CreateNewCustomer(spawnPosition);

        SetupCustomerOrder(newCustomer);
        AssignOrderManagerToTray(newCustomer);

        currentCustomer = newCustomer;
    }

    /// 現在表示されている顧客がいれば削除する。
    private void RemoveExistingCustomer()
    {
        if (currentCustomer != null)
        {
            Destroy(currentCustomer.gameObject);
        }
    }

    /// スポーンエリア内のランダムな位置を生成する。
    /// <returns>スポーンエリア内のランダムなVector3座標。</returns>
    private Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(spawnAreaTopLeft.position.x, spawnAreaBottomRight.position.x);
        float y = Random.Range(spawnAreaBottomRight.position.y, spawnAreaTopLeft.position.y);

        return new Vector3(x, y, 0);
    }

    /// 指定された位置に新しい顧客を生成する。
    /// <param name="position">顧客をスポーンする位置。</param>
    /// <returns>生成されたCustomerオブジェクト。</returns>
    private Customer CreateNewCustomer(Vector3 position)
    {
        Customer customerPrefab = GetRandomCustomerPrefab();
        return Instantiate(customerPrefab, position, Quaternion.identity);
    }

    /// 利用可能なリストからランダムな顧客プレハブを選択する。
    /// <returns>ランダムなCustomerプレハブ。</returns>
    private Customer GetRandomCustomerPrefab()
    {
        int randomIndex = Random.Range(0, customerPrefabs.Count);
        return customerPrefabs[randomIndex];
    }

    /// 顧客の注文をランダムに生成し、表示を初期化する。
    /// <param name="customer">設定する顧客。</param>
    private void SetupCustomerOrder(Customer customer)
    {
        List<Sprite> order = GenerateRandomOrder();
        customer.SetupCustomer(customer.customerImage.sprite, order);
    }

    /// このOrderManagerインスタンスを顧客のTrayDropZoneに設定する。
    /// <param name="customer">設定する顧客オブジェクト。</param>
    private void AssignOrderManagerToTray(Customer customer)
    {
        TrayDropZone trayDropZone = customer.GetComponentInChildren<TrayDropZone>();
        if (trayDropZone != null)
        {
            trayDropZone.SetOrderManager(this);
        }
    }

    /// 顧客のためにランダムな注文を生成する。
    /// <returns>顧客の注文を表すSpriteのリスト。</returns>
    private List<Sprite> GenerateRandomOrder()
    {
        List<Sprite> order = new List<Sprite>();

        Sprite randomItem = possibleItems[Random.Range(0, possibleItems.Count)];
        order.Add(randomItem);

        return order;
    }

    /// 指定されたアイテムが現在の顧客の注文の一部かどうかをチェックする。
    /// 注文が完了した場合、新しい顧客を生成する。
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
    // オーダーのチェックを行うメソッド
    public void GetScore(Sprite itemSprite)
    {
        bool isOrderCorrect = CheckOrder(itemSprite); // 正解かどうかの判定

        if (isOrderCorrect)
        {
            Debug.Log("正解の注文です。スコアを加算します。");
            gameManager.AddScore(10);  // 正解のとき、10点を加算
        }
        else
        {
            Debug.Log("注文が正しくありません。");
        }
    }

}
