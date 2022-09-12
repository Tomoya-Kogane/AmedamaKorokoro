using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// メインカメラを管理するクラス
/// </summary>
public class CameraControll : MonoBehaviour
{
    // カメラ移動用の変数
    GameObject player;
    // カメラ移動制限用の変数
    Vector2 MinCameraPos;
    Vector2 MaxCameraPos;

    // レンダーテクスチャ操作用の変数
    Camera subCamera;
    SubCameraControll subCameraControll;

    // ポストエフェクト用の変数
    public Material material1;
    public Material material2;
    public Material material3;

    // エフェクト状態管理用の変数
    // 1:通常
    // 2:グレイスケール（右画面）
    // 3:グレイスケール（全画面）
    int effect;

    // イベント定義
    public class myEvent : UnityEvent<int> { }
    // エフェクト変更
    public myEvent OnChangeEffect = new myEvent();

    // 初期処理
    void Start()
    {
        // プレイヤーボールのオブジェクトを取得
        this.player = GameObject.Find("PlayerBall");

        // カメラオブジェクトを取得
        this.subCamera = GameObject.Find("Sub Camera").GetComponent<Camera>();
        this.subCameraControll = GameObject.Find("Sub Camera").GetComponent<SubCameraControll>();

        // カメラの移動範囲を設定
        this.MinCameraPos = new Vector2(0.0f, 5.0f) ;
        this.MaxCameraPos = new Vector2(30.0f, -5.0f);

        // エフェクト状態にノーマルを設定
        this.effect = 1;
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
        cameraPos.x = this.player.transform.position.x;

        // 移動制限の判定
        if (cameraPos.x < this.MinCameraPos.x) cameraPos.x = this.MinCameraPos.x;
        if (cameraPos.x > this.MaxCameraPos.x) cameraPos.x = this.MaxCameraPos.x;

        // 移動の適用
        transform.position = cameraPos;
        this.subCamera.transform.position = cameraPos;
    }

    // ポストエフェクト処理
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        switch(this.effect)
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
    // 画面のテクスチャを作成
    public Sprite PhotoScreen()
    {
        // Texture2Dを作成
        Texture2D tex = new Texture2D(this.subCamera.targetTexture.width, this.subCamera.targetTexture.height);

        // レンダーテクスチャを有効化
        RenderTexture.active = this.subCamera.targetTexture;

        // カメラのレンダリングを実施
        this.subCamera.Render();

        // Texture2Dを作成
        tex.ReadPixels(new Rect(0, 0, this.subCamera.targetTexture.width, this.subCamera.targetTexture.height), 0, 0);
        tex.Apply();

        // レンダーテクスチャを無効化
        RenderTexture.active = null;

        // 現在の画面をSprite化して返す
        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f, 108.0f);
    }

    // プロパティ定義
    // エフェクト
    public int Effect
    {
        get { return this.effect; }
        set
        { 
            // 自身のエフェクトを変更
            this.effect = value;
            // サブカメラのエフェクトも合わせて変更
            this.subCameraControll.Effect = value;
            // エフェクト変更のイベントを発行
            OnChangeEffect.Invoke(value);
        }
    }
}
