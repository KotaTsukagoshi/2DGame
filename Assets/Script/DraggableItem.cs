using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 startPosition;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        // アイテムの初期位置を記録しておく
        startPosition = rectTransform.position;
        Debug.Log($"[Awake] Initial startPosition: {startPosition}");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        Debug.Log($"[OnBeginDrag] Current position: {rectTransform.position}");
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        ResetPosition();
        Debug.Log($"[OnEndDrag] Reset position to startPosition: {startPosition}");
    }

    public void ResetPosition()
    {
        Debug.Log($"[ResetPosition] Resetting to startPosition: {startPosition}");
        rectTransform.position = startPosition;
        Debug.Log($"[ResetPosition] Current rectTransform.position: {rectTransform.position}");
    }

    public void RestoreStartPosition()
    {
        // ゲームが終了したときなどに初期位置を再設定するメソッド
        startPosition = rectTransform.position;
        Debug.Log($"[RestoreStartPosition] Restoring startPosition to: {startPosition}");
    }
}
