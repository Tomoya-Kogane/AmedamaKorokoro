using UnityEngine;

/// <summary>
/// 移動エフェクトを管理するクラス
/// </summary>

public class WalkEffect : MonoBehaviour
{
    // ボールオブジェクト用の変数
    GameObject ball;
    PlayerBall playerBall;

    // パーティクル操作用の変数
    ParticleSystem.MainModule particleMain;

    // 初期処理
    void Start()
    {
        // ボールのオブジェクトを取得
        this.ball = GameObject.Find("PlayerBall");
        this.playerBall = this.ball.GetComponent<PlayerBall>();

        // パーティクルのメインモジュールを取得
        this.particleMain = gameObject.GetComponent<ParticleSystem>().main;
    }

    // 更新処理
    void Update()
    {
        // ボールのエフェクトステータスに応じた処理
        if (this.playerBall.Effect != 2)
        {
            // ループを停止
            this.particleMain.loop = false;
        }
        else
        {
            // 移動フェクトがボール下から発生する様に位置更新
            transform.position = new Vector3(this.ball.transform.position.x, this.ball.transform.position.y - 0.45f, 0.0f);
            // ループを開始
            this.particleMain.loop = true;
        }
    }
}
