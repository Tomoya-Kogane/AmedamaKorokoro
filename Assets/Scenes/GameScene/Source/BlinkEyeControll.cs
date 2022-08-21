using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// まばたきする目玉を管理するクラス
/// </summary>
public class BlinkEyeControll : MonoBehaviour
{
    // スプライトレンダラー操作用の変数
    SpriteRenderer spriteRenderer;
    Color color;

    // アニメーション操作用の変数
    Animator animator;
    
    // シェーダ用の変数
    public Material material1;
    public Material material2;

    // カメラエフェクト確認用の変数
    CameraControll cameraControll;

    // オブジェクト生存時間管理用の変数
    const float LIVE_TIME = 3.0f;
    float deltaTime = 0.0f;

    // 初期処理
    void Start()
    {
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

        // アニメーションのコンポーネントを取得
        this.animator = GetComponent<Animator>();
    }

    // 更新処理
    void Update()
    {
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

    // アニメーション速度の設定
    public void SetAnimationSpeed(float speed)
    {
        this.animator.speed = speed;
    }
    // アニメーション速度の取得
    public float GetAnimationSpeed()
    {
        return this.animator.speed;
    }

}
