using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // ボールのエフェクト用変数
    // エフェクトフラグ用の変数
    // 1:エフェクトなし
    // 2:移動フェクト
    // 3:ジャンプフェクト
    // 4:着地エフェクト
    int flgEffect;
    // 移動エフェクト
    public GameObject walkEffect;
    GameObject objWalkEffect;
    // ジャンプエフェクト
    public GameObject jumpEffect;
    GameObject objJumpEffect;
    // 着地エフェクト
    public GameObject touchdownEffect;
    GameObject objTouchdownEffect;
    // 
    public Explodable explodable;

    // ボールの初期位置
    Vector3 startPos;
    // グラウドの接触状態の判定値
    bool flgGroundON = false;
    // ジャンプ力
    const float JUMP_FORCE = 1200.0f;
    // ジャンプ状態の判定値
    bool flgJump = false;
    // 移動速度
    const float WALK_FORCE = 20.0f;
    // 最大移動速度の制限値
    const float MAX_WALKSPEED = 4.5f;
    // 最小移動速度の制限値
    const float MIN_WALKSPEED = 1.5f;
    // 落下判定の基準値
    const float CHECK_FALLOUT = -5.5f;

    // ボールモード管理用の変数
    // 1:飴玉
    // 2:目玉
    int ballMode;
    // ライフ管理用の変数
    int life;
    // 飴玉の耐久度用の変数
    int candyLife;
    // 傾きの基準値
    const float CHECK_THRESHOLD = 0.2f;
    // ボールステータス管理用の変数
    // 1:通常
    // 2:ダメージ
    int ballStatus;

    // 経過時間カウント用の変数
    float stepTime;
    // 復帰時間の設定値
    const float COMEBACK_TIME = 1.5f;
    
    // 画像切替用の変数
    public SpriteRenderer spriteRenderer;
    // あめ玉の画像
    public Sprite sprite1;
    // 目玉の画像
    public Sprite sprite2;

    // アニメーション操作用の変数
    Animator animator;
    float animationFrame;

    // ステージ状態確認用の変数
    GameDirector gameDirector;

    // カメラエフェクト変更用の変数
    CameraControll mainCamera;

    // 初期処理
    void Start()
    {
        // 物理演算用のボールのコンポーネントを取得
        this.rigid2D = GetComponent<Rigidbody2D>();
        // バウンドOFFの物理マテリアルを設定
        this.rigid2D.sharedMaterial = this.bounceOFF;

        // カメラのオブジェクトを取得
        this.mainCamera = GameObject.Find("Main Camera").GetComponent<CameraControll>();
        // カメラエフェクトの初期化
        this.mainCamera.SetEffectStatus(1);

        // ボールの初期位置を取得
        this.startPos = transform.position;
        // ボールモードの初期値を設定（飴玉）
        this.ballMode = 1;
        // ライフの初期値を設定
        this.life = 3;
        // 飴玉の耐久値を設定
        this.candyLife = 5;
        // ボールステータスの初期値を設定
        this.ballStatus = 1;

        // 経過時間カウントの初期値を設定
        this.stepTime = 0.0f;

        // エフェクトの初期値を設定
        this.flgEffect = 1;
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

        // ステージ管理コンポーネントを取得
        this.gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();
    }

    // 更新処理
    void Update()
    {
        // ステージ状態が進行中の場合、移動処理などを実施
        if (this.gameDirector.GetStageStatus() == 0 && this.ballStatus == 1)
        {
            // 移動処理
            Move();

            // 移動の影響確認
            CheckMovement();

            // エフェクトの判定処理
            CheckEffect();

            // エフェクト処理
            ExecuteEffect();
        }

        // ボールがダメージ状態の場合、リスタート処理を実施
        if (this.ballStatus == 2)
        {
            // 復帰時間を過ぎた場合、リスタート処理を実施
            this.stepTime += Time.deltaTime;
            if (this.stepTime > COMEBACK_TIME)
            {
                // リスタート処理
                Restart();
                // ダメージ状態を解除
                this.ballStatus = 1;
                // 復帰時間を初期化
                this.stepTime = 0.0f;
            }
        }
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

        // 最大移動速度の設定（Ｘ軸）
        if (ballSpeedX < MAX_WALKSPEED)
        {
            // 最大速度を超えていない場合、加速度を設定
            moveForce = new Vector3(walkDir * WALK_FORCE, moveForce.y, moveForce.z);
        }
        // 最小移動速度の設定（Ｘ軸）
        if (ballSpeedX < MIN_WALKSPEED)
        {
            // 最小速度を下回った場合、エフェクトを解除
            this.flgEffect = 1;
        }

        // 現在のジャンプ速度を取得（Ｙ軸）
        float ballJumpY = this.rigid2D.velocity.y;
        // ジャンプの設定（Ｙ軸）
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !flgJump)
        {
            // 現在のジャンプ速度に応じて、加算するジャンプ力を変更
            float jumpForce = 0.0f;
            if (ballJumpY > 0.0f)
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
            this.life--;
            // ダメージ状態に変更
            this.ballStatus = 2;
            // 物理演算を無効化
            this.rigid2D.simulated = false;
        }

        // 飴玉の耐久度判定
        if (this.candyLife <= 0 && this.ballMode == 1)
        {
            // ライフを１減らす
            this.life--;
            // ダメージ状態に変更
            this.ballStatus = 2;
            // 飴玉を砕く
            explodable.explode();
            // 物理演算を無効
            this.rigid2D.simulated = false;
            // ボールを非表示
            HiddenBall();
        }

        // ライフに応じて、カメラステータスを変更
        switch (this.life)
        {
            // 残機２
            case 2:
                // 左画面グレースケールを設定
                this.mainCamera.SetEffectStatus(2);
                break;
            // 残機１
            case 1:
                // 全画面グレースケールを設定
                this.mainCamera.SetEffectStatus(3);
                break;
            // ゲームオーバー
            case 0:
                // 処理未実装
                break;
            default:
                break;
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
            this.flgEffect = 1;
        }
        else if (flgGroundON)
        {
            // 地面に触れた状態で最低速度以上の場合、移動エフェクトを設定
            this.flgEffect = 2;
        }

        // ジャンプ中(上昇中)の場合、ジャンプエフェクトを設定
        if (this.flgJump && ballSpeedY > 1.0f)
        {
            this.flgEffect = 3;
        }
    }

    // エフェクト処理
    private void ExecuteEffect()
    { 
        switch(this.flgEffect)
        {
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
                this.flgEffect = 1;
                break;
            // 上記以外
            default:
                break;
        }
    }

    // リスタート処理
    private void Restart()
    {
        // 初期位置に戻る
        transform.position = this.startPos;
        // 物理演算を有効
        this.rigid2D.simulated = true;
        // ボールを表示
        DisplayBall();
        // 目玉アニメーションを設定
        this.animator.Play("Base Layer.Eye", 0, 0.0f);
        this.ballMode = 2;
        // バウンドONの物理マテリアルを設定
        this.rigid2D.sharedMaterial = this.bounceON;

        // 飴玉の欠片を破棄
        explodable.deleteFragments();

        // 移動エフェクトが有効の場合、移動エフェクトを破棄
        if (this.objWalkEffect != null)
        {
            Destroy(this.objWalkEffect);
        }
    }

    // パーティクルの移動処理は、エフェクト自体に実装
    // パーティクル更新処理
    //private void UpdateParticle()
    //{
    //    // 移動処理
    //    MoveParticle();

    //}

    // パーティクルの移動処理
    //private void MoveParticle()
    //{
    //    // パーティクルが常に下に来るように位置を更新
    //    var particleShape = this.particleSys.shape;
    //    float radians = transform.localEulerAngles.z * Mathf.Deg2Rad;
    //    float posX = 0.5f * Mathf.Sin(radians) * -1;
    //    float posY = 0.5f * Mathf.Cos(radians) * -1;
    //    particleShape.position = new Vector3(posX, posY, 0.0f);
    //}

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
                if (this.ballMode == 1)
                {
                    // 飴玉の耐久値を減らす。
                    this.candyLife -= 1;
                    this.animationFrame += 0.25f;
                    this.animator.Play("Base Layer.CandyCrash", 0, this.animationFrame);
                }
            }

            // 着地エフェクトを発生
            if (this.objTouchdownEffect == null && this.objWalkEffect == null)
            {
                this.objTouchdownEffect = Instantiate(this.touchdownEffect, this.transform.position, Quaternion.identity);
            }
        }
    }

    // 衝突判定（トリガー）
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // 受け皿と衝突したら、クリアシーンへ遷移
        if (collider.gameObject.name == "Fire Bowl")
        {
            this.gameDirector.SetStageStatus(1);
        }
    }

    // 接触判定（当たっている）
    private void OnCollisionStay2D(Collision2D collision)
    {
        // 地面に接触
        if (collision.gameObject.tag == "Ground")
        {
            flgGroundON = true;
        }
    }

    // 接触判定（離れた）
    private void OnCollisionExit2D(Collision2D collision)
    {
        // 地面から離れた
        if (collision.gameObject.tag == "Ground")
        {
            flgGroundON = false;
        }
    }

    // エフェクトフラグの取得
    public int GetEffectFlag()
    {
        return this.flgEffect;
    }

    // ボールの非表示処理
    private void HiddenBall()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    // ボールの表示処理
    private void DisplayBall()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }
}
