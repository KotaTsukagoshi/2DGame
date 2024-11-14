using UnityEngine;
using UnityEngine.UI;

public class TimeGauge : MonoBehaviour
{
    public Image progressBar;  // 時間ゲージの進行部分
    public float maxTime = 10f; // ゲージが満タンになるまでの時間

    private float currentTime = 0f;

    void Start()
    {
        // ゲージが空からスタートするように設定
        if (progressBar != null)
        {
            progressBar.fillAmount = 0f;
        }
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
        else if (progressBar == null)
        {
            Debug.LogError("ProgressBar is not assigned!");
        }
    }

}
