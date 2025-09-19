using UnityEngine;

public class BillData : MonoBehaviour
{
    Rigidbody2D rbody;
    public int itemNum; //�A�C�e���̎��ʔԍ�
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.bodyType = RigidbodyType2D.Static;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.bill ++;
            GameManager.itemsPickedState[itemNum] = true;

            //item�擾���o
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            //Rigidbody2D itemBody = GetComponent<Rigidbody2D>();
            rbody.bodyType = RigidbodyType2D.Dynamic;
            rbody.AddForce(new Vector2(0,5),ForceMode2D.Impulse);
            Destroy(gameObject, 0.5f);

        }
    }
}
