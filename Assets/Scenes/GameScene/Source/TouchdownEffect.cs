using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchdownEffect : MonoBehaviour
{
    // ボールオブジェクト用の変数
    GameObject ball;
    PlayerBall playerBall;

    // 初期処理
    void Start()
    {
        // ボールのオブジェクトを取得
        this.ball = GameObject.Find("PlayerBall");
        this.playerBall = this.ball.GetComponent<PlayerBall>();

        // ボールの下にエフェクトが発生する様に設定
        transform.position = new Vector3(this.ball.transform.position.x, this.ball.transform.position.y - 0.45f, 0.0f);
    }

    // 更新処理
    //void Update()
    //{
    //    // 未処理
    //}
}
