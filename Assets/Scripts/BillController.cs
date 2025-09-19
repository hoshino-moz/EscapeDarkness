using UnityEngine;

public class BillController : MonoBehaviour
{
    public float deleteTime = 2.0f;
    public GameObject barrierPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Invoke("FieldExpansion", deleteTime);
    }

    void FieldExpansion()
    {
        Instantiate(barrierPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            FieldExpansion();
        }
    }
}
