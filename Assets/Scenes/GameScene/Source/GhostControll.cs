using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostControll : MonoBehaviour
{
    // 幽霊操作用の変数
    Rigidbody2D rigid2D;
    // メインカメラの座標取得用の変数
    GameObject mainCamera;

    // 移動速度
    const float WALK_FORCE = 8.0f;
    // 移動速度の制限値
    const float MAX_WALKSPEED = 4.0f;
    // 移動向き（初期値右向き）
    int movedir = 1;

    // ステージ状態確認用の変数
    GameDirector gameDirector;

    // スプライトレンダラー操作用の変数
    SpriteRenderer spriteRenderer;
    Color color;

    // シェーダ用の変数
    public Material material1;
    public Material material2;

    // カメラエフェクト確認用の変数
    CameraControll cameraControll;

    // オブジェクト生存時間管理用の変数
    const float LIVE_TIME = 10.0f;
    float deltaTime = 0.0f;

    // 初期処理
    void Start()
    {
        // 幽霊操作用コンポーネントの取得
        this.rigid2D = GetComponent<Rigidbody2D>();

        // メインカメラのオブジェクトの取得
        this.mainCamera = GameObject.Find("Main Camera");

        // ステージ管理コンポーネントを取得
        this.gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();

        // スプライトレンダラーの取得
        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        this.color = this.spriteRenderer.color;

        // シェーダの設定（デフォルトシェーダ）
        this.spriteRenderer.material = this.material1;

        // カメラコントローラーコンポーネントを取得
        this.cameraControll = GameObject.Find("Main Camera").GetComponent<CameraControll>();

        // カメラのエフェクトに応じたシェーダを設定
        switch (this.cameraControll.GetEffectStatus())
        {
            // 初期エフェクトの場合、デフォルトシェーダとアルファ値(ZERO)を設定
            case 1:
                // シェーダの設定（デフォルトシェーダ）
                this.spriteRenderer.material = this.material1;
                // アルファ値の設定（ZERO）
                this.color.a = 0.0f;
                this.spriteRenderer.color = this.color;
                break;
            // 左半分をグレースケールの場合、アクセサリエフェクトとアルファ値(1)を設定
            case 2:
                // シェーダの設定（アクセサリエフェクト）
                this.spriteRenderer.material = this.material2;
                // アルファ値の設定（1）
                this.color.a = 1.0f;
                this.spriteRenderer.color = this.color;
                break;
            // 全体をグレースケールの場合、デフォルトシェーダとアルファ値(1)を設定
            case 3:
                // シェーダの設定（デフォルトシェーダ）
                this.spriteRenderer.material = this.material1;
                // アルファ値の設定（1）
                this.color.a = 1.0f;
                this.spriteRenderer.color = this.color;
                break;
            default:
                break;
        }

        // 移動方向と画像の向きを設定
        Vector3 scale = transform.localScale;
        if (transform.position.x >= this.mainCamera.transform.position.x)
        {
            this.movedir = -1;
            scale.x = 1;
        }
        else
        {
            this.movedir = 1;
            scale.x = -1;
        }
        transform.localScale = scale;
    }

    // 更新処理
    void Update()
    {
        // ステージ状態が進行中の場合、処理実施
        if (this.gameDirector.GetStageStatus() == 0)
        {
            // 移動処理
            Move();
        }

        // 一定時間経過したら、オブジェクトを削除
        if (this.cameraControll.GetEffectStatus() == 2 || this.cameraControll.GetEffectStatus() == 3)
        {
            this.deltaTime += Time.deltaTime;
            // ほぼ透過されたら、オブジェクトを破棄
            if (this.deltaTime >= LIVE_TIME)
            {
                Destroy(gameObject);
            }
        }
    }
    // 移動処理
    private void Move()
    {
        // 移動ベクトル用の変数
        Vector3 moveForce = new Vector3(0.0f, 0.0f, 0.0f);

        // 現在の移動速度を取得（Ｘ軸とＹ軸）
        float ballSpeedX = Mathf.Abs(this.rigid2D.velocity.x);

        // 移動速度の設定（Ｘ軸）
        if (ballSpeedX < MAX_WALKSPEED)
        {
            moveForce = new Vector3(this.movedir * WALK_FORCE, moveForce.y, moveForce.z);
        }

        // 移動ベクトルの適用
        this.rigid2D.AddForce(moveForce);
    }
}
