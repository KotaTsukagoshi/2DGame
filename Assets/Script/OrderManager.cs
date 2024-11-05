using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class OrderManager : MonoBehaviour
{
    public TextMeshProUGUI orderText; // �������e��\������e�L�X�g
    private List<string> possibleOrders = new List<string> { "�����S", "�o�i�i", "�u�h�E" };
    private string currentOrder;

    void Start()
    {
        GenerateOrder();
    }

    public void GenerateOrder()
    {
        int index = Random.Range(0, possibleOrders.Count);
        currentOrder = possibleOrders[index];
        orderText.text = "����: " + currentOrder;
    }

    public string GetCurrentOrder()
    {
        return currentOrder;
    }
}
