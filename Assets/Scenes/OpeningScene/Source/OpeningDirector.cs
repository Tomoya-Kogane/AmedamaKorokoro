using UnityEngine;

/// <summary>
/// �I�[�v�j���O�̋������Ǘ�����N���X
/// </summary>
public class OpeningDirector : MonoBehaviour
{
    // ��������
    void Start()
    {
        // ���t���b�V�����[�g��60�t���[���ɐݒ�
        Application.targetFrameRate = 60;
    }

    // �X�V����
    void Update()
    {
        // �X�y�[�X or ��ʃ^�b�v��GameScene�֖߂�
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            SceneMaster.instance.ChangeNextScene(SceneList.GameScene);
        }
    }
}
