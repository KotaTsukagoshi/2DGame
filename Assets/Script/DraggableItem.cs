using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // RectTransform: UI�A�C�e���̈ʒu��T�C�Y�̒��������邽�߂Ɏg�p
    private RectTransform rectTransform;
    // CanvasGroup: �h���b�O���ɑ��̃I�u�W�F�N�g�Ƃ̏Փ˔�����R���g���[�����邽�߂Ɏg�p
    private CanvasGroup canvasGroup;
    // startPosition: �h���b�O���I�������ۂɃA�C�e�������̈ʒu�ɖ߂����߂̏����ʒu��ۑ�
    private Vector3 startPosition;

    void Awake()
    {
        // RectTransform �� CanvasGroup �R���|�[�l���g�̎Q�Ƃ��擾
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        // �A�C�e���̏����ʒu���L�^���Ă���
        startPosition = rectTransform.position;
    }

    // �h���b�O���J�n���ꂽ�ۂɌĂ΂�郁�\�b�h
    public void OnBeginDrag(PointerEventData eventData)
    {
        // �h���b�O���͑��̃I�u�W�F�N�g�Ƃ̏Փ˔���𖳌�������
        // �i����ɂ��A���� UI �v�f�����̃A�C�e���̔w��Ŏ󂯎���悤�ɂȂ�j
        canvasGroup.blocksRaycasts = false;
    }

    // �h���b�O���ɌĂ΂ꑱ���郁�\�b�h
    public void OnDrag(PointerEventData eventData)
    {
        // �h���b�O�ɉ����ăA�C�e���̈ʒu���X�V
        // anchoredPosition �͐e�I�u�W�F�N�g�̍��W�n�ɂ����鑊�ΓI�Ȉʒu
        rectTransform.anchoredPosition += eventData.delta;
    }

    // �h���b�O���I�������ۂɌĂ΂�郁�\�b�h
    public void OnEndDrag(PointerEventData eventData)
    {
        // �h���b�O���I���������߁A���̃I�u�W�F�N�g�Ƃ̏Փ˔�����ĂїL����
        canvasGroup.blocksRaycasts = true;

        // �A�C�e�������̈ʒu�ɖ߂�
        // ����ɂ��A�h���b�v�����s�����ꍇ�ɃA�C�e���������ʒu�Ƀ��Z�b�g�����
        rectTransform.position = startPosition;
    }
}
