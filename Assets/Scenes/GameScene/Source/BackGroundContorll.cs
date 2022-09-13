using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背景を管理するクラス
/// </summary>
public class BackGroundContorll : MonoBehaviour
{
    // スクロール量の設定
    [Range(0.0f,1.0f)]public float scrollAmount;

    // カメラの座標取得用
    GameObject mainCamera;
    Vector3 cameraPos;

    // 自身の初期座標保存用
    Vector3 myPos;

    // 初期処理
    void Start()
    {       
        // カメラのオブジェクト取得
        this.mainCamera = GameObject.Find("Main Camera");
        this.cameraPos = this.mainCamera.transform.position;

        // 自身の初期座標
        this.myPos = gameObject.transform.position;
    }

    // 更新処理
    void Update()
    {
        Vector3 distance = this.mainCamera.transform.position - this.cameraPos;
        gameObject.transform.position = new Vector3(myPos.x + (distance.x * scrollAmount), myPos.y + (distance.y * scrollAmount), myPos.z);
    }
}
