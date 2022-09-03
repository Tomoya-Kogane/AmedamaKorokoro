using UnityEngine;

public class GhostControll : MonoBehaviour
{
    // �H�쑀��p�̕ϐ�
    Rigidbody2D rigid2D;

    // �X�v���C�g�����_���[����p�̕ϐ�
    SpriteRenderer spriteRenderer;

    // �V�F�[�_�p�̕ϐ�
    public Material material1;
    public Material material2;
    private int _effect;

    // �A�j���[�V��������p�̕ϐ�
    Animator animator;
    private bool _isAnimation;

    // ���C���J�����̃I�u�W�F�N�g�ƃN���X�p�̕ϐ�
    GameObject mainCamera;
    CameraControll cameraControll;

    // �v���C���[�m�F�p�̕ϐ�
    GameObject player;

    // �G�΃t���O
    private bool _isEnemy;
    // �ړ����x
    const float WALK_FORCE = 8.0f;
    // �ړ����x�̐����l
    const float MAX_WALKSPEED = 4.0f;
    // �ړ������i�����l�E�����j
    private int _movedir = 1;

    // ����p
    const float VIEW_ANGLE = 45.0f;
    // ����͈�
    const float VIEW_RANGE = 5.0f;

    // �I�u�W�F�N�g�������ԊǗ��p�̕ϐ�
    const float LIVE_TIME = 10.0f;
    float deltaTime = 0.0f;

    // ��������
    void Start()
    {
        // �H�쑀��p�R���|�[�l���g�̎擾
        this.rigid2D = GetComponent<Rigidbody2D>();

        // �X�v���C�g�����_���[�̎擾
        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        // �A�j���[�V��������p�̃R���|�[�l���g���擾
        this.animator = GetComponent<Animator>();
        _isAnimation = false;

        // ���C���J�����̃I�u�W�F�N�g�̎擾
        this.mainCamera = GameObject.Find("Main Camera");
        this.cameraControll = GameObject.Find("Main Camera").GetComponent<CameraControll>();
        this.cameraControll.OnChangeEffect.AddListener((value) => { SetShader((int)value); });

        // �J�����̃G�t�F�N�g�ɉ������V�F�[�_��ݒ�
        _effect = this.cameraControll.Effect;
        switch (_effect)
        {
            case 2:
                this.spriteRenderer.material = this.material2;
                break;
            case 3:
                this.spriteRenderer.material = this.material1;
                break;
            default:
                this.spriteRenderer.material = this.material1;
                break;
        }

        // �v���C���[�̎擾
        this.player = GameObject.Find("PlayerBall");

        // �G�΃t���O�̏�����
        _isEnemy = false;
        // �ړ������Ɖ摜�̌�����ݒ�
        Vector3 scale = transform.localScale;
        if (transform.position.x >= this.mainCamera.transform.position.x)
        {
            _movedir = -1;
            scale.x = 1;
        }
        else
        {
            _movedir = 1;
            scale.x = -1;
        }
        transform.localScale = scale;
    }

    // �X�V����
    void Update()
    {
        Debug.Log(transform.forward);

        // �ړ�����
        Move();

        // ���E����
        CheckVisible();

        // ��莞�Ԍo�߂�����A���g��j��
        this.deltaTime += Time.deltaTime;
        if (this.deltaTime >= LIVE_TIME)
        {
            Destroy(gameObject);
        }
    }

    // �ړ�����
    private void Move()
    {
        // �ړ��x�N�g���p�̕ϐ�
        Vector3 moveForce = new Vector3(0.0f, 0.0f, 0.0f);

        // ���݂̈ړ����x���擾�i�w���Ƃx���j
        float ballSpeedX = Mathf.Abs(this.rigid2D.velocity.x);

        // �ړ����x�̐ݒ�i�w���j
        if (ballSpeedX < MAX_WALKSPEED)
        {
            moveForce = new Vector3(_movedir * WALK_FORCE, moveForce.y, moveForce.z);
        }

        // �ړ��x�N�g���̓K�p
        this.rigid2D.AddForce(moveForce);
    }

    // ���E����
    private void CheckVisible()
    {
        // �S�[�X�g�̍��W
        Vector2 ghostPos = new Vector2(transform.position.x, transform.position.y);
        // �S�[�X�g�̌���
        Vector2 ghostDir = new Vector2((float)_movedir, 0.0f);

        // �v���C���[�̍��W
        Vector2 playerPos = new Vector2(this.player.transform.position.x, this.player.transform.position.y);

        // �S�[�X�g�ƃv���C���[�̋����ƌ���
        Vector2 targetDir = playerPos - ghostPos;

        // ����p
        float viewAngle = Mathf.Cos(VIEW_ANGLE / 2 * Mathf.Deg2Rad);

        // �S�[�X�g�ƃv���C���[�̓��όv�Z
        float innerProduct = Vector2.Dot(ghostDir, targetDir.normalized);

        // ���E����
        if (innerProduct > viewAngle && targetDir.magnitude < VIEW_RANGE)
        {
            if (!_isAnimation)
            {
                // �A�j���[�V�������Đ�
                this.animator.SetFloat("AnimeSpeed", 1.0f);
                this.animator.Play("Base Layer.GhostHorror", 0, 0.0f);
                _isAnimation = true;
                _isEnemy = true;
            }
        }
    }

    private void SetShader(int effect)
    {
        _effect = effect;
        // �J�����̃G�t�F�N�g�ɉ������V�F�[�_��ݒ�
        switch (_effect)
        {
            case 2:
                this.spriteRenderer.material = this.material2;
                break;
            case 3:
                this.spriteRenderer.material = this.material1;
                break;
            default:
                break;
        }
    }

    // �v���p�e�B��`
    // �G�΃t���O
    public bool IsEnemy
    {
        get
        {
            // �J�����G�t�F�N�g�A���W�A�G�΃t���O�̏󋵂ɉ����āA�߂�l��ݒ�
            switch (_effect)
            {
                // �����G�t�F�N�g�̏ꍇ�A�G�΃t���O���I�t��Ԃ�
                case 1:
                    return false;
                // ��ʉE�����O���[�X�P�[���̏ꍇ�A��ʉE���ł͎��g�Őݒ肵���G�΃t���O��Ԃ�
                case 2:
                    if (this.mainCamera.transform.position.x < transform.position.x)
                    {
                        return _isEnemy;
                    }
                    else
                    {
                        return false;
                    }
                // �S�̂��O���[�X�P�[���̏ꍇ�A���g�Őݒ肵���G�΃t���O��Ԃ�
                case 3:
                    return _isEnemy;
                default:
                    return false;
            }
        }
    }
}