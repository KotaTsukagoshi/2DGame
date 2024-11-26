using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeGauge : MonoBehaviour
{
    [Header("Gauge Settings")]
    public Image progressBar;  // 時間ゲージの進行部分（UIのImageコンポーネント）
    public float maxTime = 3f;  // ゲージが満タンになるまでの時間（秒）

    private float currentTime = 0f;  // 経過時間を保持

    // 時間切れを通知するためのイベント（リスナーを追加できる）
    public event Action OnTimeUp;

    void Start()
    {
        InitializeGauge();  // ゲージの初期化
    }

    void Update()
    {
        UpdateGauge();  // ゲージの更新
    }

    /// ゲージを初期化するメソッド。
    /// ゲージが空からスタートするように設定。
    private void InitializeGauge()
    {
        if (progressBar != null)
        {
            progressBar.fillAmount = 0f;
        }
        else
        {
            Debug.LogError("ProgressBar is not assigned!");
        }

        ResetGauge();
    }

    /// ゲージの進行を管理するメソッド。
    /// 経過時間に応じてゲージを増加させ、時間切れイベントを発生させる。
    private void UpdateGauge()
    {
        // ゲージが有効であり、まだ満タンでない場合のみ進行
        if (progressBar != null && currentTime < maxTime)
        {
            currentTime += Time.deltaTime;  // 経過時間を追加
            UpdateProgressBar();

            // デバッグメッセージ
            //Debug.Log($"Current Time: {currentTime}");
            //Debug.Log($"Fill Amount: {progressBar.fillAmount}");
        }
        else if (currentTime >= maxTime)
        {
            HandleTimeUp();  // ゲージが満タンになった時の処理
        }
    }

    /// ゲージのfillAmountを更新するメソッド。
    private void UpdateProgressBar()
    {
        progressBar.fillAmount = currentTime / maxTime;
    }

    /// ゲージが満タンになった時の処理。
    /// 時間切れイベントを呼び出す。
    private void HandleTimeUp()
    {
        OnTimeUp?.Invoke();  // 時間切れイベントを発生
    }

    /// ゲージをリセットし、再スタートさせるメソッド。
    public void ResetGauge()
    {
        currentTime = 0f;
        if (progressBar != null)
        {
            progressBar.fillAmount = 0f;
        }
    }

    public void RecoverTime(float amount)
    {
        currentTime = Mathf.Min(currentTime + amount, maxTime);  // ゲージが最大を超えないようにする
        UpdateProgressBar();  // ゲージを更新
    }

}
