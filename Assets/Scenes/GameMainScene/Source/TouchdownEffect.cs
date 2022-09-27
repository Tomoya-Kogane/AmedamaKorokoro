using UnityEngine;

/// <summary>
/// 着地エフェクトを管理するクラス
/// </summary>
public class TouchdownEffect : MonoBehaviour
{
    // ボールオブジェクト用の変数
    GameObject ball;

    // 初期処理
    void Start()
    {
        // ボールのオブジェクトを取得
        this.ball = GameObject.Find("PlayerBall");

        // ボールの下にエフェクトが発生する様に設定
        transform.position = new Vector3(this.ball.transform.position.x, this.ball.transform.position.y - 0.45f, 0.0f);
    }
}
