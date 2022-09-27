using UnityEngine;

/// <summary>
/// クリアシーンを管理するクラス
/// </summary>
public class ClearDirector : MonoBehaviour
{
    // ゲームクリア時の画像設定用
    SpriteRenderer image;

    // 初期処理
    void Start()
    {
        // リフレッシュレートを60フレームに設定
        Application.targetFrameRate = 60;
        // ゲームクリア時の画像を設定
        image = GameObject.Find("GameSceneImage").GetComponent<SpriteRenderer>();
        image.sprite = SceneMaster.SceneData.GameDisplay;
    }

    // 更新処理
    void Update()
    {
        // スペース or 画面タップでGameSceneへ戻る
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            SceneMaster.instance.ChangeNextScene(SceneList.GameMainScene);
        }
    }
}
