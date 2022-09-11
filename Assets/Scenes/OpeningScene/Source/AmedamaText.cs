using UnityEngine;

/// <summary>
/// Amedamaテキストの挙動を管理するクラス
/// </summary>
public class AmedamaText : MonoBehaviour
{
    // 初期位置
    private Vector3 _startPos;

    // 初期処理
    void Start()
    {
        // 初期位置を保存
        _startPos = transform.position;
    }

    // 更新処理
    void Update()
    {
        // テキストを左右に跳ねさせる
        float cycleRL = Mathf.Sin(Time.time * 2) * 100;
        float cycleUD = Mathf.Sin(Time.time * 4) * 100;
        if (cycleUD < 0) cycleUD *= -1.0f;
        transform.position = new Vector3(_startPos.x + cycleRL, _startPos.y + cycleUD, _startPos.z);
    }
}
