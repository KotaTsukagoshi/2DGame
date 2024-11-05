using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class OrderManager : MonoBehaviour
{
    public List<Sprite> itemSprites; //�����Ɏg����A�C�e���̃��X�g
    public List<Image> orderImages;�@//UI��̒����A�C�e���\���p��Image���X�g
    private List<Sprite> currentOrder = new List<Sprite>();


    void Start()
    {
        GenerateOrder();
    }

    public void GenerateOrder()
    {
        currentOrder.Clear();
        // itemSprites��null�܂��͋�łȂ����m�F
        if (itemSprites == null || itemSprites.Count == 0)
        {
            Debug.LogError("itemSprites���ݒ肳��Ă��Ȃ����A��ł��B");
            return;
        }

        for (int i = 0; i < orderImages.Count; i++)
        {
            if (i < itemSprites.Count) // �A�C�e�����\���ɂ��邩�m�F
            {
                //�����_���ɃA�C�e����I��Œ������X�g�ɒǉ�
                Sprite randomItem = itemSprites[Random.Range(0, itemSprites.Count)];
                currentOrder.Add(randomItem);
                orderImages[i].sprite = randomItem;
                orderImages[i].enabled = true;
            }
            else
            {
                Debug.LogWarning("�A�C�e�����s�����Ă܂��B");
            }
        }
    }

    public bool CheckOrder(Sprite deliveredItem)
    {
        return currentOrder.Contains(deliveredItem);
    }
}
