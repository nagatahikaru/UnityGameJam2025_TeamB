using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BackGroundMover : MonoBehaviour
{
    private const float  k_maxLength = 1f;       // テクスチャのオフセットがリピートする最大値
    private const string k_propName = "_MainTex";// マテリアルのプロパティ名

    // 背景のオフセット速度
    [SerializeField]
    private Vector2 m_offsetSpeed;
    //背景の設定
    private Material m_copiedMaterial;

    private void Start()
    {
        // Imageコンポーネントを取得して、マテリアルをコピーする
        var image = GetComponent<Image>();
        // Imageコンポーネントが存在しない場合は例外を投げる
        m_copiedMaterial = new Material(image.material);
        // マテリアルのプロパティを設定する
        image.material = m_copiedMaterial;

        // マテリアルがnullだったら例外が出ます。
        Assert.IsNotNull(m_copiedMaterial);
    }

    private void Update()
    {
        // 時間のスケールが0の場合は何もしない
        if (Time.timeScale == 0f)
        {
            return;
        }

        // xとyの値が0 ～ 1でリピートするようにする
        var x = Mathf.Repeat(Time.time * m_offsetSpeed.x, k_maxLength);
        var y = Mathf.Repeat(Time.time * m_offsetSpeed.y, k_maxLength);
        var offset = new Vector2(x, y);
        // マテリアルのオフセットを設定する
        m_copiedMaterial.SetTextureOffset(k_propName, offset);
    }

    private void OnDestroy()
    {
        // ゲームオブジェクト破壊時にマテリアルのコピーも消しておく
        if (m_copiedMaterial != null)
        {
            Destroy(m_copiedMaterial);
        }
        // m_copiedMaterialをnullにしておく
        m_copiedMaterial = null;
    }
}