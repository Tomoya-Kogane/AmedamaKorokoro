using System.Collections;
using UnityEngine;
using Amedamakorokoro.Utilities.SceneDataPack;

/// <summary>
/// GameSceneを管理するクラス
/// </summary>
public class GameDirector : MonoBehaviour
{
    // トランジション操作用の変数
    Transition transition;

    // プレイヤー状態確認用の変数
    PlayerBall player;

    // カメラエフェクト変更用の変数
    CameraControll mainCamera;

    // UI操作用の変数
    UIDirector uiDirector;

    // 自動生成するオブジェクト用の変数
    GhostGenerator ghost;
    BlinkEyeGenerator blinkEye;

    // ステータス管理用の変数
    // 0:ゲーム進行中
    // 1:ゲームクリア
    // 2:ゲームオーバー
    private int _status;

    // ポーズフラグ
    private bool _isPause = false;

    // 初期処理
    void Start()
    {
        // リフレッシュレートを60フレームに設定
        Application.targetFrameRate = 60;

        // トランジション操作用コンポーネントを取得
        this.transition = GameObject.Find("TransitionImage").GetComponent<Transition>();
        this.transition.OnTransition.AddListener(Restart);
        this.transition.OnComplete.AddListener(() => this.player.ResetBallPause());

        // プレイヤー状態確認用の変数
        this.player = GameObject.Find("PlayerBall").GetComponent<PlayerBall>();
        // ダメージイベントにリスナーを追加
        this.player.OnDamage.AddListener(Fade);
        // クリアイベントにリスナーを追加
        this.player.OnClear.AddListener(GameClear);

        // カメラのオブジェクトを取得
        this.mainCamera = GameObject.Find("Main Camera").GetComponent<CameraControll>();

        // UI管理のクラスを取得
        this.uiDirector = GameObject.Find("UI Director").GetComponent<UIDirector>();

        // 自動生成するオブジェクト用の変数
        this.ghost = GameObject.Find("GhostGenerator").GetComponent<GhostGenerator>();
        this.blinkEye = GameObject.Find("BlinkEyeGenerator").GetComponent<BlinkEyeGenerator>();

        // ポーズイベントの登録
        SceneMaster.instance.OnScenePause.AddListener(Pause);
        SceneMaster.instance.OnSceneUnpouse.AddListener(Unpause);
    }

    // 更新処理
    private void Update()
    {
        // ポーズ中の場合、更新処理を終了
        if (_isPause)
        {
            return;
        }

        // エスケープキー押下
        if (Input.GetKey(KeyCode.Escape))
        {
            // ポーズ処理を実施
            SceneMaster.instance.Pause();
            // メニューシーンを追加
            SceneMaster.instance.AdditiveScene(SceneList.MenuScene, null);
        }
    }

    // 暗転処理
    private void Fade()
    {
        // 暗転処理が未実行の場合
        if (!this.transition.IsRunning)
        {
            // プレイヤーライフが残っている場合
            if (this.player.Life != 0)
            {
                // フェードアウト＆インを実行
                this.transition.FadeOutIn(2.0f);
            }
            // プレイヤーライフが残っていない場合
            else
            {
                // フェードアウトを実行
                this.transition.FadeOut(2.0f);
            }
        }
    }

    // ステージクリア処理
    private void GameClear()
    {
        StartCoroutine(GameClearCoroutine());
    }
    private IEnumerator GameClearCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        var sprite = GameObject.Find("Main Camera").GetComponent<CameraControll>().PhotoScreen();
        var data = new DefaultSceneDataPack(SceneList.ClearScene, sprite);
        SceneMaster.instance.ChangeNextScene(SceneList.ClearScene, data);
    }

    // ゲームオーバー処理
    private void GameOver()
    {
        StartCoroutine(GameOverCoroutine());
    }
    private IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        var data = new DefaultSceneDataPack(SceneList.GameOverScene, null);
        SceneMaster.instance.ChangeNextScene(SceneList.GameOverScene, data);
    }


    // リスタート処理
    private void Restart()
    {
        // プレイヤーのリスタート処理
        this.player.Restart();

        // プレイヤーの残ライフに応じて、処理実施
        switch (this.player.Life)
        {
            // 残機２
            case 2:
                // 左画面グレースケールを設定
                this.mainCamera.Effect = 2;
                // UIを変更（目玉UIの片目閉じ）
                this.uiDirector.ChangeUI(2);
                // ゴーストと瞬きする目玉の自動生成を開始
                this.ghost.CanSpawn = true;
                this.blinkEye.CanSpawn = true;
                break;
            // 残機１
            case 1:
                // 全画面グレースケールを設定
                this.mainCamera.Effect = 3;
                // UIを変更（目玉UIの両目閉じ）
                this.uiDirector.ChangeUI(3);
                break;
            // ゲームオーバー
            case 0:
                // ゲームオーバーシーンへ遷移
                GameOver();
                break;
            default:
                break;
        }
    }

    // ポーズ処理
    private void Pause()
    {
        _isPause = true;
        // ゴーストと瞬きする目玉の自動生成を停止
        this.ghost.CanSpawn = false;
        this.blinkEye.CanSpawn = false;
    }

    // ポーズ解除処理
    private void Unpause()
    {
        _isPause = false;
        // ゴーストと瞬きする目玉の自動生成を開始
        this.ghost.CanSpawn = true;
        this.blinkEye.CanSpawn = true;
    }

    // プロパティ定義
    // ステータス
    public int Status
    {
        get { return _status; }
    }
}
