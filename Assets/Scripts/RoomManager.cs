using UnityEngine;

public class RoomManager : MonoBehaviour
{

    public static int[] doorsPositionNumber = { 0, 0, 0 }; // 各入口の配置番号
    public static int key1PositionNumber; // 鍵1 の配置番号
    public static int[] itemsPositionNumber = { 0, 0, 0, 0, 0 }; // アイテムの配置番号
    public GameObject[] items = new GameObject[5]; //5 つのアイテムプレハブの内訳
    public GameObject room; // ドアのプレハブ
    public GameObject dummyDoor; // ダミードアのプレハブ
    public GameObject key; // キーのプレハブ

    public static bool positioned; // 初回配置が済みかどうか
    public static string toRoomNumber = "fromRoom1"; //Player が配置されるべき位置

    GameObject player; //プレーヤー情報

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (!positioned)
        {
            StartKeysPosition(); //キーの初回配置
            StartItemsPosition();
            positioned = true;
        }
    }

    void StartKeysPosition()
    {
        //全Key1スポットの取得
        GameObject[] keySpots = GameObject.FindGameObjectsWithTag("KeySpot");

        //ランダムに番号を取得 (第1引数以上　第2引数未満)
        int rand = Random.Range(1, (keySpots.Length + 1));

        //全スポットをチェックしに行く
        foreach (GameObject spots in keySpots)
        {
            if (spots.GetComponent<KeySpot>().spotNum == rand)
            {
                Instantiate(key,spots.transform.position, Quaternion.identity);
                //どのスポット番号にキーを配置したかを記憶する
                key1PositionNumber = rand;
            }
        }

        GameObject keySpot;
        GameObject obj;

        keySpot = GameObject.FindGameObjectWithTag("KeySpot2");
        obj = Instantiate(key,keySpot.transform.position, Quaternion.identity);
        obj.GetComponent<KeyData>().keyType = KeyType.key2;

        keySpot = GameObject.FindGameObjectWithTag("KeySpot3");
        obj = Instantiate(key, keySpot.transform.position, Quaternion.identity);
        obj.GetComponent<KeyData>().keyType = KeyType.key3;
    }

    void StartItemsPosition()
    {
        GameObject[] itemSpots = GameObject.FindGameObjectsWithTag("ItemSpot");

        for(int i= 0; i < items.Length; i++)
        {
            //ランダムな数字の取得
            //同じ数字が出たら引き直し
            //スポットの全チェック
            //一致していればアイテムを生成
            //どのアイテムが生成されたかを記録
            //生成したアイテムに識別番号を割り振り
        }
    }

}
