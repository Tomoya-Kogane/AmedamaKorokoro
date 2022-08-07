using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostGenerator : MonoBehaviour
{
    // ランダム生成対象の変数
    public GameObject ghostPrefab;

    // メインカメラの座標取得用の変数
    GameObject mainCamera;
    // カメラエフェクト確認用の変数
    CameraControll cameraControll;

    // スポーン管理用の変数
    const float SPAWN_TIME = 3.0f;
    float deltaTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        // カメラとカメラコントローラーの取得
        this.mainCamera = GameObject.Find("Main Camera");
        this.cameraControll = GameObject.Find("Main Camera").GetComponent<CameraControll>();
    }

    // Update is called once per frame
    void Update()
    {
        // カメラエフェクトがグレイスケールの場合、幽霊を追加
        if (this.cameraControll.GetEffectStatus() == 2 || this.cameraControll.GetEffectStatus() == 3)
        {
            // 2秒毎に追加処理
            this.deltaTime += Time.deltaTime;
            if (this.deltaTime > SPAWN_TIME)
            {
                // カメラの範囲内にランダムで追加
                this.deltaTime = 0.0f;
                GameObject spawn = Instantiate(ghostPrefab);
                float posX = Random.Range(-1.0f, 1.0f);
                float posY = Random.Range(0.0f, 3.0f);
                spawn.transform.position = new Vector3(this.mainCamera.transform.position.x + (posX * 10.0f), this.mainCamera.transform.position.y + posY, 0.0f);
            }
        }
    }
}
