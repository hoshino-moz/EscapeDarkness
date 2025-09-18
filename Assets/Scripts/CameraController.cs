using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;

    public float followSpeed = 5.0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //�v���[���[�����擾
        player = GameObject.FindGameObjectWithTag("Player");

        //�X�^�[�g
        transform.position = new Vector3(
            player.transform.position.x,
            player.transform.position.y,
            -10);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player == null) return; //�Q�[���I�[�o�[���̃G���[���

        //�ڎw���ׂ��|�C���g
        Vector3 nextPos = new Vector3(player.transform.position.x, player.transform.position.y, -10);

        //����
        Vector3 nowPos = transform.position;

        transform.position = Vector3.Lerp( nowPos, nextPos, followSpeed * Time.deltaTime);


    }
}
