using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeGauge : MonoBehaviour
{
    public Image progressBar;  // 時間ゲージの進行部分
    public float maxTime = 3f; // ゲージが満タンになるまでの時間

    private float currentTime = 0f;

    // 時間切れを通知するためのイベント
    public event Action OnTimeUp;

    void Start()
    {
        // ゲージが空からスタートするように設定
        if (progressBar != null)
        {
            progressBar.fillAmount = 0f;
        }
        ResetGauge();  // ゲージをリセットして初期化
    }

    void Update()
    {
        // ゲージが満タンでなければ進行
        if (progressBar != null && currentTime < maxTime)
        {
            currentTime += Time.deltaTime;  // 経過時間を追加
            progressBar.fillAmount = currentTime / maxTime;  // fillAmountを更新

            // デバッグ用メッセージ
            Debug.Log("Current Time: " + currentTime);
            Debug.Log("Fill Amount: " + progressBar.fillAmount);
        }
        else if (currentTime >= maxTime)
        {
            // ゲージが満タンになったら時間切れイベントを呼び出す
            OnTimeUp?.Invoke();
        }
        else if (progressBar == null)
        {
            Debug.LogError("ProgressBar is not assigned!");
        }
    }
    // ゲージをリセットするメソッド
    public void ResetGauge()
    {
        currentTime = 0f;
        if (progressBar != null)
        {
            progressBar.fillAmount = 0f;
        }
    }
}
