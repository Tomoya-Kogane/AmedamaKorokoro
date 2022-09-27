using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// PressKey�e�L�X�g�̋������Ǘ�����N���X
/// </summary>
public class PressKeyText : MonoBehaviour
{
    // �e�L�X�g�̓��ߑ���p
    TextMeshProUGUI textMesh;
    Color textColor;

    // �e�L�X�g�_���܂ł̑ҋ@����
    const float START_LIGHTING = 3.0f;
    private float _countTime = 0.0f;

    // ��������
    void Start()
    {
        // �e�L�X�g�̃R���|�[�l���g�擾
        this.textMesh = gameObject.GetComponent<TextMeshProUGUI>();
        this.textColor = this.textMesh.color;
        this.textMesh.color = new Color(this.textColor.r, this.textColor.g, this.textColor.b, 0.0f);
    }

    // �X�V����
    void Update()
    {
        // �ҋ@���Ԃ𒴂�����A�e�L�X�g�̓_�������{
        _countTime += Time.deltaTime;
        if (_countTime > START_LIGHTING)
        {
            float cycle = Mathf.Cos(Time.time);
            this.textMesh.color = new Color(this.textColor.r, this.textColor.g, this.textColor.b, Mathf.Abs(cycle));
        }
    }
}
