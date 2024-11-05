using UnityEngine;
using UnityEngine.EventSystems;

public class TraiDropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;
        if (droppedItem != null)
        {
            //�A�C�e�����g���[�̈ʒu�ɔz�u
            droppedItem.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            //�[�i����������ǉ�����
            Debug.Log("�[�i�����I");
        }
    }
}