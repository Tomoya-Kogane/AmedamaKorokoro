using UnityEngine;

/// <summary>
/// まばたきする目玉を管理するクラス
/// </summary>
public class BlinkEyeControll : MonoBehaviour
{
    // スプライトレンダラー操作用の変数
    SpriteRenderer spriteRenderer;

    // シェーダ用の変数
    public Material material1;
    public Material material2;

    // カメラエフェクト確認用の変数
    CameraControll cameraControll;

    // オブジェクト生存時間管理用の変数
    const float LIVE_TIME = 6.0f;
    float deltaTime = 0.0f;

    // 初期処理
    void Start()
    {
        // スプライトレンダラーの取得
        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        // カメラコントローラーコンポーネントを取得
        this.cameraControll = GameObject.Find("Main Camera").GetComponent<CameraControll>();
        this.cameraControll.OnChangeEffect.AddListener((value) => { SetShader((int)value); });

        // カメラのエフェクトに応じたシェーダを設定
        switch (this.cameraControll.Effect)
        {
            case 2:
                this.spriteRenderer.material = this.material2;
                break;
            case 3:
                this.spriteRenderer.material = this.material1;
                break;
            default:
                this.spriteRenderer.material = this.material1;
                break;
        }
    }

    // 更新処理
    void Update()
    {
        // 一定時間経過したら、自身を破棄
        this.deltaTime += Time.deltaTime;
        if (this.deltaTime >= LIVE_TIME)
        {
            Destroy(gameObject);
        }
    }

    private void SetShader(int value)
    {
        // カメラのエフェクトに応じたシェーダを設定
        switch (value)
        {
            case 2:
                this.spriteRenderer.material = this.material2;
                break;
            case 3:
                this.spriteRenderer.material = this.material1;
                break;
            default:
                this.spriteRenderer.material = this.material1;
                break;
        }
    }
}
