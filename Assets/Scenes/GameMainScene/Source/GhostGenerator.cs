using UnityEngine;

/// <summary>
/// ゴーストを生成するクラス
/// </summary>
public class GhostGenerator : MonoBehaviour
{
    // ランダム生成対象の変数
    public GameObject ghostPrefab;

    // メインカメラのオブジェクトとクラス用の変数
    GameObject mainCamera;
    CameraControll cameraControll;

    // スポーン管理用の変数
    const float SPAWN_TIME = 6.0f;
    float deltaTime = 0.0f;
    bool canSpawn = false;

    // 初期処理
    void Start()
    {
        // カメラとカメラコントローラーの取得
        this.mainCamera = GameObject.Find("Main Camera");
        this.cameraControll = GameObject.Find("Main Camera").GetComponent<CameraControll>();

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
        GameObject spawn;
        float posX;
        float posY;

        this.deltaTime += Time.deltaTime;
        if (this.deltaTime > SPAWN_TIME)
        {
            this.deltaTime = 0.0f;
            switch (this.cameraControll.Effect)
            {
                case 2:
                    // カメラの右側に追加
                    spawn = Instantiate(ghostPrefab);
                    posY = Random.Range(0.0f, 3.0f);
                    spawn.transform.position = new Vector3(this.mainCamera.transform.position.x + 15.0f, this.mainCamera.transform.position.y + posY, 0.0f);
                    break;
                case 3:
                    // カメラの左右に追加
                    spawn = Instantiate(ghostPrefab);
                    posX = (float)Random.Range(0, 2);
                    posY = Random.Range(0.0f, 3.0f);
                    spawn.transform.position = new Vector3(this.mainCamera.transform.position.x + (posX * 30.0f) - 15.0f, this.mainCamera.transform.position.y + posY, 0.0f);
                    break;
                default:
                    break;
            }
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
