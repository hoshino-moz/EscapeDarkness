using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
    //ƒvƒŒƒCƒ„[‚ÆG‚ê‚½‚©

    //Scene‚ÌØ‚è‘Ö‚¦

}
