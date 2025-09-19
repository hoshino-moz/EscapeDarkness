using UnityEngine;

public class RoomManager : MonoBehaviour
{

    public static int[] doorsPositionNumber = { 0, 0, 0 }; // �e�����̔z�u�ԍ�
    public static int key1PositionNumber; // ��1 �̔z�u�ԍ�
    public static int[] itemsPositionNumber = { 0, 0, 0, 0, 0 }; // �A�C�e���̔z�u�ԍ�
    public GameObject[] items = new GameObject[5]; //5 �̃A�C�e���v���n�u�̓���
    public GameObject room; // �h�A�̃v���n�u
    public GameObject dummyDoor; // �_�~�[�h�A�̃v���n�u
    public GameObject key; // �L�[�̃v���n�u

    public static bool positioned; // ����z�u���ς݂��ǂ���
    public static string toRoomNumber = "fromRoom1"; //Player ���z�u�����ׂ��ʒu

    GameObject player; //�v���[���[���

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (!positioned)
        {
            StartKeysPosition(); //�L�[�̏���z�u
            StartItemsPosition();
            positioned = true;
        }
    }

    void StartKeysPosition()
    {
        //�SKey1�X�|�b�g�̎擾
        GameObject[] keySpots = GameObject.FindGameObjectsWithTag("KeySpot");

        //�����_���ɔԍ����擾 (��1�����ȏ�@��2��������)
        int rand = Random.Range(1, (keySpots.Length + 1));

        //�S�X�|�b�g���`�F�b�N���ɍs��
        foreach (GameObject spots in keySpots)
        {
            if (spots.GetComponent<KeySpot>().spotNum == rand)
            {
                Instantiate(key,spots.transform.position, Quaternion.identity);
                //�ǂ̃X�|�b�g�ԍ��ɃL�[��z�u���������L������
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
            //�����_���Ȑ����̎擾
            //�����������o�����������
            //�X�|�b�g�̑S�`�F�b�N
            //��v���Ă���΃A�C�e���𐶐�
            //�ǂ̃A�C�e�����������ꂽ�����L�^
            //���������A�C�e���Ɏ��ʔԍ�������U��
        }
    }

}
