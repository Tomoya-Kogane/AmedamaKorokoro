using UnityEngine;
using UnityEditor;
using System.Collections;

/// <summary>
/// UnityEditor�ŃO���b�h��`�悵�I�u�W�F�N�g���X�i�b�v������N���X
/// </summary>
[UnityEditor.CustomEditor(typeof(Transform))]
public class DrawGrid : Editor
{

    void OnSceneGUI()
    {
        // �O���b�h���\���ɂ���ꍇ�͏������~�߂�
        if (!GridEditWindow.isGridEnabled)
        {
            return;
        }

        // �O���b�h�̐F�B
        Color color;

        // �O���b�h�`��̊J�n�n�_��GridEditWindow����Q��
        Vector3 originPos = GridEditWindow.originPos;

        // �O���b�h�`��̊Ԋu
        float gridDistance = GridEditWindow.gridDistance;

        // �O���b�h�̖{���@���@�ʐρ��Ԋu
        float numX = GridEditWindow.gridAreaX / gridDistance;
        float numY = GridEditWindow.gridAreaY / gridDistance;

        // �c����`��B�J�n�ʒu����1�{���E�ɕ`�悵�Ă����B
        for (int x = 0; x <= numX; x++)
        {
            // ���̊J�n�ʒu���v�Z�B
            Vector3 pos = originPos + Vector3.right * x * gridDistance;

            // 5�̔{���͔��ɂ��Ėڗ�������
            if (x % 5 == 0)
            {
                // 0.7�������ĐF�𗎂���������
                color = Color.white * 0.7f;
            }
            // ����ȊO�͐��F
            else
            {
                color = Color.cyan * 0.7f;
            }

            // ����`��
            Debug.DrawLine(pos, pos + Vector3.up * GridEditWindow.gridAreaY, color);
        }

        // ������`��B�J�n�ʒu����1�{����ɕ`�悵�Ă����B
        for (int y = 0; y <= numY; y++)
        {
            Vector3 pos = originPos + Vector3.up * y * gridDistance;

            if (y % 5 == 0)
            {
                color = Color.white * 0.7f;
            }
            else
            {
                color = Color.cyan * 0.7f;
            }

            Debug.DrawLine(pos, pos + Vector3.right * GridEditWindow.gridAreaX, color);
        }

        // �O���b�h�ɂ҂�����ƍ��킹��I�u�W�F�N�g�̃g�����X�t�H�[�����擾
        Transform objectTransform = target as Transform;

        // �I�u�W�F�N�g���X�i�b�v�����鏈��
        Vector3 objectPosition = objectTransform.transform.position;
        objectPosition.x = Mathf.Floor(objectPosition.x / gridDistance) * gridDistance;
        objectPosition.y = Mathf.Floor(objectPosition.y / gridDistance) * gridDistance;
        objectTransform.transform.position = objectPosition;

        // �ĕ`��
        EditorUtility.SetDirty(target);
    }
}