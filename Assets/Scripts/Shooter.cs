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
        //スペースキー
        if (Input.GetButtonDown("Jump")) Shoot();
    }

    public void Shoot()
    {
        if (inAttack || GameManager.bill <= 0) return;

        GameManager.bill--;
        inAttack = true;

        //プレイヤーの角度を入手
        float angleZ = playerCnt.angleZ;
        //Rotationが扱える Quatanion型にする
        Quaternion q = Quaternion.Euler(0, 0, angleZ);

        //生成 ※お札、プレイヤーの位置、プレイヤーと同じ角度
        GameObject obj = Instantiate(billPrefab ,transform.position, q);

        //生成したオブジェクトのRigidbody2Dの情報を取得
        Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>() ;

        //生成したオブジェクトが向くべき方角を入手
        float x = Mathf.Cos(angleZ * Mathf.Deg2Rad); //角度に対する底辺 X軸の方向
        float y = Mathf.Sin(angleZ * Mathf.Deg2Rad); //角度に対する高さ Y軸の方向

        //角度を分解したxとyをもとに方向データとして整理
        Vector2 v = (new Vector2(x, y)).normalized * shootSpeed;

        //AddForceで指定した方角に飛ばす
        rbody.AddForce(v, ForceMode2D.Impulse);

        //時間差で攻撃中フラグを解除
        Invoke("StopAttack", shootDelay);

    }

    void StopAttack()
    {
        inAttack = false; //攻撃中フラグをOFFにする
    }
}
