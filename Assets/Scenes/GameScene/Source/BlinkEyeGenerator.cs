using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// まばたきする目玉をランダム生成するクラス
/// </summary>
public class BlinkEyeGenerator : MonoBehaviour
{
    // ランダム生成対象の変数
    public GameObject blinkEyePrefab;

    // メインカメラの座標取得用の変数
    GameObject mainCamera;
    // カメラエフェクト確認用の変数
    CameraControll cameraControll;

    // スポーン管理用の変数
    const float SPAWN_TIME = 6.0f;
    float deltaTime = 0.0f;

    // 初期処理
    void Start()
    {
        // カメラとカメラコントローラーの取得
        this.mainCamera = GameObject.Find("Main Camera");
        this.cameraControll = GameObject.Find("Main Camera").GetComponent<CameraControll>();
    }

    // 更新処理
    void Update()
    {
        // カメラエフェクトがグレイスケールの場合、まばたきする目玉を追加
        if (this.cameraControll.GetEffectStatus() == 2 || this.cameraControll.GetEffectStatus() == 3)
        {
            // 指定秒数毎に追加
            this.deltaTime += Time.deltaTime;
            if (this.deltaTime > SPAWN_TIME)
            {
                // カメラの範囲内にランダムで追加
                this.deltaTime = 0.0f;
                GameObject spawn = Instantiate(blinkEyePrefab);
                float posX = Random.Range(-7.0f, 7.0f);
                float posY = 4.2f;
                spawn.transform.position = new Vector3(this.mainCamera.transform.position.x + posX, this.mainCamera.transform.position.y + posY, 0.0f);
            }
        }
    }
}
