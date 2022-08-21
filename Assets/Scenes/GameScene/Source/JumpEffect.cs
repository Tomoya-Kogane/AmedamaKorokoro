using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEffect : MonoBehaviour
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
        // 移動フェクトがボール下から発生する様に位置更新
        transform.position = new Vector3(this.ball.transform.position.x, this.ball.transform.position.y - 0.45f, 0.0f);
    }
}
