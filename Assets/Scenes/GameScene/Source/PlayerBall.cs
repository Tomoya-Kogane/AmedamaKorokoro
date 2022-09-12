using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// プレイヤー操作のボールを管理するクラス
/// </summary>
public class PlayerBall : MonoBehaviour
{
    // ボール操作用の変数
    Rigidbody2D rigid2D;
    // 物理マテリアル（バウンドのON/OFF）
    public PhysicsMaterial2D bounceOFF;
    public PhysicsMaterial2D bounceON;

    // エフェクト管理用の変数
    // 0:エフェクト解除
    // 1:エフェクトなし
    // 2:移動フェクト
    // 3:ジャンプフェクト
    // 4:着地エフェクト
    private int _effect;
    // 移動エフェクト
    public GameObject walkEffect;
    GameObject objWalkEffect;
    // ジャンプエフェクト
    public GameObject jumpEffect;
    GameObject objJumpEffect;
    // 着地エフェクト
    public GameObject touchdownEffect;
    GameObject objTouchdownEffect;
    // 砕けるエフェクト
    public Explodable explodable;

    // ボールの初期位置
    Vector3 startPos;
    // グラウドの接触状態の判定値
    bool flgGround = false;
    // ジャンプ状態の判定値
    bool flgJump = false;

    // ジャンプ力
    const float JUMP_FORCE = 1200.0f;
    // 移動速度
    const float WALK_FORCE = 20.0f;
    // 最大移動速度の制限値
    const float MAX_WALKSPEED = 4.5f;
    // 最小移動速度の制限値
    const float MIN_WALKSPEED = 1.5f;
    // 落下判定の基準値
    const float CHECK_FALLOUT = -5.5f;
    // 傾きの基準値
    const float CHECK_THRESHOLD = 0.2f;

    // モード管理用の変数
    // 1:飴玉
    // 2:目玉
    private int _mode;
    // ライフ管理用の変数
    private int _life;
    // 飴玉の耐久値用の変数
    private int _candyLife;

    // ステータス管理用の変数
    // 1:通常
    // 2:ダメージ
    // 3:リカバリー
    private int _status;

    // アニメーション操作用の変数
    Animator animator;
    float animationFrame;

    // ポーズフラグ
    private bool _isPause = false;

    // イベント定義
    public UnityEvent OnDamage;
    public UnityEvent OnClear;

    // 初期処理
    void Start()
    {
        // 物理演算用のボールのコンポーネントを取得
        this.rigid2D = GetComponent<Rigidbody2D>();
        // バウンドOFFの物理マテリアルを設定
        this.rigid2D.sharedMaterial = this.bounceOFF;

        // ボールの初期位置を取得
        this.startPos = transform.position;
        // ボールモードの初期値を設定（飴玉）
        _mode = 1;
        // ライフの初期値を設定
        _life = 3;
        // 飴玉の耐久値を設定
        _candyLife = 5;
        // ボールステータスの初期値を設定
        _status = 1;

        // エフェクトの初期値を設定
        _effect = 1;
        this.objWalkEffect = null;
        this.objJumpEffect = null;
        this.objTouchdownEffect = null;

        // アニメーション操作用のコンポーネントを取得
        this.animator = GetComponent<Animator>();
        // アニメーションの停止
        this.animator.SetFloat("AnimeSpeed", 0.0f);
        this.animator.Play("Base Layer.CandyCrash", 0, 0.0f);
        // アニメーションフレームの初期値を設定
        this.animationFrame = 0.0f;

        // ポーズイベントの登録
        SceneMaster.instance.OnScenePause.AddListener(Pause);
        SceneMaster.instance.OnSceneUnpouse.AddListener(Unpause);
    }

    // 更新処理
    void Update()
    {
        // ステータスが通常以外の場合、更新処理を終了
        // ポーズ中の場合、更新処理を終了
        if (_status != 1 || _isPause == true)
        {
            return;
        }

        // 移動処理
        Move();

        // 移動の影響確認
        CheckMovement();

        // エフェクトの判定処理
        CheckEffect();

        // エフェクト処理
        ExecuteEffect();
    }

