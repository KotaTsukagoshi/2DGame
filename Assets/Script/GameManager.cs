using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject EndPanel;
    public Button startButton;
    public TextMeshProUGUI countdownText;
    public GameObject gameElements;

    public TMP_FontAsset newFont;  // 新しいフォントをインスペクターから設定
    public float newFontSize = 50f;  // 新しいフォントサイズを設定

    public GameObject quitPanel;  // Quit パネルの参照
    public Button quitButton;     // Quit ボタンの参照
    public Button retryButton;    // Retry ボタンの参照
    public TextMeshProUGUI finishText; // 終了時に表示するテキスト

    public OrderManager orderManager; // OrderManagerへの参照
    public TimeGauge timeGauge;  // TimeGaugeの参照

    private bool isGameFinished = false;

    void Start()
    {
        gameElements.SetActive(false);
        startPanel.SetActive(true);
        finishText.gameObject.SetActive(false);  // 最初は非表示

        // フォントを変更
        if (newFont != null)
        {
            countdownText.font = newFont;
            finishText.font = newFont;
        }
        // フォントサイズを変更
        countdownText.fontSize = newFontSize;
        finishText.fontSize = newFontSize;

        startButton.onClick.AddListener(StartGameCountdown);
        quitButton.onClick.AddListener(QuitGame);
        retryButton.onClick.AddListener(RestartGame);  // Retryボタンの処理を追加

        // TimeGaugeの時間切れイベントに登録
        if (timeGauge != null)
        {
            timeGauge.OnTimeUp += EndGame;
        }

        Time.timeScale = 1;  // ゲーム開始時は通常速度
    }

    public void StartGameCountdown()
    {
        Debug.Log("Start Button Pressed"); // ログ追加
        startButton.gameObject.SetActive(false);
        EndPanel.gameObject.SetActive(false);
        quitPanel.SetActive(false);  // スタートボタンが押されたらQuitパネルを非表示にする
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        // 最初に「Ready?」を表示
        countdownText.text = "Ready?";
        yield return new WaitForSeconds(1f); // 1秒待機

        // 「GO!」を表示
        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f); // 1秒待機してから次の処理

        // カウントダウン終了後の処理
        countdownText.gameObject.SetActive(false);  // カウントダウンのテキストを非表示
        gameElements.SetActive(true);  // ゲーム要素を表示
        startPanel.SetActive(false);  // スタートパネルを非表示

        // OrderManagerの初期化メソッドを呼び出してゲームをスタート
        if (orderManager != null)
        {
            orderManager.InitializeOrders();
        }
        // TimeGaugeのリセット
        if (timeGauge != null)
        {
            timeGauge.ResetGauge();
        }
    }

    // ゲーム終了時に呼び出されるメソッド
    public void EndGame()
    {
        if (!isGameFinished)
        {
            isGameFinished = true;
            Time.timeScale = 0;  // ゲームの進行を停止
            gameElements.SetActive(false);  // ゲーム要素を非表示

            EndPanel.SetActive(true);

            finishText.text = "Finish!";    // 終了メッセージを表示
            finishText.gameObject.SetActive(true);

            // 1秒待機後にボタンを表示
            StartCoroutine(ShowEndButtonsAfterDelay());
        }
    }

    private IEnumerator ShowEndButtonsAfterDelay()
    {
        yield return new WaitForSecondsRealtime(1f); // ゲーム停止中でも実時間で1秒待機
        quitPanel.SetActive(true); // QuitとRetryボタンを表示
    }

    // Retryボタンの処理
    public void RestartGame()
    {
        //Debug.Log("Retry Button Pressed");
        //isGameFinished = false;
        //finishText.gameObject.SetActive(false);  // 終了メッセージを非表示
        //quitPanel.SetActive(false);  // Quitパネルを非表示
        //Time.timeScale = 1;  // ゲームの進行を再開
        // OrderManagerの初期化
        if (orderManager != null)
        {
            orderManager.InitializeOrders();
        }

        // TimeGaugeのリセット
        if (timeGauge != null)
        {
            timeGauge.ResetGauge();
        }
        StartGameCountdown();  // ゲームを再スタート
    }

    // Quitボタンの処理
    private void QuitGame()
    {
        Debug.Log("Quit Button Pressed");
        Application.Quit();  // ゲームを終了
    }
}
