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
    public GameObject title;               // ゲームタイトル表示用
    public TextMeshProUGUI countdownText;  // カウントダウンのテキスト
    public GameObject gameElements;        // ゲーム中の要素（非表示/表示用）

    private DraggableItem[] draggableItems;  // DraggableItemへの参照を保持する配列

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

    [Header("BGM")]
    public AudioClip BGM;
    public AudioSource audioSource;

    [Header("SE")]
    public AudioClip StartSE;
    public AudioSource StartSound;
    public AudioClip EndSE;
    public AudioSource EndSound;

    [Header("Score")]
    public TextMeshProUGUI ScoreText;

    private bool isGameFinished = false;   // ゲームが終了したかどうかのフラグ
    private float volumeScale = 1.0f;
    private int Score = 0;

    void Start()
    {
        InitializeUI();
        InitializeGame();
        CacheDraggableItems();

        // AudioSource の初期化
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = BGM;
        audioSource.loop = true; // ループ再生したい場合
        audioSource.Play();      // ゲーム開始時に再生
    }

    /// ゲーム開始時のUIとボタン設定の初期化を行うメソッド。
    private void InitializeUI()
    {
        // ゲーム要素を非表示、開始パネルを表示
        gameElements.SetActive(false);
        startPanel.SetActive(true);
        finishText.gameObject.SetActive(false);

        // フォント設定を適用
        SetupFontSettings();

        // ボタンのクリックイベントを設定
        SetupButtonEvents();
    }

    /// フォントの設定を行うメソッド。
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

    /// ボタンのクリックイベントを設定するメソッド。
    private void SetupButtonEvents()
    {
        startButton.onClick.AddListener(StartGameCountdown);

        quitButton.onClick.AddListener(QuitGame);
        retryButton.onClick.AddListener(RestartGame);
    }

    /// ゲームロジックの初期設定を行うメソッド。
    private void InitializeGame()
    {
        // タイムゲージの時間切れイベントに登録
        if (timeGauge != null)
        {
            timeGauge.OnTimeUp += EndGame;

        }
        Time.timeScale = 1;  // ゲーム開始時は通常速度
    }

    /// シーン内の全ての DraggableItem をキャッシュするメソッド。
    [System.Obsolete]
    private void CacheDraggableItems()
    {
        draggableItems = FindObjectsOfType<DraggableItem>();
    }

    /// ゲーム開始時のカウントダウンを開始するメソッド。
    public void StartGameCountdown()
    {
        StartSound.PlayOneShot(StartSE, volumeScale);
        Debug.Log("Start Button Pressed");
        HideInitialUI();
        StartCoroutine(CountdownCoroutine());
    }

    /// ゲーム開始時の初期UIを非表示にするメソッド。
    private void HideInitialUI()
    {
        startButton.gameObject.SetActive(false);
        title.gameObject.SetActive(false);
        endPanel.gameObject.SetActive(false);
        quitPanel.SetActive(false);
        // 音声を停止
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

    }

    /// カウントダウンを実行するコルーチン。
    private IEnumerator CountdownCoroutine()
    {
        countdownText.text = "Ready?";
        yield return new WaitForSeconds(1f);

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);


        // カウントダウン終了後の処理
        StartGame();
    }

    /// ゲームの開始処理を行うメソッド。
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

    /// ゲーム終了時に呼び出されるメソッド。ゲームの進行を停止する。
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

    /// ゲーム終了後、1秒待機してから終了ボタンを表示するコルーチン。
    private IEnumerator ShowEndButtonsAfterDelay()
    {
        yield return new WaitForSecondsRealtime(1f); // 実時間で1秒待機
        finishText.gameObject.SetActive(false);
    }

    /// ゲームをリスタートするメソッド。Retryボタン用。
    public void RestartGame()
    {
        Debug.Log("Retry Button Pressed");
        ResetGame();
        StartGameCountdown();

    }

    /// ゲームのリセット処理を行うメソッド。
    private void ResetGame()
    {
        isGameFinished = false;  // ゲーム終了フラグをリセット
        quitPanel.SetActive(false);

        Time.timeScale = 1;  // ゲームの進行を再開
        gameElements.SetActive(false);
        startPanel.SetActive(true);

        // カウントダウンテキストをリセット
        countdownText.gameObject.SetActive(true);

        if (orderManager != null)
        {
            orderManager.InitializeOrders();
        }

        if (timeGauge != null)
        {
            timeGauge.ResetGauge();
        }

        ResetDraggableItems();
    }

    /// DraggableItemのリセットを行うメソッド。
    private void ResetDraggableItems()
    {
        foreach (var draggableItem in draggableItems)
        {
            draggableItem.ResetItem();
        }
    }

    /// ゲームを終了するメソッド。Quitボタン用。
    private void QuitGame()
    {
        Debug.Log("Quit Button Pressed");
        Application.Quit();  // ゲームを終了
    }

    public void AddScore(int points)
    {
        Score += points;
        Debug.Log("Score updated to: " + Score);  // スコアの値を確認
        UpdateScoreText();
    }
    private void UpdateScoreText()
    {
        if (ScoreText != null)
        {
            ScoreText.text = "Score: " + Score.ToString();  // スコアをテキストに反映
        }
    }
}