    // 移動処理
    private void Move()
    {
        // 移動ベクトル用の変数
        Vector3 moveForce = new Vector3(0.0f, 0.0f, 0.0f);

        // 向きの設定（矢印キー＆傾き）
        int walkDir = 0;
        if (Input.GetKey(KeyCode.RightArrow) || Input.acceleration.x > CHECK_THRESHOLD)
        {
            walkDir = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.acceleration.x < -CHECK_THRESHOLD)
        {
            walkDir = -1;
        }

        // 現在の移動速度を取得（Ｘ軸）
        float ballSpeedX = Mathf.Abs(this.rigid2D.velocity.x);
        // 最大速度を超えていない場合、加速度を設定（Ｘ軸）
        if (ballSpeedX < MAX_WALKSPEED)
        {
            moveForce = new Vector3(walkDir * WALK_FORCE, moveForce.y, moveForce.z);
        }

        // 現在のジャンプ速度を取得（Ｙ軸）
        float ballJumpY = this.rigid2D.velocity.y;
        // ジャンプの設定（Ｙ軸）
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !flgJump)
        {
            // 現在のジャンプ速度に応じて、加算するジャンプ力を変更
            float jumpForce = 0.0f;
            if (ballJumpY > 0.5f)
            {
                // 上方向にバウンド中の場合、ジャンプ速度を0.6倍で加算
                jumpForce = JUMP_FORCE * 0.6f;
            }
            else 
            {
                // 上記以外の場合、ジャンプ速度を等倍で加算
                jumpForce = JUMP_FORCE;
            }
            moveForce = new Vector3(moveForce.x, jumpForce, moveForce.z);
            this.flgJump = true;
        }

