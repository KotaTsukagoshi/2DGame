using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject startPanel;          // ゲーム開始時のパネル
    public GameObject endPanel;            // ゲーム終了時のパネル
    public Button startButton;             // ゲーム開始ボタン
    public TextMeshProUGUI countdownText;  // カウントダウンのテキスト
    public GameObject gameElements;        // ゲーム中の要素（非表示/表示用）

    [Header("Font Settings")]
    public TMP_FontAsset newFont;          // カウントダウンテキスト用のフォント
    public float newFontSize = 50f;        // カウントダウンテキストのフォントサイズ

    [Header("End Game UI")]
    public GameObject quitPanel;           // 終了時のQuitパネル
    public Button quitButton;              // ゲーム終了ボタン
    public Button retryButton;             // リトライボタン
    public TextMeshProUGUI finishText;     // 終了時のメッセージ表示用

    [Header("Game Managers")]
    public OrderManager orderManager;      // OrderManagerへの参照
    public TimeGauge timeGauge;            // TimeGaugeの参照

    private bool isGameFinished = false;   // ゲームが終了したかどうかのフラグ

    void Start()
    {
        InitializeUI();
        InitializeGame();
    }

    /// <summary>
    /// ゲーム開始時のUIとボタン設定の初期化を行うメソッド。
    /// </summary>
    private void InitializeUI()
    {
        // ゲーム要素を非表示、開始パネルを表示
        gameElements.SetActive(false);
        startPanel.SetActive(true);
        finishText.gameObject.SetActive(false);

        // カウントダウンテキストのフォントとサイズを設定
        SetupFontSettings();

        // ボタンのクリックイベントを設定
        startButton.onClick.AddListener(StartGameCountdown);
        quitButton.onClick.AddListener(QuitGame);
        retryButton.onClick.AddListener(RestartGame);
    }

    /// <summary>
    /// フォントの設定を行うメソッド。
    /// </summary>
    private void SetupFontSettings()
    {
        if (newFont != null)
        {
            countdownText.font = newFont;
            finishText.font = newFont;
        }
        countdownText.fontSize = newFontSize;
        finishText.fontSize = newFontSize;
    }

    /// <summary>
    /// ゲームロジックの初期設定を行うメソッド。
    /// </summary>
    private void InitializeGame()
    {
        // タイムゲージの時間切れイベントに登録
        if (timeGauge != null)
        {
            timeGauge.OnTimeUp += EndGame;
        }
        Time.timeScale = 1;  // ゲーム開始時は通常速度
    }

    /// <summary>
    /// ゲーム開始時のカウントダウンを開始するメソッド。
    /// </summary>
    public void StartGameCountdown()
    {
        Debug.Log("Start Button Pressed");
        startButton.gameObject.SetActive(false);
        endPanel.gameObject.SetActive(false);
        quitPanel.SetActive(false);  // スタート時にQuitパネルを非表示

        StartCoroutine(CountdownCoroutine());
    }

    /// <summary>
    /// カウントダウンを実行するコルーチン。
    /// </summary>
    private IEnumerator CountdownCoroutine()
    {
        countdownText.text = "Ready?";
        yield return new WaitForSeconds(1f);

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);

        // カウントダウン終了後の処理
        StartGame();
    }

    /// <summary>
    /// ゲームの開始処理を行うメソッド。
    /// </summary>
    private void StartGame()
    {
        countdownText.gameObject.SetActive(false);
        gameElements.SetActive(true);
        startPanel.SetActive(false);

        if (orderManager != null)
        {
            orderManager.InitializeOrders();
        }

        if (timeGauge != null)
        {
            timeGauge.ResetGauge();
        }
    }

    /// <summary>
    /// ゲーム終了時に呼び出されるメソッド。ゲームの進行を停止する。
    /// </summary>
    public void EndGame()
    {
        if (!isGameFinished)
        {
            isGameFinished = true;
            Time.timeScale = 0;  // ゲームの進行を停止
            gameElements.SetActive(false);

            finishText.text = "Finish!";
            finishText.gameObject.SetActive(true);

            // 1秒後に終了ボタンを表示
            StartCoroutine(ShowEndButtonsAfterDelay());
            endPanel.SetActive(true);


        }
    }

    /// <summary>
    /// ゲーム終了後、1秒待機してから終了ボタンを表示するコルーチン。
    /// </summary>
    private IEnumerator ShowEndButtonsAfterDelay()
    {
        yield return new WaitForSecondsRealtime(1f); // 実時間で1秒待機
        finishText.gameObject.SetActive(false);

    }

    /// <summary>
    /// ゲームをリスタートするメソッド。Retryボタン用。
    /// </summary>
    public void RestartGame()
    {
        Debug.Log("Retry Button Pressed");
        ResetGame();
        StartGameCountdown();
    }

    /// <summary>
    /// ゲームのリセット処理を行うメソッド。
    /// </summary>
    private void ResetGame()
    {
        quitPanel.SetActive(false);

        Time.timeScale = 1;  // ゲームの進行を再開
        gameElements.SetActive(false);
        startPanel.SetActive(true);

        // カウントダウンテキストをリセット
        countdownText.gameObject.SetActive(true);  // カウントダウンテキストを表示

        if (orderManager != null)
        {
            orderManager.InitializeOrders();
        }

        if (timeGauge != null)
        {
            timeGauge.ResetGauge();
        }
    }

    /// <summary>
    /// ゲームを終了するメソッド。Quitボタン用。
    /// </summary>
    private void QuitGame()
    {
        Debug.Log("Quit Button Pressed");
        Application.Quit();  // ゲームを終了
    }
}
