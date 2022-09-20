using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// サウンドを管理するクラス（シングルトン）
/// </summary>
public class BgmMaster : MonoBehaviour
{
    // インスタンス
    public static BgmMaster instance;

    // BGM管理用の変数
    AudioSource audioSource;
    // BGMのループ位置
    const float GAME_BGM_LOOPTIME = 17.0f;
    // BGMのループ開始位置
    const float GAME_BGM_RESTARTTIME = 1.4f;

    // 生成処理
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        instance = this;

        // BGM操作用コンポーネントを取得
        this.audioSource = GetComponent<AudioSource>();
    }

    // 更新処理
    void Update()
    {
        // BGMの再生時間がループ位置に達してるか判断
        if (this.audioSource.time > GAME_BGM_LOOPTIME)
        {
            this.audioSource.time = GAME_BGM_RESTARTTIME;
            this.audioSource.Play();
        }
    }
}
