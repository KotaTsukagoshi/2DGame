using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public GameObject startPanel;
    public Button startButton;
    public TextMeshProUGUI countdownText;
    public GameObject gameElements;

    public TMP_FontAsset newFont;  // 新しいフォントをインスペクターから設定
    public float newFontSize = 50f;  // 新しいフォントサイズを設定

    public GameObject quitPanel;  // Quit パネルの参照

    public OrderManager orderManager; // OrderManagerへの参照


    void Start()
    {
        gameElements.SetActive(false);
        startPanel.SetActive(true);
        quitPanel.SetActive(true);  // Quit パネルを最初は表示

        // フォントを変更
        if (newFont != null)
        {
            countdownText.font = newFont;
        }
        // フォントサイズを変更
        countdownText.fontSize = newFontSize;

        startButton.onClick.AddListener(StartGameCountdown);
    }

    public void StartGameCountdown()
    {
        Debug.Log("Start Button Pressed"); // ログ追加
        startButton.gameObject.SetActive(false);
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
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
    Application.Quit();
#endif
    }
}
