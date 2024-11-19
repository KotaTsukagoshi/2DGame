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
        InitializeComponents();
        SetInitialPosition(); // 初期位置を保存
    }

    /// <summary>
    /// 必要なコンポーネントを初期化する。
    /// </summary>
    private void InitializeComponents()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// 初期位置を保存するメソッド。
    /// </summary>
    private void SetInitialPosition()
    {
        initialStartPosition = rectTransform.position;
        startPosition = initialStartPosition;
        Debug.Log($"[SetInitialPosition] initialStartPosition set to: {initialStartPosition}");
    }

    /// <summary>
    /// ドラッグ開始時の処理。
    /// </summary>
    /// <param name="eventData">ドラッグイベントデータ。</param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        // ドラッグ中にRaycastを無効化
        canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// ドラッグ中の処理。
    /// </summary>
    /// <param name="eventData">ドラッグイベントデータ。</param>
    public void OnDrag(PointerEventData eventData)
    {
        // アイテムの位置を更新
        rectTransform.anchoredPosition += eventData.delta;
    }

    /// <summary>
    /// ドラッグ終了時の処理。
    /// </summary>
    /// <param name="eventData">ドラッグイベントデータ。</param>
    public void OnEndDrag(PointerEventData eventData)
    {
        // ドラッグ終了時にRaycastを有効化
        canvasGroup.blocksRaycasts = true;
        ResetPosition(); // アイテムを元の位置に戻す
    }

    /// <summary>
    /// アイテムを初期位置にリセットするメソッド。
    /// </summary>
    public void ResetPosition()
    {
        rectTransform.position = initialStartPosition;
        Debug.Log($"[ResetPosition] Reset to initialStartPosition: {initialStartPosition}");
    }

    /// <summary>
    /// ゲームのリセット時に呼び出されるメソッド。
    /// </summary>
    public void ResetItem()
    {
        SetInitialPosition(); // 初期位置を再設定
        ResetPosition();      // アイテムを元の位置にリセット
    }
}
