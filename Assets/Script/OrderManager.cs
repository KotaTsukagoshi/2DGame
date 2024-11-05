using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OrderManager : MonoBehaviour
{
    public List<Sprite> itemSprites; //�A�C�e���̃��X�g
    public List<Image> orderImages;�@//UI��̒����A�C�e���\���p��Image���X�g
    private List<Sprite> currentOrder = new List<Sprite>();

    void Start()
    {
        GenerateOrder();
    }

    public void GenerateOrder()
    {
        currentOrder.Clear();

        //�\�������A�C�e���������_���ɑI��
        for (int i = 0; i < orderImages.Count; i++)
        {
            //�����_���ɃA�C�e����I��Œ������X�g�ɒǉ�
            Sprite randomItem = itemSprites[Random.Range(0, itemSprites.Count)];
            currentOrder.Add(randomItem);
            orderImages[i].sprite = randomItem;
            orderImages[i].enabled = true;
        }
    }

    public bool CheckOrder(Sprite itemSprite)
    {
        return currentOrder.Contains(itemSprite);
    }
}
