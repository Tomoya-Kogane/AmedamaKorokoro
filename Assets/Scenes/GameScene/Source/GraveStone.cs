using UnityEngine;

/// <summary>
/// 墓石を管理するクラス
/// </summary>
public class GraveStone : MonoBehaviour
{
    // スプライトレンダラー操作用の変数
    SpriteRenderer spriteRenderer;
    Color color;

    // シェーダ用の変数Listener

    public Material material1;
    public Material material2;

    // 当たり判定操作用の変数
    Collider2D mayCollider2D;

    // メインカメラの変数
    GameObject mainCamera;
    CameraControll cameraControll;

    // 初期処理
    void Start()
    {
        // スプライトレンダラーの取得
        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        this.color = this.spriteRenderer.color;

        // シェーダの設定（デフォルトシェーダ）
        this.spriteRenderer.material = this.material1;

        // Colliderの取得
        this.mayCollider2D = gameObject.GetComponent<Collider2D>();

        // メインカメラの取得
        this.mainCamera = GameObject.Find("Main Camera");
        this.cameraControll = GameObject.Find("Main Camera").GetComponent<CameraControll>();
        this.cameraControll.OnChangeEffect.AddListener((value) => { SetShader((int)value); });

        // シェーダとアルファ値の設定（透過）
        this.spriteRenderer.material = this.material1;
        this.color.a = 0.0f;
        this.spriteRenderer.color = this.color;
    }

    // 更新処理
    void Update()
    {
        // カメラエフェクトに応じて当たり判定を設定
        switch (this.cameraControll.Effect)
        {
            // カメラエフェクトなしの場合、当たり判定を無効化
            case 1:
                this.mayCollider2D.enabled = false;
                break;
            // 右画面がグレイスケールの場合、描画位置に応じて当たり判定を設定
            case 2:
                // 画面の右側に存在する場合、当たり判定を有効化
                if (transform.position.x >= this.mainCamera.transform.position.x)
                {
                    this.mayCollider2D.enabled = true;
                }
                // 画面の左側に存在する場合、当たり判定を無効化
                else
                {
                    this.mayCollider2D.enabled = false;
                }
                break;
            // 画面全体がグレイスケールの場合、当たり判定を有効化
            case 3:
                this.mayCollider2D.enabled = true;
                break;
            default:
                break;
        }
    }

    private void SetShader(int value)
    {
        // カメラエフェクトに応じてシェーダを設定
        switch (value)
        {
            // 初期エフェクトの場合、デフォルトシェーダとアルファ値(ZERO)を設定
            case 1:
                // シェーダの設定（デフォルトシェーダ）
                this.spriteRenderer.material = this.material1;
                // アルファ値の設定（ZERO）
                this.color.a = 0.0f;
                this.spriteRenderer.color = this.color;
                break;
            // 画面右側がグレースケールの場合、アクセサリエフェクトとアルファ値(1)を設定
            case 2:
                // シェーダの設定（アクセサリエフェクト）
                this.spriteRenderer.material = this.material2;
                // アルファ値の設定（1）
                this.color.a = 1.0f;
                this.spriteRenderer.color = this.color;
                break;
            // 全体がグレースケールの場合、デフォルトシェーダとアルファ値(1)を設定
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
    }
}
