using UnityEngine;

/// <summary>
/// まばたきする目玉をランダム生成するクラス
/// </summary>
public class BlinkEyeGenerator : MonoBehaviour
{
    // ランダム生成対象の変数
    public GameObject blinkEyePrefab;

    // メインカメラのオブジェクト用の変数
    GameObject mainCamera;

    // スポーン管理用の変数
    const float SPAWN_TIME = 6.5f;
    float deltaTime = 0.0f;
    bool canSpawn;

    // 初期処理
    void Start()
    {
        // カメラとカメラコントローラーの取得
        this.mainCamera = GameObject.Find("Main Camera");
        // 自動生成フラグをオフ
        this.canSpawn = false;
    }

    // 更新処理
    void Update()
    {
        // 自動生成フラグがオフの場合、処理終了
        if (!this.canSpawn)
        {
            return;
        }

        // 自動生成フラグがオンの場合、指定秒数毎に追加
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

    // プロパティ定義
    // 自動生成フラグ
    public bool CanSpawn
    {
        get { return this.canSpawn; }
        set { this.canSpawn = value; }
    }
}
