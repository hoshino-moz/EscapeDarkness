using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("プレイヤーの基礎ステータス")]
    public float playerSpeed = 3.0f;

    float axisH;  //横方向の入力状況
    float axisV;  //縦方向の入力状況

    [Header("プレイヤーの角度計算用")]
    public float angleZ = -90f;  //プレイヤーの角度計算用

    [Header("スポットライト　On/Off")]
    public GameObject spotLight;  //対象のスポットライト

    bool inDamage;   //ダメージ中かどうかのフラグ管理

    //コンポーネント
    Rigidbody2D rbody;
    Animator anime;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //コンポーネントの取得
        rbody = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();

        //スポットライトを所持していれば表示
        if (GameManager.hasSpotLight)
        {
            spotLight.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameState != GameState.playing) return;

        Move();  //上下左右の入力値の取得
        angleZ = GetAngle(); //その時の角度を変数angleZに繁栄
        Animation();  //angleZを利用してアニメーション
    }

    private void FixedUpdate()
    {
        //プレイ中でなれば何もしない
        if (GameManager.gameState != GameState.playing) return;

        //ダメージフラグが立っている間
        if (inDamage)
        {
            //点滅演出
            //Sinメソッドの角度
            float val = Mathf.Sin(Time.time * 50);

            if (val > 0)
            {
                //描画機能を有効
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                //描画機能を無効
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }

                //入力によるvelocityが働かないようにする
                return;
        }

        //入力状況に応じてPlayerを動かす
        rbody.linearVelocity = (new Vector2(axisH, axisV)).normalized * playerSpeed;
    }

    //上下左右の入力値の取得
    public void Move()
    {
        //axisH と axisV 入力状態取得
        axisH = Input.GetAxisRaw("Horizontal");
        axisV = Input.GetAxisRaw("Vertical");
    }

    //プレイヤーの向いている角度を取得
    public float GetAngle()
    {
        //現在座標の取得
        Vector2 fromPos = transform.position;

        //その瞬間のキー入力に応じた予測座標の取得
        Vector2 toPos = new Vector2(fromPos.x + axisH, fromPos.y + axisV);

        float angle;  //returnされる値の準備

        //入力情報あれば角度産出
        if (axisH != 0 || axisV != 0)
        {
            float dirX = toPos.x - fromPos.x;
            float dirY = toPos.y - fromPos.y;

            //第一引数　高さ　第二引数　底辺　ラジアンで出力
            float rad = Mathf.Atan2(dirY, dirX);

            //角度表記変換
            angle = rad * Mathf.Rad2Deg;
        }
        else  //何も入力されていなければ 前フレームの角度情報を据え置き
        {
            angle = angleZ;
        }

        return angle;
    }

    void Animation()
    {
        //入力がある場合
        if (axisH != 0 || axisV != 0)
        {
            //ひとまずRunアニメを走らせる
            anime.SetBool("run", true);

            //angleZを使って方角を決める　direction int型
            //int型のdirection 下:0 上:1 右:2 左:それ以外

            if (angleZ > -135f && angleZ < -45f)  //下
            {
                anime.SetInteger("direction", 0);
            }
            else if (angleZ >= -45f && angleZ <= 45f)  //右
            {
                anime.SetInteger("direction", 2);
                transform.localScale = new Vector2(1, 1);
            }
            else if (angleZ > 45f && angleZ < 135f)  //上
            {
                anime.SetInteger("direction", 1);
            }
            else                                    //左
            {
                anime.SetInteger("direction", 3);
                transform.localScale = new Vector2(-1, 1);
            }
        }
        else //何も入力がない場合
        {
            anime.SetBool("run", false); //走るフラグをOFF
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ぶつかった相手がEnemyだったら
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
            //プレイヤーの動きを一旦ストップ
            rbody.linearVelocity = Vector2.zero;
            //方向を決める//プレイヤーと敵との差を取得し、方向を決める
            Vector3 v = (transform.position - enemy.transform.position).normalized ;
            rbody.AddForce(v *4, ForceMode2D.Impulse);

            //点滅させるためのフラグ
            inDamage = true;

            //時間差で点滅フラグ解除
            Invoke("DamageEnd", 0.25f);
        }
        else
        {
            //残りHPが無ければゲームオーバー
            GameOver();
        }
    }

    void DamageEnd()
    {
        inDamage = false; //点滅フラグを解除
        gameObject.GetComponent<SpriteRenderer>().enabled = true;  //プレイヤーを確実に表示
    }

    void GameOver()
    {
        //状態をゲームオーバーに
        GameManager.gameState = GameState.gameover;

        //演出
        GetComponent<CircleCollider2D>().enabled = false;  //当たり判定の無効か
        rbody.linearVelocity = Vector2.zero; //動きを止める
        rbody.gravityScale = 1.0f; //重力の復活
        anime.SetTrigger("dead");  //死亡のアニメクリップの発動
        rbody.AddForce(new Vector2(0,5), ForceMode2D.Impulse); //上に跳ね上げる
        Destroy(gameObject, 1.0f); //1秒後に存在を消去
    }

}
