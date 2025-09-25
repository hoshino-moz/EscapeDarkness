using UnityEngine;

public class BillData : MonoBehaviour
{
    Rigidbody2D rbody;
    public int itemNum; //アイテムの識別番号
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>(); //Rigidbody2Dコンポーネントの取得
        rbody.bodyType = RigidbodyType2D.Static; //Rigidbodyの挙動を静止
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.bill ++; //1増やす
            GameManager.itemsPickedState[itemNum] = true; //該当する取得フラグをON

            //item取得演出
            gameObject.GetComponent<CircleCollider2D>().enabled = false; //①コライダーを無効化
            //Rigidbody2D itemBody = GetComponent<Rigidbody2D>();
            rbody.bodyType = RigidbodyType2D.Dynamic; //②Rigidbody2Dの復活（Dynamicにする）
            rbody.AddForce(new Vector2(0,5),ForceMode2D.Impulse); //③上に打ち上げ（上向き5の力）
            Destroy(gameObject, 0.5f); //④自分自身（オブジェクトごと）を抹消（0.5秒後）

        }
    }
}
