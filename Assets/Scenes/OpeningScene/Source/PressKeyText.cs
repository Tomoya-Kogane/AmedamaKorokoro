using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// PressKeyテキストの挙動を管理するクラス
/// </summary>
public class PressKeyText : MonoBehaviour
{
    // テキストの透過操作用
    TextMeshProUGUI textMesh;
    Color textColor;

    // テキスト点灯までの待機時間
    const float START_LIGHTING = 3.0f;
    private float _countTime = 0.0f;

    // 初期処理
    void Start()
    {
        // テキストのコンポーネント取得
        this.textMesh = gameObject.GetComponent<TextMeshProUGUI>();
        this.textColor = this.textMesh.color;
        this.textMesh.color = new Color(this.textColor.r, this.textColor.g, this.textColor.b, 0.0f);
    }

    // 更新処理
    void Update()
    {
        // 待機時間を超えたら、テキストの点灯を実施
        _countTime += Time.deltaTime;
        if (_countTime > START_LIGHTING)
        {
            float cycle = Mathf.Cos(Time.time);
            this.textMesh.color = new Color(this.textColor.r, this.textColor.g, this.textColor.b, Mathf.Abs(cycle));
        }
    }
}
