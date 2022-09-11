using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// メニューを管理するクラス
/// </summary>
public class MenuDirector : MonoBehaviour
{
    // カーソルの座標設定
    public Vector3 continuePosition;
    public Vector3 quitPosition;

    // カーソル操作用
    RectTransform cursor;

    // メニューリスト
    private enum MenuList
    {
        Continue = 0,
        Quit = 1
    }
    private MenuList _menu;

    // 初期処理
    void Start()
    {
        // カーソルのコンポーネント取得
        this.cursor = GameObject.Find("Cursor").GetComponent<RectTransform>();
        // メニューの初期値
        _menu = MenuList.Continue;
    }

    // 更新処理
    void Update()
    {
        // 上矢印キー押下
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.cursor.anchoredPosition = continuePosition;
            _menu = MenuList.Continue;
        }

        // 下矢印キー押下
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.cursor.anchoredPosition = quitPosition;
            _menu = MenuList.Quit;
        }

        // Spaceキー押下
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch(_menu)
            {
                case MenuList.Continue:
                    // ゲームシーンへ戻る
                    break;
                case MenuList.Quit:
                    // ゲームを終了する
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                    break;
                default:
                    break;
            }
        }
    }
}
