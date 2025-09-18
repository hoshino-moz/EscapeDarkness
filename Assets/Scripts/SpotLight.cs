using UnityEngine;

public class SpotLight : MonoBehaviour
{
    public PlayerController playerCnt; //PlayerControllerコンポーネント
    public float rotationSpeed = 20.0f; //スポットライトの回転速度



    // Update is called once per frame
    void LateUpdate()
    {
        //寸前までのスポットライトの回転値(Z軸のみ)
        //float currentAngle = transform.eulerAngles.z;

        //プレイヤーの角度
        float targetAngle = playerCnt.angleZ;
        //ターゲットとなる角度を調整
        Quaternion targetRotation = Quaternion.Euler(0, 0, (targetAngle - 90));

        //現在の回転を今～ターゲットになるように滑らかに補完する Quaternion.Slerp メソッド
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    }
}
