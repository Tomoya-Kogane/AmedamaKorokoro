using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameSceneを管理するクラス
/// </summary>
public class GameDirector : MonoBehaviour
{
    // BGM管理用の変数
    AudioSource audioSource;
    // BGMのループ位置
    float LoopAudioTime = 17.0f;

    // 初期処理
    void Start()
    {
        // リフレッシュレートを60フレームに設定
        Application.targetFrameRate = 60;
        // BGM操作用コンポーネントを取得
        this.audioSource = GetComponent<AudioSource>();
    }

    // 更新処理
    void Update()
    {
        // BGMの再生時間がループ位置に達してるか判断
        if (this.audioSource.time > this.LoopAudioTime)
        {
            this.audioSource.time = 1.4f;
            this.audioSource.Play();
        }

    }
}
