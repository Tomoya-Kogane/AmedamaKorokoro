using UnityEngine;
using UnityEditor;
using System.Collections;

/// <summary>
/// �O���b�h�̃p�����[�^�𐧌䂷��E�B���h�E
/// </summary>
public class GridEditWindow : EditorWindow  
{
    // NOTE: 
    // �e�ϐ���DrawGrid.cs�ŎQ�Ƃ��邽��static�ɂ���
    // �O���b�h��\�����邩�ǂ���
    public static bool isGridEnabled = false;
    // �O���b�h�̊J�n�n�_
    public static Vector2 originPos = Vector2.zero;
    // �O���b�h�̖ʐς̉��̒���
    public static int gridAreaX = 0;
    // �O���b�h�̖ʐς̏c�̒���
    public static int gridAreaY = 0;
    // �O���b�h�̕`��̊Ԋu
    public static float gridDistance = 0f;

    // �G�f�B�^�̏�̃��j���[�^�u����E�B���h�E���Ăяo����悤�ɂ���
    [MenuItem("GridEditorWindow/GridEditor")]
    static void Open()
    {
        EditorWindow.GetWindow<GridEditWindow>("GridEditor");
    }

    private void OnGUI()
    {
        // �O���b�h�̊J�n�n�_��ҏW�ł���悤�ɂ���
        originPos = EditorGUILayout.Vector2Field("Position", originPos);
        // �O���b�h�̉��̒�����ҏW�ł���悤�ɂ���
        gridAreaX = EditorGUILayout.IntField("�`�悷��X���W�͈̔�", gridAreaX);
        // �O���b�h�̏c�̒�����ҏW�ł���悤�ɂ���
        gridAreaY = EditorGUILayout.IntField("�`�悷��Y���W�͈̔�", gridAreaY);
        // �O���b�h�̊Ԋu��ҏW�ł���悤�ɂ���
        gridDistance = EditorGUILayout.FloatField("�`�悷��Ԋu", gridDistance);

        // �Ԃ�������
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        // �O���b�h��`�悷�邩�ǂ������`�F�b�N�{�b�N�X�ŕύX�ł���悤�ɂ���
        isGridEnabled = EditorGUILayout.Toggle("Grid�`��", isGridEnabled);
    }
}