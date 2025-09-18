using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("�v���C���[�̊�b�X�e�[�^�X")]
    public float playerSpeed = 3.0f;

    float axisH;  //�������̓��͏�
    float axisV;  //�c�����̓��͏�

    [Header("�v���C���[�̊p�x�v�Z�p")]
    public float angleZ = -90f;  //�v���C���[�̊p�x�v�Z�p

    [Header("�X�|�b�g���C�g�@On/Off")]
    public GameObject spotLight;  //�Ώۂ̃X�|�b�g���C�g

    bool inDamage;   //�_���[�W�����ǂ����̃t���O�Ǘ�

    //�R���|�[�l���g
    Rigidbody2D rbody;
    Animator anime;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //�R���|�[�l���g�̎擾
        rbody = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();

        //�X�|�b�g���C�g���������Ă���Ε\��
        if (GameManager.hasSpotLight)
        {
            spotLight.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameState != GameState.playing) return;

        Move();  //�㉺���E�̓��͒l�̎擾
        angleZ = GetAngle(); //���̎��̊p�x��ϐ�angleZ�ɔɉh
        Animation();  //angleZ�𗘗p���ăA�j���[�V����
    }

    private void FixedUpdate()
    {
        //�v���C���łȂ�Ή������Ȃ�
        if (GameManager.gameState != GameState.playing) return;

        //�_���[�W�t���O�������Ă����
        if (inDamage)
        {
            //�_�ŉ��o
            //Sin���\�b�h�̊p�x
            float val = Mathf.Sin(Time.time * 50);

            if (val > 0)
            {
                //�`��@�\��L��
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                //�`��@�\�𖳌�
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }

                //���͂ɂ��velocity�������Ȃ��悤�ɂ���
                return;
        }

        //���͏󋵂ɉ�����Player�𓮂���
        rbody.linearVelocity = (new Vector2(axisH, axisV)).normalized * playerSpeed;
    }

    //�㉺���E�̓��͒l�̎擾
    public void Move()
    {
        //axisH �� axisV ���͏�Ԏ擾
        axisH = Input.GetAxisRaw("Horizontal");
        axisV = Input.GetAxisRaw("Vertical");
    }

    //�v���C���[�̌����Ă���p�x���擾
    public float GetAngle()
    {
        //���ݍ��W�̎擾
        Vector2 fromPos = transform.position;

        //���̏u�Ԃ̃L�[���͂ɉ������\�����W�̎擾
        Vector2 toPos = new Vector2(fromPos.x + axisH, fromPos.y + axisV);

        float angle;  //return�����l�̏���

        //���͏�񂠂�Ίp�x�Y�o
        if (axisH != 0 || axisV != 0)
        {
            float dirX = toPos.x - fromPos.x;
            float dirY = toPos.y - fromPos.y;

            //�������@�����@�������@��Ӂ@���W�A���ŏo��
            float rad = Mathf.Atan2(dirY, dirX);

            //�p�x�\�L�ϊ�
            angle = rad * Mathf.Rad2Deg;
        }
        else  //�������͂���Ă��Ȃ���� �O�t���[���̊p�x���𐘂��u��
        {
            angle = angleZ;
        }

        return angle;
    }

    void Animation()
    {
        //���͂�����ꍇ
        if (axisH != 0 || axisV != 0)
        {
            //�ЂƂ܂�Run�A�j���𑖂点��
            anime.SetBool("run", true);

            //angleZ���g���ĕ��p�����߂�@direction int�^
            //int�^��direction ��:0 ��:1 �E:2 ��:����ȊO

            if (angleZ > -135f && angleZ < -45f)  //��
            {
                anime.SetInteger("direction", 0);
            }
            else if (angleZ >= -45f && angleZ <= 45f)  //�E
            {
                anime.SetInteger("direction", 2);
                transform.localScale = new Vector2(1, 1);
            }
            else if (angleZ > 45f && angleZ < 135f)  //��
            {
                anime.SetInteger("direction", 1);
            }
            else                                    //��
            {
                anime.SetInteger("direction", 3);
                transform.localScale = new Vector2(-1, 1);
            }
        }
        else //�������͂��Ȃ��ꍇ
        {
            anime.SetBool("run", false); //����t���O��OFF
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�Ԃ��������肪Enemy��������
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetDamage(collision.gameObject); 
        }
    }

    void GetDamage(GameObject enemy)
    {
        if (GameManager.gameState != GameState.playing)  return;

        GameManager.playerHP--;

        if(GameManager.playerHP > 0)
        {
            //�v���C���[�̓�������U�X�g�b�v
            rbody.linearVelocity = Vector2.zero;
            //���������߂�//�v���C���[�ƓG�Ƃ̍����擾���A���������߂�
            Vector3 v = (transform.position - enemy.transform.position).normalized ;
            rbody.AddForce(v *4, ForceMode2D.Impulse);

            //�_�ł����邽�߂̃t���O
            inDamage = true;

            //���ԍ��œ_�Ńt���O����
            Invoke("DamageEnd", 0.25f);
        }
        else
        {
            //�c��HP��������΃Q�[���I�[�o�[
            GameOver();
        }
    }

    void DamageEnd()
    {
        inDamage = false; //�_�Ńt���O������
        gameObject.GetComponent<SpriteRenderer>().enabled = true;  //�v���C���[���m���ɕ\��
    }

    void GameOver()
    {
        //��Ԃ��Q�[���I�[�o�[��
        GameManager.gameState = GameState.gameover;

        //���o
        GetComponent<CircleCollider2D>().enabled = false;  //�����蔻��̖�����
        rbody.linearVelocity = Vector2.zero; //�������~�߂�
        rbody.gravityScale = 1.0f; //�d�͂̕���
        anime.SetTrigger("dead");  //���S�̃A�j���N���b�v�̔���
        rbody.AddForce(new Vector2(0,5), ForceMode2D.Impulse); //��ɒ��ˏグ��
        Destroy(gameObject, 1.0f); //1�b��ɑ��݂�����
    }

}
