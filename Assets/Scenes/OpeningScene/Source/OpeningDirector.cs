using UnityEngine;

/// <summary>
/// オープニングの挙動を管理するクラス
/// </summary>
public class OpeningDirector : MonoBehaviour
{
    // 初期処理
    void Start()
    {
        // リフレッシュレートを60フレームに設定
        Application.targetFrameRate = 60;
    }

    // 更新処理
    void Update()
    {
        // スペース or 画面タップでGameSceneへ戻る
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            SceneMaster.instance.ChangeNextScene(SceneList.GameScene);
        }
    }
}
