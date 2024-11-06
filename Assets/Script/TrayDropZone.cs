using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrayDropZone : MonoBehaviour, IDropHandler
{
    // orderManager: �����A�C�e�����Ǘ����� OrderManager �ւ̎Q��
    public OrderManager orderManager;

    // �h���b�O����Ă���A�C�e�������̃h���b�v�]�[���Ƀh���b�v���ꂽ�Ƃ��ɌĂ΂�郁�\�b�h
    // eventData: �h���b�v���ꂽ�A�C�e���̃C�x���g�f�[�^
    public void OnDrop(PointerEventData eventData)
    {
        // �h���b�v���ꂽ�Q�[���I�u�W�F�N�g���擾
        GameObject droppedItem = eventData.pointerDrag;

        // �h���b�v���ꂽ�A�C�e�������݂��邩�m�F
        if (droppedItem != null)
        {
            // �h���b�v���ꂽ�A�C�e���� Image �R���|�[�l���g���擾
            Image itemImage = droppedItem.GetComponent<Image>();

            // �A�C�e���� Image �R���|�[�l���g�������Ă��邩�AorderManager ���ݒ肳��Ă��邩���m�F
            if (itemImage != null && orderManager != null)
            {
                // �A�C�e���̃X�v���C�g���������X�g���ɑ��݂��邩�`�F�b�N
                if (orderManager.CheckOrder(itemImage.sprite))
                {
                    Debug.Log("�[�i�����I");
                    Destroy(droppedItem); // �������A�C�e�����[�i���ꂽ�ꍇ�A�A�C�e�����폜
                }
                else
                {
                    Debug.Log("�Ԉ�����A�C�e�����[�i����܂����I");
                }
            }
            else
            {
                Debug.LogWarning("�A�C�e����Image�R���|�[�l���g�����݂��Ȃ����AOrderManager�����ݒ�ł�");
            }
        }
    }
}
