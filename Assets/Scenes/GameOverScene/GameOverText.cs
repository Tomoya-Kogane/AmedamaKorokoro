using UnityEngine;

/// <summary>
/// GameOver�������Ǘ�����N���X
/// </summary>
public class GameOverText : MonoBehaviour
{
    // �X�V����
    void Update()
    {
        // �e�L�X�g�̊g��k�����J��Ԃ�
        float cycle = Mathf.Sin(Time.time * 3);
        transform.localScale = new Vector3(Mathf.Abs(cycle) + 1, Mathf.Abs(cycle) + 1, 1.0f);
    }
}
