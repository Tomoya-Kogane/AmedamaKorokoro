using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDirector : MonoBehaviour
{
    // プレイヤーライフ確認用の変数
    private PlayerBall _player;

    // UIモード管理用の変数
    // 1：飴玉モード
    // 2：目玉モード
    int mode;

    // UIオブジェクト操作用の変数
    // 飴玉UI
    GameObject objCandyUI;

    // 目玉UI
    GameObject objEyeUI1;
    GameObject objEyeUI2;

    // 初期処理
    void Start()
    {
        // プレイヤーのコンポーネントを取得
        _player = GameObject.Find("PlayerBall").GetComponent<PlayerBall>();
        // UIモードの初期化
        this.mode = 1;

        // 飴玉UIのオブジェクトを取得
        this.objCandyUI = GameObject.Find("CandyLife");

        // 目玉UIのオブジェクトを取得
        this.objEyeUI1 = GameObject.Find("EyeLife1");
        this.objEyeUI2 = GameObject.Find("EyeLife2");
        // 目玉UIの無効化
        this.objEyeUI1.SetActive(false);
        this.objEyeUI2.SetActive(false);

    }

    // 更新処理
    void Update()
    {
        switch (this.mode)
        {
            // 飴玉UI
            case 1:
                // 飴玉の耐久値によって、塗りつぶし領域を変更
                this.objCandyUI.GetComponent<Image>().fillAmount = _player.CandyLife * 0.2f;
                break;
            default:
                break;
        }
    }

    // UIモードの切り替え
    public void ChangeUI(int mode)
    {
        Animator obj;
        this.mode = mode;

        switch(this.mode)
        {
            // 飴玉UI
            case 1:
                // 飴玉UIを有効化
                this.objCandyUI.SetActive(true);

                // 目玉UIを無効化
                this.objEyeUI1.SetActive(false);
                this.objEyeUI2.SetActive(false);

                break;
            // 目玉UI1（片目閉じ）
            case 2:
                // 目玉UIを有効化
                this.objEyeUI1.SetActive(true);
                this.objEyeUI2.SetActive(true);

                // 目玉UI1の設定
                obj = this.objEyeUI1.GetComponent<Animator>();
                obj.SetFloat("AnimeSpeed", 0.0f);
                obj.Play("Base Layer.EyeUI", 0, 1.0f);

                // 目玉UI2の設定
                obj = this.objEyeUI2.GetComponent<Animator>();
                obj.SetFloat("AnimeSpeed", 0.0f);
                obj.Play("Base Layer.EyeUI", 0, 0.0f);

                // 飴玉UIを無効化
                this.objCandyUI.SetActive(false);
                break;
            // 目玉UI2（両目閉じ）
            case 3:
                // 目玉UIを有効化
                this.objEyeUI1.SetActive(true);
                this.objEyeUI2.SetActive(true);

                // 目玉UI1の設定
                obj = this.objEyeUI1.GetComponent<Animator>();
                obj.SetFloat("AnimeSpeed", 0.0f);
                obj.Play("Base Layer.EyeUI", 0, 1.0f);

                // 目玉UI2の設定
                obj = this.objEyeUI2.GetComponent<Animator>();
                obj.SetFloat("AnimeSpeed", 0.0f);
                obj.Play("Base Layer.EyeUI", 0, 1.0f);

                // 飴玉UIを無効化
                this.objCandyUI.SetActive(false);
                break;
            default:
                break;
        }
    }
}

