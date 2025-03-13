using UnityEngine;

public class Shild_item : MonoBehaviour
{

    void Start()
    {
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 60*Time.deltaTime, 0);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾�� �浹 ��
        {
            CharacterController player = other.GetComponent<CharacterController>();

            if (player != null)
            {
                player.ShieldAtive(); // ���� Ȱ��ȭ
            }

            Destroy(gameObject); // ������ ����
        }
    }
}
