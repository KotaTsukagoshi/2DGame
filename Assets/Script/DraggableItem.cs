using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, IPointerDownHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private float volumeScale = 1.0f;

    public AudioSource audioSource;
    public AudioClip audioClip;

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

        // AudioSource の初期化がされていない場合はエラーを防ぐ
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    /// <summary>
    /// ボタンを押し込んだ瞬間の処理。
    /// </summary>
    /// <param name="eventData">クリックイベントデータ。</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        // ボタンを押し込んだ瞬間に音を再生
        if (audioClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(audioClip, volumeScale);
        }
    }

    /// <summary>
    /// クリック時に呼び出される処理。
    /// </summary>
    /// <param name="eventData">クリックイベントデータ。</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        // クリック時に何かを処理する場合
        Debug.Log("アイテムがクリックされました。");
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
