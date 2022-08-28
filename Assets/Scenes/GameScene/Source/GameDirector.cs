using System.Collections;
using System.Collections.Generic;
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
    private Transition _transition;

    // プレイヤー状態確認用の変数
    private PlayerBall _player;

    // カメラエフェクト変更用の変数
    CameraControll mainCamera;

    // UI操作用の変数
    UIDirector uiDirector;

    // ステージステータス管理用の変数
    int stageStatus;

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
        _transition = GameObject.Find("TransitionImage").GetComponent<Transition>();
        _transition.OnTransition.AddListener(Restart);
        _transition.OnComplete.AddListener(() => _player.ResetBallPause()) ;

        // カメラのオブジェクトを取得
        this.mainCamera = GameObject.Find("Main Camera").GetComponent<CameraControll>();
        this.mainCamera.SetEffectStatus(1);

        // UI管理のクラスを取得
        this.uiDirector = GameObject.Find("UI Director").GetComponent<UIDirector>();

        // プレイヤー状態確認用の変数
        _player = GameObject.Find("PlayerBall").GetComponent<PlayerBall>();

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

        // ステージステータスに応じた処理分岐
        switch(this.stageStatus)
        {
            // ゲーム進行中
            case 0:
                // プレイヤーがダメージ状態の場合
                if (_player.Status == 3 && _transition.IsRunning() == false)
                {
                    // プレイヤーライフが残っている場合、
                    if (_player.Life != 0)
                    {
                        // フェードアウト＆インを実行
                        _transition.FadeOutIn(2.0f);
                    } 
                    else
                    {
                        // フェードアウトを実行
                        _transition.FadeOut(2.0f);
                    }
                }
                break;
            // ゲームクリア
            case 1:
                // クリアシーンへ遷移（遅延あり）
                StartCoroutine(DelayChangeScene(1, SCENE_CHANGETIME));
                break;
            // 上記以外
            default:
                // 未処理
                break;
        }

    }

    // シーン切り替え処理（遅延あり）
    private IEnumerator DelayChangeScene(int selectScene, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject.Find("Main Camera").GetComponent<CameraControll>().PhotoScreen();
        switch (selectScene)
        {
            case 1:
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

    // ステージステータスの設定
    public void SetStageStatus(int status) { this.stageStatus = status; }
    // ステージステータスの取得
    public int GetStageStatus() { return this.stageStatus; }

    // ゲームシーンのリスタート処理
    private void Restart()
    {
        // プレイヤーのりスタート処理
        _player.Restart();

        // プレイヤーの残りライフ応じてカメラエフェクトを設定
        switch (_player.Life)
        {
            // 残機２
            case 2:
                // 左画面グレースケールを設定
                this.mainCamera.SetEffectStatus(2);
                // UIを変更（目玉UIの片目閉じ）
                this.uiDirector.ChangeUI(2);
                break;
            // 残機１
            case 1:
                // 全画面グレースケールを設定
                this.mainCamera.SetEffectStatus(3);
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
}
