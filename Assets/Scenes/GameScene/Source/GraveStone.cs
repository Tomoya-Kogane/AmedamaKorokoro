using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveStone : MonoBehaviour
{
    // メインカメラの変数
    GameObject mainCamera;
    CameraControll cameraControll;

    // ステージ状態確認用の変数
    GameDirector gameDirector;

    // 当たり判定操作用の変数
    Collider2D mayCollider2D;

    // スプライトレンダラー操作用の変数
    SpriteRenderer spriteRenderer;
    Color color;

    // シェーダ用の変数
    public Material material1;
    public Material material2;

    // 初期処理
    void Start()
    {
        // メインカメラの取得
        this.mainCamera = GameObject.Find("Main Camera");
        this.cameraControll = GameObject.Find("Main Camera").GetComponent<CameraControll>();

        // スプライトレンダラーの取得
        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        this.color = this.spriteRenderer.color;

        // シェーダの設定（デフォルトシェーダ）
        this.spriteRenderer.material = this.material1;

        // Colliderの取得
        this.mayCollider2D = gameObject.GetComponent<Collider2D>();
    }

    // 更新処理
    void Update()
    {
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
                // 当たり判定を無効化
                this.mayCollider2D.enabled = false;
                break;
            // 左半分をグレースケールの場合、アクセサリエフェクトとアルファ値(1)を設定
            case 2:
                // シェーダの設定（アクセサリエフェクト）
                this.spriteRenderer.material = this.material2;
                // アルファ値の設定（1）
                this.color.a = 1.0f;
                this.spriteRenderer.color = this.color;
                // 画面右側の場合、当たり判定を有効化
                if (transform.position.x >= this.mainCamera.transform.position.x)
                {
                    this.mayCollider2D.enabled = true;
                }
                // 画面左側の場合、当たり判定を無効化
                else
                {
                    this.mayCollider2D.enabled = false;
                }
                break;
            // 全体をグレースケールの場合、デフォルトシェーダとアルファ値(1)を設定
            case 3:
                // シェーダの設定（デフォルトシェーダ）
                this.spriteRenderer.material = this.material1;
                // アルファ値の設定（1）
                this.color.a = 1.0f;
                this.spriteRenderer.color = this.color;
                // 当たり判定を有効化
                this.mayCollider2D.enabled = true;
                break;
            default:
                break;
        }
    }
}
