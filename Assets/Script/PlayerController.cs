using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public OrderManager orderManager;

    public void CheckOrder(string selectedItem)
    {
        string currentOrder = orderManager.GetCurrentOrder();
        if (selectedItem == currentOrder)
        {
            Debug.Log("正しい注文です！");
            orderManager.GenerateOrder();
        }
        else
        {
            Debug.Log("間違った注文です！");
        }
    }
}