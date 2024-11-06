using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OrderManager : MonoBehaviour
{
    // itemSprites: �Q�[�����ŗ��p�\�ȑS�ẴA�C�e���̃X�v���C�g���X�g
    public List<Sprite> itemSprites;

    // orderImages: �����A�C�e����\�����邽�߂�UI Image���X�g
    public List<Image> orderImages;

    // currentOrder: ���݂̒����Ƃ��ĕK�v�ȃA�C�e���̃X�v���C�g���X�g
    private List<Sprite> currentOrder = new List<Sprite>();

    void Start()
    {
        // �Q�[���J�n���ɍŏ��̒����𐶐�
        GenerateOrder();
    }

    // ���݂̒����𐶐����AUI�Ƀ����_���ɃA�C�e����\�����郁�\�b�h
    public void GenerateOrder()
    {
        // �O�̒������N���A���A�V���������A�C�e�����X�g���쐬
        currentOrder.Clear();

        // orderImages�̐����������_���ȃA�C�e����I��ŁA�������X�g��UI�ɐݒ�
        for (int i = 0; i < orderImages.Count; i++)
        {
            // itemSprites���烉���_���ɃA�C�e����I�����AcurrentOrder��orderImages�ɒǉ�
            Sprite randomItem = itemSprites[Random.Range(0, itemSprites.Count)];
            currentOrder.Add(randomItem);
            orderImages[i].sprite = randomItem;
            orderImages[i].enabled = true; // UI�v�f��\������
        }
    }

    // �v���C���[���g���[�ɏ悹���A�C�e�����������X�g�ɂ��邩���m�F���郁�\�b�h
    // itemSprite: �v���C���[���[�i���悤�Ƃ����A�C�e���̃X�v���C�g
    // �߂�l: �������X�g�Ɋ܂܂�Ă����true�A�܂܂�Ă��Ȃ����false
    public bool CheckOrder(Sprite itemSprite)
    {
        return currentOrder.Contains(itemSprite);
    }
}
