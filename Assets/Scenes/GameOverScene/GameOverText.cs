using UnityEngine;

/// <summary>
/// GameOver文字を管理するクラス
/// </summary>
public class GameOverText : MonoBehaviour
{
    // 更新処理
    void Update()
    {
        // テキストの拡大縮小を繰り返す
        float cycle = Mathf.Sin(Time.time * 3);
        transform.localScale = new Vector3(Mathf.Abs(cycle) + 1, Mathf.Abs(cycle) + 1, 1.0f);
    }
}
