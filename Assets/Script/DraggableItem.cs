using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    // ゲーム開始時の初期位置を保持
    private Vector3 initialStartPosition;
    // 現在の初期位置を追跡
    private Vector3 startPosition;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        // 初期位置を保存（Awakeでの保存をやめる）
        SetInitialPosition();
    }

    // 初期位置を保存するメソッド
    private void SetInitialPosition()
    {
        initialStartPosition = rectTransform.position;
        startPosition = initialStartPosition;
        Debug.Log($"[SetInitialPosition] initialStartPosition set to: {initialStartPosition}");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        ResetPosition();
    }

    // アイテムを初期位置にリセットするメソッド
    public void ResetPosition()
    {
        // ゲームのリセット時に初期位置に戻す
        rectTransform.position = initialStartPosition;
        Debug.Log($"[ResetPosition] Reset to initialStartPosition: {initialStartPosition}");
    }

    // ゲームのリセット時に呼び出されるメソッド
    public void ResetItem()
    {
        // 初期位置を再設定
        SetInitialPosition();
        ResetPosition();
    }
}
