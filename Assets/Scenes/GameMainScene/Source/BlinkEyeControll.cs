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

    // ポーズフラグ
    private bool _isPause = false;

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

        // ポーズイベントの登録
        SceneMaster.instance.OnScenePause.AddListener(Pause);
        SceneMaster.instance.OnSceneUnpouse.AddListener(Unpause);
    }

    // 更新処理
    void Update()
    {
        // ポーズ中の場合、更新処理を終了
        if (_isPause)
        {
            return;
        }

        // 一定時間経過したら、自身を破棄
        this.deltaTime += Time.deltaTime;
        if (this.deltaTime >= LIVE_TIME)
        {
            Destroy(gameObject);
        }
    }

    // シェーダ設定
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

    // ポーズ処理
    private void Pause()
    {
        _isPause = true;
    }

    // ポーズ解除処理
    private void Unpause()
    {
        _isPause = false;
    }
}
