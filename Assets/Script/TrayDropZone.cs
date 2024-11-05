using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TraiDropZone : MonoBehaviour, IDropHandler
{
    public OrderManager orderManager;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;
        if (droppedItem != null)
        {
            //�h���b�v���ꂽ�A�C�e���̃X�v���C�g���擾
            Image itemImage = droppedItem.GetComponent<Image>();
            if (itemImage != null)
            {
                //�������X�g�ƈ�v���邩�m�F
                if (orderManager.CheckOrder(itemImage.sprite))
                {
                    Debug.Log("�[�i�����I");
                    Destroy(droppedItem);//�������[�i���ꂽ�̂ŃA�C�e���폜
                }
            }
            else
            {
                Debug.Log("�Ԉ�����A�C�e�����[�i����܂����I");
            }
        }
    }
}