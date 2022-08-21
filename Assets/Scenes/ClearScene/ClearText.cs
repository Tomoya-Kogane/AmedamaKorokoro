using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clear�������Ǘ�����N���X
/// </summary>
public class ClearText : MonoBehaviour
{
    // ��������
    void Start()
    {
        
    }

    // �X�V�����i���t���[���j
    void Update()
    {
        float cycle = Mathf.Sin(Time.time * 3);
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, cycle * 10);
        transform.localScale = new Vector3(Mathf.Abs(cycle) + 1, Mathf.Abs(cycle) + 1, 1.0f);
    }
}
