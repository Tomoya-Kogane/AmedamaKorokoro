using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// クリアシーンを管理するクラス
/// </summary>
public class ClearDirector : MonoBehaviour
{
    // BGM管理用の変数
    AudioSource audioSource;
    // BGMの開始位置（前シーンからの引き継ぎ）
    private static float s_audioStartTime = 0.0f;
    // BGMのループ位置
    const float AUDIO_LOOPTIME = 17.0f;

    // 初期処理
    void Start()
    {
        // リフレッシュレートを60フレームに設定
        Application.targetFrameRate = 60;
        // BGM操作用コンポーネントを取得
        this.audioSource = GetComponent<AudioSource>();
        this.audioSource.time = s_audioStartTime;
        this.audioSource.Play();
    }

    // 更新処理
    void Update()
    {
        // BGMの再生時間がループ位置に達してるか判断
        if (this.audioSource.time > AUDIO_LOOPTIME)
        {
            this.audioSource.time = 1.4f;
            this.audioSource.Play();
        }

        // スペース or 画面タップでGameSceneへ戻る
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    // BGM開始位置の設定
    public void SetAudioTime(float time)
    {
        s_audioStartTime = time;
    }
}
