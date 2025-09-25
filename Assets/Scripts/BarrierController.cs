using UnityEngine;

public class BarrierController : MonoBehaviour
{
    public float deleteTime = 5.0f; //Á–Å‚·‚é‚Ü‚Å‚ÌŠÔ


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SoundManager.instance.SEPlay(SEType.Barrier); //SE ƒTƒEƒ“ƒh

        //deleteTime•bŒã‚ÉÁ–Å
        Destroy(gameObject, deleteTime);
    }
}
