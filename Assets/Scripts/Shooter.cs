using System;
using UnityEditor.Build;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    PlayerController playerCnt;

    public GameObject billPrefab;
    public float shootSpeed;
    public float shootDelay;
    bool inAttack;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCnt = GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        //�X�y�[�X�L�[
        if (Input.GetButtonDown("Jump")) Shoot();
    }

    public void Shoot()
    {
        if (inAttack || GameManager.bill <= 0) return;

        GameManager.bill--;
        inAttack = true;

        //�v���C���[�̊p�x�����
        float angleZ = playerCnt.angleZ;
        //Rotation�������� Quatanion�^�ɂ���
        Quaternion q = Quaternion.Euler(0, 0, angleZ);

        //���� �����D�A�v���C���[�̈ʒu�A�v���C���[�Ɠ����p�x
        GameObject obj = Instantiate(billPrefab ,transform.position, q);

        //���������I�u�W�F�N�g��Rigidbody2D�̏����擾
        Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>() ;

        //���������I�u�W�F�N�g�������ׂ����p�����
        float x = Mathf.Cos(angleZ * Mathf.Deg2Rad); //�p�x�ɑ΂����� X���̕���
        float y = Mathf.Sin(angleZ * Mathf.Deg2Rad); //�p�x�ɑ΂��鍂�� Y���̕���

        //�p�x�𕪉�����x��y�����Ƃɕ����f�[�^�Ƃ��Đ���
        Vector2 v = (new Vector2(x, y)).normalized * shootSpeed;

        //AddForce�Ŏw�肵�����p�ɔ�΂�
        rbody.AddForce(v, ForceMode2D.Impulse);

        //���ԍ��ōU�����t���O������
        Invoke("StopAttack", shootDelay);

    }

    void StopAttack()
    {
        inAttack = false; //�U�����t���O��OFF�ɂ���
    }
}
