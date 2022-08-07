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
                // 未処理
                break;
            // ゲームクリア
            case 1:
                // クリアシーンへ遷移（遅延あり）
                StartCoroutine(DelayChangeScene(SCENE_CHANGETIME));
                break;
            // 上記以外
            default:
                // 未処理
                break;
        }

    }

    // シーン切り替え処理（遅延あり）
    private IEnumerator DelayChangeScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject.Find("Main Camera").GetComponent<CameraControll>().PhotoScreen();
        SceneManager.LoadScene("ClearScene");
    }
    

    // シーン切り替え時の処理
    public void ChangeSceneDirector(Scene next, LoadSceneMode mode)
    {
        // 次のシーンへBGMの再生位置を引き渡し
        GameObject.Find("ClearDirector").GetComponent<ClearDirector>().SetAudioTime(this.audioSource.time);

        // オブジェクトの破棄
        Destroy(gameObject);

        // イベント解除（シーン切り替え）
        SceneManager.sceneLoaded -= ChangeSceneDirector;
    }

    // ステージステータスの設定
    public void SetStageStatus(int status)
    {
        this.stageStatus = status;
    }
    // ステージステータスの取得
    public int GetStageStatus()
    {
        return this.stageStatus;
    }
}
