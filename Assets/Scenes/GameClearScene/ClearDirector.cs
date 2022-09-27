using UnityEngine;

/// <summary>
/// �N���A�V�[�����Ǘ�����N���X
/// </summary>
public class ClearDirector : MonoBehaviour
{
    // �Q�[���N���A���̉摜�ݒ�p
    SpriteRenderer image;

    // ��������
    void Start()
    {
        // ���t���b�V�����[�g��60�t���[���ɐݒ�
        Application.targetFrameRate = 60;
        // �Q�[���N���A���̉摜��ݒ�
        image = GameObject.Find("GameSceneImage").GetComponent<SpriteRenderer>();
        image.sprite = SceneMaster.SceneData.GameDisplay;
    }

    // �X�V����
    void Update()
    {
        // �X�y�[�X or ��ʃ^�b�v��GameScene�֖߂�
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            SceneMaster.instance.ChangeNextScene(SceneList.GameMainScene);
        }
    }
}
