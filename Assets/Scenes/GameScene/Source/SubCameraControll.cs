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
    // 2:グレイスケール（右画面）
    // 3:グレイスケール（全画面）
    int effect;

    // 初期処理
    void Start()
    {
        // エフェクト状態にノーマルを設定
        this.effect = 1;
    }

    // ポストエフェクト処理
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        switch (this.effect)
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
    public int Effect
    {
        get { return this.effect; }
        set { this.effect = value; }
    }
}
