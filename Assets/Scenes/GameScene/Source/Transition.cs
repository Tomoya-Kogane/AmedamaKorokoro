using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// �g�����W�V���������s����N���X
/// </summary>
public class Transition : MonoBehaviour
{
    // �g�����W�V�����Ώۂ̃}�e���A��
    public Material transition1;

    // �}�e���A������p�̕ϐ�
    private Material _material;

    // ���s���t���O
    private bool _isRunning;

    // �C�x���g��`
    [SerializeField]
    public UnityEvent OnTransition;
    [SerializeField]
    public UnityEvent OnComplete;

    // ��������
    void Start()
    {
        // �g�����W�V�����Ώۂ̃}�e���A���擾
        _material = GetComponent<Image>().material;
        _material.SetFloat("_Alpha", 0.0f);

        // ���s���t���O�̏�����
        _isRunning = false;
    }

    // ���s���t���O�擾����
    public bool IsRunning()
    {
        return _isRunning;
    }

    // �t�F�[�h�C������
    public void FadeIn(float time)
    {
        StartCoroutine(FadeIn(transition1, time));
    }
    // �g�����W�V�������s�̃R���[�`���i�t�F�[�h�C�������s�j
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

    // �t�F�[�h�A�E�g����
    public void FadeOut(float time)
    {
        StartCoroutine(FadeOut(transition1, time));
    }

    // �g�����W�V�������s�̃R���[�`���i�t�F�[�h�A�E�g�����s�j
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

    // �t�F�[�h�����i�A�E�g����C���j
    public void FadeOutIn(float time)
    {
        StartCoroutine(FadeOutIn(transition1, time));
    }

    // �g�����W�V�������s�̃R���[�`���i�t�F�[�h�A�E�g����C�������s�j
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

    // �g�����W�V�������s�̃R���[�`���i�w�肳�ꂽ�b���Ńg�����W�V�������s�j
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
