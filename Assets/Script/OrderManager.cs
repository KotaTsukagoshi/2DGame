using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class OrderManager : MonoBehaviour
{
    public TextMeshProUGUI orderText; // 注文内容を表示するテキスト
    private List<string> possibleOrders = new List<string> { "リンゴ", "バナナ", "ブドウ" };
    private string currentOrder;

    void Start()
    {
        GenerateOrder();
    }

    public void GenerateOrder()
    {
        int index = Random.Range(0, possibleOrders.Count);
        currentOrder = possibleOrders[index];
        orderText.text = "注文: " + currentOrder;
    }

    public string GetCurrentOrder()
    {
        return currentOrder;
    }
}
