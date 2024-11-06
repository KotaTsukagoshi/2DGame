using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // RectTransform: UIアイテムの位置やサイズの調整をするために使用
    private RectTransform rectTransform;
    // CanvasGroup: ドラッグ中に他のオブジェクトとの衝突判定をコントロールするために使用
    private CanvasGroup canvasGroup;
    // startPosition: ドラッグが終了した際にアイテムを元の位置に戻すための初期位置を保存
    private Vector3 startPosition;

    void Awake()
    {
        // RectTransform と CanvasGroup コンポーネントの参照を取得
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        // アイテムの初期位置を記録しておく
        startPosition = rectTransform.position;
    }

    // ドラッグが開始された際に呼ばれるメソッド
    public void OnBeginDrag(PointerEventData eventData)
    {
        // ドラッグ中は他のオブジェクトとの衝突判定を無効化する
        // （これにより、他の UI 要素がこのアイテムの背後で受け取れるようになる）
        canvasGroup.blocksRaycasts = false;
    }

    // ドラッグ中に呼ばれ続けるメソッド
    public void OnDrag(PointerEventData eventData)
    {
        // ドラッグに応じてアイテムの位置を更新
        // anchoredPosition は親オブジェクトの座標系における相対的な位置
        rectTransform.anchoredPosition += eventData.delta;
    }

    // ドラッグが終了した際に呼ばれるメソッド
    public void OnEndDrag(PointerEventData eventData)
    {
        // ドラッグが終了したため、他のオブジェクトとの衝突判定を再び有効化
        canvasGroup.blocksRaycasts = true;

        // アイテムを元の位置に戻す
        // これにより、ドロップが失敗した場合にアイテムが初期位置にリセットされる
        rectTransform.position = startPosition;
    }
}
