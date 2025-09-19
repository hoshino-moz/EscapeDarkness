using UnityEngine;

//
public enum KeyType
{
    key1, key2, key3,
}

public class KeyData : MonoBehaviour
{
    public KeyType keyType = KeyType.key1;
    Rigidbody2D rbody;
    
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
            switch (keyType)
            {
                case KeyType.key1:
                    GameManager.key1++;
                    GameManager.keysPickedState[0] = true;
                    break;
                case KeyType.key2:
                    GameManager.key1++;
                    GameManager.keysPickedState[1] = true;
                    break;
                case KeyType.key3:
                    GameManager.key1++;
                    GameManager.keysPickedState[2] = true;
                    break;
            }

            //éÊìæââèo
            GetComponent<CircleCollider2D>().enabled = false;
            rbody.bodyType = RigidbodyType2D.Dynamic;
            rbody.AddForce(new Vector2 (0,5), ForceMode2D.Impulse);
            Destroy(gameObject, 0.5f);

        }
    }

}
