using UnityEngine;
using UnityEngine.UI;

public class BackGroundMover : MonoBehaviour
{
    // 背景のテクスチャがリピートする範囲の最大値（0 ～ 1）
    private const float k_maxLength = 1f;

    // マテリアルの "_MainTex" プロパティ名（デフォルトのテクスチャ）
    private const string k_propName = "_MainTex";

    // 背景のオフセット速度 (xとyの値)
    [SerializeField]
    private Vector2 m_offsetSpeed;

    // 背景のマテリアル（Imageコンポーネントにアタッチされたマテリアル）
    private Material m_material;

    // 初期化処理
    private void Start()
    {
        // Imageコンポーネントからマテリアルを取得
        if (TryGetComponent<Image>(out Image image))
        {
            m_material = image.material;
        }
        else
        {
            Debug.LogError("Image component not found on the GameObject.");
        }
    }

    // 更新処理（毎フレーム呼ばれる）
    private void Update()
    {
        // マテリアルが設定されている場合、背景のオフセットを更新
        if (m_material != null)
        {
            // x と y のオフセット値を時間に基づいて計算し、リピートさせる
            var offset = new Vector2(
                Mathf.Repeat(Time.time * m_offsetSpeed.x, k_maxLength),
                Mathf.Repeat(Time.time * m_offsetSpeed.y, k_maxLength)
            );

            // マテリアルのテクスチャオフセットを設定
            m_material.SetTextureOffset(k_propName, offset);
        }
    }

    // オブジェクトが破棄される際に呼ばれる（背景のオフセットをリセット）
    private void OnDestroy()
    {
        // マテリアルが設定されている場合、オフセットを元に戻す
        if (m_material != null)
        {
            m_material.SetTextureOffset(k_propName, Vector2.zero);
        }
    }
}
