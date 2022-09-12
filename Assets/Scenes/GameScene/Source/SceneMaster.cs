using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Amedamakorokoro.Utilities.SceneDataPack;

/// <summary>
/// シーン切替とポーズを管理するクラス（シングルトン）
/// </summary>
public class SceneMaster : MonoBehaviour
{
    // インスタンス
    public static SceneMaster instance;

    // シーン間で受け渡すデータ
    public static SceneDataPack SceneData;

    // イベント定義
    public UnityEvent OnSceneChangeComplete;
    public UnityEvent OnSceneUnloadComplete;
    public UnityEvent OnScenePause;
    public UnityEvent OnSceneUnpouse;

    // 実行中フラグ
    private bool _isRunning = false;

    // 現在のシーン名
    private SceneList _currentScene;
    // 過去のシーン名
    private SceneList _previousScene;

    // 生成処理
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        instance = this;
        _currentScene = (SceneList)0;
        _previousScene = (SceneList)0;
        SceneData = null;
    }

    // シーン切替処理
    public void ChangeNextScene(SceneList nextScene, SceneDataPack data = null)
    {
        // 実行中、又は同じシーン名を指定した場合、切替処理は実施しない
        if (_isRunning || _currentScene == nextScene) return;

        // 受け渡すデータが未指定の場合、シーン情報のみ設定
        if (data == null)
        {
            SceneData = new DefaultSceneDataPack(nextScene, null);
        }
        else
        {
            SceneData = data;
        }

        // シーン切替処理（本体）
        StartCoroutine(ChangeSceneCoroutine(nextScene));
    }

    // シーン切替処理（本体）
    private IEnumerator ChangeSceneCoroutine(SceneList nextScene)
    {
        // シーン切替実行
        _isRunning = true;

        // 切替先シーンの読み込み
        yield return SceneManager.LoadSceneAsync(nextScene.ToString(), LoadSceneMode.Single);

        // 過去のシーン名を設定
        _previousScene = _currentScene;

        // 現在のシーン名を設定
        _currentScene = nextScene;

        // シーン切替完了のイベントを発行
        OnSceneChangeComplete.Invoke();

        // シーン切替完了
        _isRunning = false;
    }

    // シーン追加処理
    public void AdditiveScene(SceneList addScene, SceneDataPack data = null)
    {
        // 実行中の場合、シーン追加処理は実施しない
        if (_isRunning) return;

        // 受け渡すデータが未指定の場合、シーン情報のみ設定
        if (data == null)
        {
            SceneData = new DefaultSceneDataPack(addScene, null);
        }
        else
        {
            SceneData = data;
        }

        // シーン切替処理（本体）
        StartCoroutine(AdditiveSceneCoroutine(addScene));
    }

    // シーン追加処理（本体）
    private IEnumerator AdditiveSceneCoroutine(SceneList nextScene)
    {
        // シーン切替実行
        _isRunning = true;

        // 切替先シーンの読み込み
        yield return SceneManager.LoadSceneAsync(nextScene.ToString(), LoadSceneMode.Additive);

        // 過去のシーン名を設定
        _previousScene = _currentScene;

        // 現在のシーン名を設定
        _currentScene = nextScene;

        // シーン切替完了のイベントを発行
        OnSceneChangeComplete.Invoke();

        // シーン切替完了
        _isRunning = false;
    }

    // シーンアンロード処理
    public void UnloadScene(SceneList unloadScene, bool isPause)
    {
        // 実行中の場合、シーンアンロード処理は実施しない
        if (_isRunning) return;

        // シーンアンロード処理（本体）
        StartCoroutine(UnloadSceneCoroutine(unloadScene, isPause));
    }

    // シーンアンロード処理（本体）
    private IEnumerator UnloadSceneCoroutine(SceneList unloadScene, bool isPause)
    {
        // シーンアンロード実行
        _isRunning = true;

        // シーンのアンロード
        yield return SceneManager.UnloadSceneAsync(unloadScene.ToString());

        // ポーズ中の場合、ポーズ解除処理を実施する
        if (isPause)
        {
            Unpause();
        }

        // 現在のシーン名を設定
        _currentScene = _previousScene;

        // シーンアンロード完了のイベントを発行
        OnSceneUnloadComplete.Invoke();

        // シーンアンロード完了
        _isRunning = false;
    }

    // ポーズ処理
    public void Pause()
    {
        Time.timeScale = 0.0f;
        OnScenePause.Invoke();
    }

    // ポーズ解除処理
    public void Unpause()
    {
        OnSceneUnpouse.Invoke();
        Time.timeScale = 1.0f;
    }
}
