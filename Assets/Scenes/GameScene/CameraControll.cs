using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// メインカメラを管理するクラス
/// </summary>
public class CameraControll : MonoBehaviour
{
    // カメラ移動用の変数
    GameObject ball;
    // カメラ移動制限用の変数
    Vector2 MinCameraPos;
    Vector2 MaxCameraPos;

    // ポストエフェクト用の変数
    public Material material1;
    public Material material2;
    public Material material3;

    // エフェクト状態管理用の変数
    // 1:通常、2:ﾊｰﾌｸﾞﾚｲｽｹｰﾙ、3:ｵｰﾙｸﾞﾚｲｽｹｰﾙ
    int effectStatus;

    // 初期処理
    void Start()
    {
        // プレイヤーボールのオブジェクトを取得
        this.ball = GameObject.Find("PlayerBall");
        // カメラの移動範囲を設定
        this.MinCameraPos = new Vector2(0.0f, 5.0f) ;
        this.MaxCameraPos = new Vector2(30.0f, -5.0f);
        // エフェクト状態にノーマルを設定
        this.effectStatus = 1;
    }

    // 更新処理
    void Update()
    {
        // 移動処理
        Move();
    }

    // 移動処理
    private void Move()
    {
        // カメラ位置用の変数
        Vector3 cameraPos = transform.position;

        // カメラ移動（ボールのX座標に合わせて移動）
        cameraPos.x = this.ball.transform.position.x;

        // 移動制限の判定
        if (cameraPos.x < this.MinCameraPos.x) cameraPos.x = this.MinCameraPos.x;
        if (cameraPos.x > this.MaxCameraPos.x) cameraPos.x = this.MaxCameraPos.x;

        // 移動の適用
        transform.position = cameraPos;
    }

    // ポストエフェクト処理
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        switch(this.effectStatus)
        {
            // 初期エフェクト（エフェクトなしと同様）
            case 1:
                Graphics.Blit(source, destination, material1);
                break;
            // 左半分をグレースケール
            case 2:
                Graphics.Blit(source, destination, material2);
                break;
            // 全体をグレースケール
            case 3:
                Graphics.Blit(source, destination, material3);
                break;
            // エフェクトなし
            default:
                Graphics.Blit(source, destination);
                break;
        }
    }

    // エフェクト状態の取得
    public int getEffectStatus()
    {
        return this.effectStatus;
    }

    // エフェクト状態の設定
    public void setEffectStatus(int status)
    {
        this.effectStatus = status;
    }
}
