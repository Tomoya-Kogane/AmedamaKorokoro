using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ���j���[���Ǘ�����N���X
/// </summary>
public class MenuDirector : MonoBehaviour
{
    // �J�[�\���̍��W�ݒ�
    public Vector3 continuePosition;
    public Vector3 quitPosition;

    // �J�[�\������p
    RectTransform cursor;

    // ���j���[���X�g
    private enum MenuList
    {
        Continue = 0,
        Quit = 1
    }
    private MenuList _menu;

    // ��������
    void Start()
    {
        // �J�[�\���̃R���|�[�l���g�擾
        this.cursor = GameObject.Find("Cursor").GetComponent<RectTransform>();
        // ���j���[�̏����l
        _menu = MenuList.Continue;
    }

    // �X�V����
    void Update()
    {
        // ����L�[����
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.cursor.anchoredPosition = continuePosition;
            _menu = MenuList.Continue;
        }

        // �����L�[����
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.cursor.anchoredPosition = quitPosition;
            _menu = MenuList.Quit;
        }

        // Space�L�[����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch(_menu)
            {
                case MenuList.Continue:
                    // �Q�[���V�[���֖߂�
                    break;
                case MenuList.Quit:
                    // �Q�[�����I������
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
