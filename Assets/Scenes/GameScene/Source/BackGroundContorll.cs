using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �w�i���Ǘ�����N���X
/// </summary>
public class BackGroundContorll : MonoBehaviour
{
    // �X�N���[���ʂ̐ݒ�
    [Range(0.0f,1.0f)]public float scrollAmount;

    // �J�����̍��W�擾�p
    GameObject mainCamera;
    Vector3 cameraPos;

    // ���g�̏������W�ۑ��p
    Vector3 myPos;

    // ��������
    void Start()
    {       
        // �J�����̃I�u�W�F�N�g�擾
        this.mainCamera = GameObject.Find("Main Camera");
        this.cameraPos = this.mainCamera.transform.position;

        // ���g�̏������W
        this.myPos = gameObject.transform.position;
    }

    // �X�V����
    void Update()
    {
        Vector3 distance = this.mainCamera.transform.position - this.cameraPos;
        gameObject.transform.position = new Vector3(myPos.x + (distance.x * scrollAmount), myPos.y + (distance.y * scrollAmount), myPos.z);
    }
}
