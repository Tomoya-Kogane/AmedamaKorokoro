using UnityEngine;

/// <summary>
/// Amedama�e�L�X�g�̋������Ǘ�����N���X
/// </summary>
public class AmedamaText : MonoBehaviour
{
    // �����ʒu
    private Vector3 _startPos;

    // ��������
    void Start()
    {
        // �����ʒu��ۑ�
        _startPos = transform.position;
    }

    // �X�V����
    void Update()
    {
        // �e�L�X�g�����E�ɒ��˂�����
        float cycleRL = Mathf.Sin(Time.time * 2) * 100;
        float cycleUD = Mathf.Sin(Time.time * 4) * 100;
        if (cycleUD < 0) cycleUD *= -1.0f;
        transform.position = new Vector3(_startPos.x + cycleRL, _startPos.y + cycleUD, _startPos.z);
    }
}
