using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// GameSceneを管理するクラス
/// </summary>
public class GameDirector : MonoBehaviour
{
    // BGM管理用の変数
    AudioSource audioSource;
    // BGMのループ位置
    const float AUDIO_LOOPTIME = 17.0f;
    // BGMのループ開始位置
    const float AUDIO_STARTTIME = 1.4f;

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

    // シーン切り替え時のスリープ時間（フレーム数指定）
    const float SCENE_CHANGETIME = 2.0f;

    // 初期処理
    void Start()
    {
        // リフレッシュレートを60フレームに設定
        Application.targetFrameRate = 60;

        // BGM操作用コンポーネントを取得
        this.audioSource = GetComponent<AudioSource>();

        // トランジション操作用コンポーネントを取得
        this.transition = GameObject.Find("TransitionImage").GetComponent<Transition>();
        this.transition.OnTransition.AddListener(Restart);
        this.transition.OnComplete.AddListener(() => this.player.ResetBallPause());

        // プレイヤー状態確認用の変数
        this.player = GameObject.Find("PlayerBall").GetComponent<PlayerBall>();
        // ダメージイベントにリスナーを追加
        this.player.OnDamage.AddListener(Fade);
        // クリアイベントにリスナーを追加
        this.player.OnClear.AddListener(() => StartCoroutine(DelayChangeScene(1, SCENE_CHANGETIME)));

        // カメラのオブジェクトを取得
        this.mainCamera = GameObject.Find("Main Camera").GetComponent<CameraControll>();

        // UI管理のクラスを取得
        this.uiDirector = GameObject.Find("UI Director").GetComponent<UIDirector>();

        // 自動生成するオブジェクト用の変数
        this.ghost = GameObject.Find("GhostGenerator").GetComponent<GhostGenerator>();
        this.blinkEye = GameObject.Find("BlinkEyeGenerator").GetComponent<BlinkEyeGenerator>();

        // シーン振り替え時の破棄を無効化
        DontDestroyOnLoad(gameObject);

        // イベント登録（シーン切り替え）
        SceneManager.sceneLoaded += ChangeSceneDirector;
    }

    // 更新処理
    void Update()
    {
        // BGMの再生時間がループ位置に達してるか判断
        if (this.audioSource.time > AUDIO_LOOPTIME)
        {
            this.audioSource.time = AUDIO_STARTTIME;
            this.audioSource.Play();
        }
    }

    // シーン切り替え処理（遅延あり）
    private IEnumerator DelayChangeScene(int selectScene, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        switch (selectScene)
        {
            case 1:
                GameObject.Find("Main Camera").GetComponent<CameraControll>().PhotoScreen();
                SceneManager.LoadScene("ClearScene");
                break;
            case 2:
                SceneManager.LoadScene("GameOverScene");
                break;
            default:
                break;
        }
    }
    
    // シーン切り替え時の処理
    public void ChangeSceneDirector(Scene next, LoadSceneMode mode)
    {
        switch (next.name)
        {
            case "ClearScene":
                // 次のシーンへBGMの再生位置を引き渡し
                GameObject.Find("ClearDirector").GetComponent<ClearDirector>().SetAudioTime(this.audioSource.time);
                break;
            case "GameOverScene":
                // 次のシーンへBGMの再生位置を引き渡し
                GameObject.Find("GameOverDirector").GetComponent<GameOverDirector>().SetAudioTime(this.audioSource.time);
                break;
            default:
                break;
        }

        // イベント解除（シーン切り替え）
        SceneManager.sceneLoaded -= ChangeSceneDirector;

        // オブジェクトの破棄
        Destroy(gameObject);
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

    // リスタート処理
    private void Restart()
    {
        // プレイヤーのりスタート処理
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
                // ゲームオーバーシーンへ遷移（遅延あり）
                StartCoroutine(DelayChangeScene(2, SCENE_CHANGETIME));
                break;
            default:
                break;
        }
    }

    // プロパティ定義
    // ステータス
    public int Status
    {
        get { return _status; }
    }
}