        // 移動ベクトルの適用
        this.rigid2D.AddForce(moveForce);
    }

    // 移動の影響確認
    private void CheckMovement()
    {
        // 落下判定（画面外）
        if (transform.position.y < CHECK_FALLOUT)
        {
            // ライフを１減らす
            _life--;
            // リカバリー状態に移行
            _status = 3;
            // 物理演算を無効化
            this.rigid2D.simulated = false;
            // ダメージイベントを発行
            OnDamage.Invoke();
        }

        // 飴玉の耐久度判定
        if (_candyLife <= 0 && _mode == 1 && _status == 1)
        {
            // ライフを１減らす
            _life--;
            // リカバリー状態に移行
            _status = 3;
            // 飴玉を砕く
            explodable.explode();
            // 物理演算を無効
            this.rigid2D.simulated = false;
            // ボールを非表示
            HiddenBall();
            // ダメージイベントを発行
            OnDamage.Invoke();
        }
    }

    // エフェクト処理
    private void CheckEffect()
    {
        // 現在の移動速度を取得
        float ballSpeedX = Mathf.Abs(this.rigid2D.velocity.x);
        float ballSpeedY = this.rigid2D.velocity.y;

        // 最低移動速度の設定（Ｘ軸）
        if (ballSpeedX < MIN_WALKSPEED)
        {
            // 最低速度未満の場合、移動エフェクトを解除
            _effect = 1;
        }
        else if (flgGround)
        {
            // 地面に触れた状態で最低速度以上の場合、移動エフェクトを設定
            _effect = 2;
        }

        // ジャンプ中(上昇中)の場合、ジャンプエフェクトを設定
        if (this.flgJump && ballSpeedY > 1.0f)
        {
            _effect = 3;
        }
    }

    // エフェクト処理
    private void ExecuteEffect()
    { 
        switch(_effect)
        {
            // エフェクト解除
            case 0:
                // 移動エフェクトの解除
                if (this.objWalkEffect != null)
                {
                    Destroy(this.objWalkEffect);
                }
                // ジャンプエフェクトの解除
                if (this.objJumpEffect != null)
                {
                    Destroy(this.objJumpEffect);
                }
                // 着地エフェクトの解除
                if (this.objTouchdownEffect != null)
                {
                    Destroy(this.objTouchdownEffect);
                }
                // エフェクトなしに変更
                _effect = 1;
                break;
            // エフェクトなし
            case 1:
                break;
            // 移動フェクト
            case 2:
                if (this.objWalkEffect == null)
                {
                    this.objWalkEffect = Instantiate(this.walkEffect, this.transform.position, Quaternion.identity);
                }
                break;
            // ジャンプエフェクト
            case 3:
                if (this.objJumpEffect == null)
                {
                    this.objJumpEffect = Instantiate(this.jumpEffect, this.transform.position, Quaternion.identity);
                }
                // ジャンプエフェクトは一度きりの為、エフェクトなしに変更
                _effect = 1;
                break;
            // 上記以外
            default:
                break;
        }
    }

    // リスタート処理
    public void Restart()
    {
        // 初期位置に戻る
        transform.position = this.startPos;
        // 目玉モードに変更
        _mode = 2;
        // ボールを表示
        DisplayBall();
        // 目玉アニメーションを設定
        this.animator.SetFloat("AnimeSpeed", 0.0f);
        this.animator.Play("Base Layer.Eye", 0, 0.0f);
        // バウンドONの物理マテリアルを設定
        this.rigid2D.sharedMaterial = this.bounceON;

        // 飴玉の欠片を破棄
        explodable.deleteFragments();

        // エフェクトを解除
        _effect = 0;
        ExecuteEffect();
    }

    // ボールの一時停止状態の解除処理
    public void ResetBallPause()
    {
        // 通常状態へ移行
        _status = 1;
        // 物理演算を有効
        this.rigid2D.simulated = true;
    }

    // 衝突判定
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 地面と衝突した場合、
        if (collision.gameObject.tag == "Ground")
        {
            // ジャンプフラグをOFF
            if (this.flgJump)
            {
                this.flgJump = false;
                // 飴玉モードの場合
                if (_mode == 1)
                {
                    // 飴玉の耐久値を減らす。
                    _candyLife -= 1;
                    this.animationFrame += 0.25f;
                    this.animator.Play("Base Layer.CandyCrash", 0, this.animationFrame);
                }
            }

            // 着地エフェクトを発生
            if (this.objTouchdownEffect == null && this.objWalkEffect == null && collision.gameObject.name != "Fire Bowl")
            {
                this.objTouchdownEffect = Instantiate(this.touchdownEffect, this.transform.position, Quaternion.identity);
            }
        }
        // スパイクと衝突した場合、
        if (collision.gameObject.tag == "Spike" && _status == 1)
        {
            switch (_mode)
            {
                // 飴玉
                case 1:
                    // 物理演算を無効
                    this.rigid2D.simulated = false;
                    // 飴玉の耐久値をZEROに変更
                    _candyLife = 0;
                    // ライフを１減らす
                    _life--;
                    // リカバリー状態に移行
                    _status = 3;
                    // 飴玉を砕く
                    explodable.explode();
                    // ボールを非表示
                    HiddenBall();
                    // ダメージイベントを発行
                    OnDamage.Invoke();
                    break;
                // 目玉
                case 2:
                    // ダメージ状態へ移行
                    _status = 2;
                    // 物理演算を無効化
                    this.rigid2D.simulated = false;
                    // ダメージ状態の演出を実行
                    StartCoroutine(DamageStaging());
                    break;
                default:
                    break;
            }
        }
    }

    // シーン切り替え処理（遅延あり）
    private IEnumerator DamageStaging()
    {
        // ダメージアニメーションを再生
        this.animator.SetFloat("AnimeSpeed", 1.0f);
        this.animator.Play("Base Layer.EyeDamage", 0, 0.0f);
        // ダメージ状態を指定秒数だけ維持
        yield return new WaitForSeconds(1.0f);
        // ライフを１減らす
        _life--;
        // リカバリー状態に移行
        _status = 3;
        // ダメージイベントを発行
        OnDamage.Invoke();
    }

    // 衝突判定（トリガー）
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // 受け皿と衝突したら、クリアイベントを発行
        if (collider.gameObject.name == "Fire Bowl")
        {
            // エフェクトを解除
            _effect = 0;
            ExecuteEffect();

            // クリアイベントを発行
            OnClear.Invoke();
        }

        // 敵性との判定
        if (_mode == 2 && collider.gameObject.tag == "Enemy")
        {
            Debug.Log("敵");
            // 敵対フラグがオンの場合、ダメージを発生
            if (collider.gameObject.GetComponent<GhostControll>().IsEnemy)
            {
                // ダメージ状態の演出を実行
                StartCoroutine(DamageStaging());
            }
        }
    }

    // 接触判定（当たっている）
    private void OnCollisionStay2D(Collision2D collision)
    {
        // 地面に接触
        if (collision.gameObject.tag == "Ground")
        {
            flgGround = true;
        }
    }

    // 接触判定（離れた）
    private void OnCollisionExit2D(Collision2D collision)
    {
        // 地面から離れた
        if (collision.gameObject.tag == "Ground")
        {
            flgGround = false;
        }
    }

    // ボールの非表示処理
    private void HiddenBall()
    {
        SpriteRenderer spriteRenderer;
        Color color;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
        color.a = 0.0f;
        spriteRenderer.color = color;
    }

    // ボールの表示処理
    private void DisplayBall()
    {
        SpriteRenderer spriteRenderer;
        Color color;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
        color.a = 1.0f;
        spriteRenderer.color = color;
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

    // プロパティ定義
    // プレイヤーのライフ
    public int Life
    {
        get { return _life; }
    }
    // プレイヤーのモード
    public int Mode
    {
        get { return _mode; }
    }
    // プレイヤーのステータス
    public int Status
    {
        get { return _status; }
    }
    // 飴玉の耐久値
    public int CandyLife
    {
        get { return _candyLife; }
    }
    // エフェクト状態
    public int Effect
    {
        get { return _effect; }
    }
}
