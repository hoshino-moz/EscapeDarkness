using UnityEngine;

public class BillController : MonoBehaviour
{
    public float deleteTime = 2.0f; //自動発動までの時間
    public GameObject barrierPrefab; //自己消滅と引き換えに生成するプレハブ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //deleteTime秒後に「バリア展開して消滅」
        Invoke("FieldExpansion", deleteTime);
    }

    //バリア展開と自己消滅を行うメソッド
    void FieldExpansion()
    {
        Instantiate(barrierPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    //敵とぶつかったらバリア発動
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            FieldExpansion();
        }
    }
}
