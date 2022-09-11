using UnityEngine;

/// <summary>
/// オープニングのボールの挙動を管理するクラス
/// </summary>
public class OpeningBall : MonoBehaviour
{
    // ボール操作用の変数
    Rigidbody2D rigid2D;
    // 移動先座標
    public Vector2 destinationPosition;
    // 移動方向
    public enum MoveDir
    {
        Left = 1,
        Right = -1
    }
    public MoveDir moveDir;

    // 移動速度
    const float WALK_FORCE = 20.0f;
    // 移動速度の制限
    const float MAX_WALKSPEED = 4.5f;

    // 初期処理
    void Start()
    {
        // 物理演算用のボールのコンポーネントを取得
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    // 更新処理
    void Update()
    {
        // 目的地に近づくまで移動
        var distance = Vector3.Distance(transform.position, new Vector3(destinationPosition.x, destinationPosition.y, 0.0f));
        if (distance > 0.1f)
        {
            Move();
        }
        // 目的地に到着したら停止
        else
        {
            this.rigid2D.velocity = Vector3.zero;
        }
    }

    // 移動処理
    private void Move()
    {
        // 移動ベクトル用の変数
        Vector3 moveForce = new Vector3(0.0f, 0.0f, 0.0f);

        // 現在の移動速度を取得（Ｘ軸）
        float ballSpeedX = Mathf.Abs(this.rigid2D.velocity.x);
        if (ballSpeedX < MAX_WALKSPEED)
        {
            moveForce = new Vector3((float)moveDir * WALK_FORCE, moveForce.y, moveForce.z);
        }

        // 移動ベクトルの適用
        this.rigid2D.AddForce(moveForce);
    }
}
