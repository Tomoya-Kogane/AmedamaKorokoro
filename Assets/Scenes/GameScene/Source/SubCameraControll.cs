using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// サブカメラを管理するクラス
/// </summary>
public class SubCameraControll : MonoBehaviour
{
    // ポストエフェクト用の変数
    public Material material1;
    public Material material2;
    public Material material3;

    // エフェクト状態管理用の変数
    // 1:通常
    // 2:グレイスケール（半分）
    // 3:グレイスケール（全体）
    int effectStatus = 1;

    // Start is called before the first frame update
    void Start()
    {
        // エフェクト状態にノーマルを設定
        this.effectStatus = 1;
    }

    // ポストエフェクト処理
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        switch (this.effectStatus)
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

    // プロパティ定義
    // エフェクト状態
    public int EffectStatus
    {
        get { return this.effectStatus; }
        set { this.effectStatus = value; }
    }
}
