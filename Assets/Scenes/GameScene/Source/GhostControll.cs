using UnityEngine;

public class GhostControll : MonoBehaviour
{
    // 幽霊操作用の変数
    Rigidbody2D rigid2D;

    // スプライトレンダラー操作用の変数
    SpriteRenderer spriteRenderer;

    // シェーダ用の変数
    public Material material1;
    public Material material2;
    private int _effect;

    // アニメーション操作用の変数
    Animator animator;
    private bool _isAnimation;

    // メインカメラのオブジェクトとクラス用の変数
    GameObject mainCamera;
    CameraControll cameraControll;

    // プレイヤー確認用の変数
    GameObject player;

    // 敵対フラグ
    private bool _isEnemy;
    // 移動速度
    const float WALK_FORCE = 8.0f;
    // 移動速度の制限値
    const float MAX_WALKSPEED = 4.0f;
    // 移動向き（初期値右向き）
    private int _movedir = 1;

    // 視野角
    const float VIEW_ANGLE = 45.0f;
    // 視野範囲
    const float VIEW_RANGE = 5.0f;

    // オブジェクト生存時間管理用の変数
    const float LIVE_TIME = 10.0f;
    float deltaTime = 0.0f;

    // 初期処理
    void Start()
    {
        // 幽霊操作用コンポーネントの取得
        this.rigid2D = GetComponent<Rigidbody2D>();

        // スプライトレンダラーの取得
        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        // アニメーション操作用のコンポーネントを取得
        this.animator = GetComponent<Animator>();
        _isAnimation = false;

        // メインカメラのオブジェクトの取得
        this.mainCamera = GameObject.Find("Main Camera");
        this.cameraControll = GameObject.Find("Main Camera").GetComponent<CameraControll>();
        this.cameraControll.OnChangeEffect.AddListener((value) => { SetShader((int)value); });

        // カメラのエフェクトに応じたシェーダを設定
        _effect = this.cameraControll.Effect;
        switch (_effect)
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

        // プレイヤーの取得
        this.player = GameObject.Find("PlayerBall");

        // 敵対フラグの初期化
        _isEnemy = false;
        // 移動方向と画像の向きを設定
        Vector3 scale = transform.localScale;
        if (transform.position.x >= this.mainCamera.transform.position.x)
        {
            _movedir = -1;
            scale.x = 1;
        }
        else
        {
            _movedir = 1;
            scale.x = -1;
        }
        transform.localScale = scale;
    }

    // 更新処理
    void Update()
    {
        Debug.Log(transform.forward);

        // 移動処理
        Move();

        // 視界判定
        CheckVisible();

        // 一定時間経過したら、自身を破棄
        this.deltaTime += Time.deltaTime;
        if (this.deltaTime >= LIVE_TIME)
        {
            Destroy(gameObject);
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
            moveForce = new Vector3(_movedir * WALK_FORCE, moveForce.y, moveForce.z);
        }

        // 移動ベクトルの適用
        this.rigid2D.AddForce(moveForce);
    }

    // 視界判定
    private void CheckVisible()
    {
        // ゴーストの座標
        Vector2 ghostPos = new Vector2(transform.position.x, transform.position.y);
        // ゴーストの向き
        Vector2 ghostDir = new Vector2((float)_movedir, 0.0f);

        // プレイヤーの座標
        Vector2 playerPos = new Vector2(this.player.transform.position.x, this.player.transform.position.y);

        // ゴーストとプレイヤーの距離と向き
        Vector2 targetDir = playerPos - ghostPos;

        // 視野角
        float viewAngle = Mathf.Cos(VIEW_ANGLE / 2 * Mathf.Deg2Rad);

        // ゴーストとプレイヤーの内積計算
        float innerProduct = Vector2.Dot(ghostDir, targetDir.normalized);

        // 視界判定
        if (innerProduct > viewAngle && targetDir.magnitude < VIEW_RANGE)
        {
            if (!_isAnimation)
            {
                // アニメーションを再生
                this.animator.SetFloat("AnimeSpeed", 1.0f);
                this.animator.Play("Base Layer.GhostHorror", 0, 0.0f);
                _isAnimation = true;
                _isEnemy = true;
            }
        }
    }

    private void SetShader(int effect)
    {
        _effect = effect;
        // カメラのエフェクトに応じたシェーダを設定
        switch (_effect)
        {
            case 2:
                this.spriteRenderer.material = this.material2;
                break;
            case 3:
                this.spriteRenderer.material = this.material1;
                break;
            default:
                break;
        }
    }

    // プロパティ定義
    // 敵対フラグ
    public bool IsEnemy
    {
        get
        {
            // カメラエフェクト、座標、敵対フラグの状況に応じて、戻り値を設定
            switch (_effect)
            {
                // 初期エフェクトの場合、敵対フラグをオフを返す
                case 1:
                    return false;
                // 画面右側がグレースケールの場合、画面右側では自身で設定した敵対フラグを返す
                case 2:
                    if (this.mainCamera.transform.position.x < transform.position.x)
                    {
                        return _isEnemy;
                    }
                    else
                    {
                        return false;
                    }
                // 全体がグレースケールの場合、自身で設定した敵対フラグを返す
                case 3:
                    return _isEnemy;
                default:
                    return false;
            }
        }
    }
}