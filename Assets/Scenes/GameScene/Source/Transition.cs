using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// トランジションを実行するクラス
/// </summary>
public class Transition : MonoBehaviour
{
    // トランジション対象のマテリアル
    public Material transition1;

    // マテリアル操作用の変数
    private Material _material;

    // 実行中フラグ
    private bool _isRunning;

    // イベント定義
    [SerializeField]
    public UnityEvent OnTransition;
    [SerializeField]
    public UnityEvent OnComplete;

    // 初期処理
    void Start()
    {
        // トランジション対象のマテリアル取得
        _material = GetComponent<Image>().material;
        _material.SetFloat("_Alpha", 0.0f);

        // 実行中フラグの初期化
        _isRunning = false;
    }

    // 実行中フラグ取得処理
    public bool IsRunning()
    {
        return _isRunning;
    }

    // フェードイン処理
    public void FadeIn(float time)
    {
        StartCoroutine(FadeIn(transition1, time));
    }
    // トランジション実行のコルーチン（フェードインを実行）
    IEnumerator FadeIn(Material material, float time)
    {
        _isRunning = true;
        yield return ExecuteTrasition(material, 1, time);
        if (OnTransition != null)
        {
            OnTransition.Invoke();
        }
        if (OnComplete != null)
        {
            OnComplete.Invoke();
        }
        _isRunning = false;
    }

    // フェードアウト処理
    public void FadeOut(float time)
    {
        StartCoroutine(FadeOut(transition1, time));
    }

    // トランジション実行のコルーチン（フェードアウトを実行）
    IEnumerator FadeOut(Material material, float time)
    {
        _isRunning = true;
        yield return ExecuteTrasition(material, 0, time);
        if (OnTransition != null)
        {
            OnTransition.Invoke();
        }
        if (OnComplete != null)
        {
            OnComplete.Invoke();
        }
        _isRunning = false;
    }

    // フェード処理（アウトからイン）
    public void FadeOutIn(float time)
    {
        StartCoroutine(FadeOutIn(transition1, time));
    }

    // トランジション実行のコルーチン（フェードアウトからインを実行）
    IEnumerator FadeOutIn(Material material, float time)
    {
        _isRunning = true;
        yield return ExecuteTrasition(material, 0, time);
        if (OnTransition != null)
        {
            OnTransition.Invoke();
        }

        yield return new WaitForEndOfFrame();

        yield return ExecuteTrasition(material, 1, time);
        if (OnComplete != null)
        {
            OnComplete.Invoke();
        }
        _isRunning = false;
    }

    // トランジション実行のコルーチン（指定された秒数でトランジション実行）
    // 
    IEnumerator ExecuteTrasition(Material material, float fadeMode, float time)
    {
        _material = material;
        float current = 0;
        while (current < time)
        {
            _material.SetFloat("_Alpha", Mathf.Abs(fadeMode - (current / time)));
            yield return new WaitForEndOfFrame();
            current += Time.deltaTime;
        }
        _material.SetFloat("_Alpha", Mathf.Abs(fadeMode - 1));
    }
}
