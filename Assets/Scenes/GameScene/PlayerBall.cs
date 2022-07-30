using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤー操作のボールを管理するクラス
/// </summary>
public class PlayerBall : MonoBehaviour
{
    // ボール操作用の変数
    Rigidbody2D rigid2D;
    // ボールの初期位置
    Vector3 startPos;
    // ジャンプ力
    float jumpForce = 680.0f;
    // ジャンプ状態の判定値
    bool flgJump = false;
    // 移動速度
    float walkForce = 20.0f;
    // 移動速度の制限値
    float MaxWalkSpeed = 4.5f;
    // 落下判定の基準値
    float CheckFallOut = -4.5f;
    // ライフ管理用の変数
    int life;
    // 傾きの基準値
    float CheckThreshold = 0.2f;
    
    // 画像切替用の変数
    public SpriteRenderer spriteRenderer;
    // あめ玉の画像
    public Sprite sprite1;
    // 目玉の画像
    public Sprite sprite2;

    // ポストエフェクト変更用の変数
    GameObject mainCamera;

    // 初期処理
    void Start()
    {
        // 物理演算用のボールのコンポーネントを取得
        this.rigid2D = GetComponent<Rigidbody2D>();
        // ボールの初期位置を取得
        this.startPos = transform.position;
        // カメラのオブジェクトを取得
        this.mainCamera = GameObject.Find("Main Camera");
        // ポストエフェクトの初期化
        this.mainCamera.GetComponent<CameraControll>().setEffectStatus(1);
        // ライフの初期値を設定
        this.life = 2;
    }

    // 更新処理
    void Update()
    {
        // 移動処理
        Move();

        // 移動の影響確認
        CheckMovement();

    }

    // 移動処理
    private void Move()
    {
        // 移動ベクトル用の変数
        Vector3 moveForce = new Vector3(0.0f, 0.0f, 0.0f);

        // 向きの設定（矢印キー）
        int walkDir = 0;
        if (Input.GetKey(KeyCode.RightArrow)) walkDir = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) walkDir = -1;

        // 向きの設定（傾き）
        if (Input.acceleration.x > this.CheckThreshold) walkDir = 1;
        if (Input.acceleration.x < -this.CheckThreshold) walkDir = -1;

        // 現在の移動速度を取得（Ｘ軸とＹ軸）
        float ballSpeedX = Mathf.Abs(this.rigid2D.velocity.x);

        // 移動速度の設定（Ｘ軸）
        if (ballSpeedX < this.MaxWalkSpeed)
        {
            moveForce = new Vector3(walkDir * this.walkForce, moveForce.y, moveForce.z);
        }

        // ジャンプの設定（Ｙ軸）
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !flgJump)
        {
            moveForce = new Vector3(moveForce.x, this.jumpForce, moveForce.z);
            flgJump = true;
        }

        // 移動ベクトルの適用
        this.rigid2D.AddForce(moveForce);
    }

    // 移動の影響確認
    private void CheckMovement()
    {
        // 落下判定（画面外）
        if (transform.position.y < CheckFallOut)
        {
            // 落下したら、初期位置に戻る
            transform.position = this.startPos;

            // ライフに応じて、ポストエフェクトを設定
            switch (this.life)
            {
                // 左画面グレースケールのエフェクトを設定
                case 2:
                    this.mainCamera.GetComponent<CameraControll>().setEffectStatus(2);
                    break;
                // 全画面グレースケールのエフェクトを設定
                case 1:
                    this.mainCamera.GetComponent<CameraControll>().setEffectStatus(3);
                    break;
                // ゲームオーバー
                case 0:
                    // 処理未実装
                    break;
                default:
                    break;
            }

            // ライフを１減らす
            this.life--;
            // 目玉テクスチャを設定
            this.spriteRenderer.sprite = this.sprite2;
        }
    }

    // 衝突判定
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 地面と衝突した場合、ジャンプフラグをOFF
        if (collision.gameObject.tag == "Ground")
        {
            this.flgJump = false;
        }
    }
}
