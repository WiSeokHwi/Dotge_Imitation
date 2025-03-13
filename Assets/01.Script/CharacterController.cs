using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CharacterController : MonoBehaviour
{
    public float walkSpeed = 15f;
    private float rotationSpeed = 700f;
    private Rigidbody rd;
    public GameObject shieldEffect;
    private bool isShieldActive = false;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rd = GetComponent<Rigidbody>();
        if (shieldEffect != null)
        {
            shieldEffect.SetActive(false); // ó������ ��ȣ�� ��Ȱ��ȭ
        }
    }
    void Update()
    {
        MovePlayer();
    }
    public void ShieldAtive()
    {
        if (shieldEffect != null)
        {
            shieldEffect.SetActive(true); // ���� ����Ʈ Ȱ��ȭ
            StartCoroutine(DisableShieldAfterDelay(10f)); // 10�� �ڿ� ���� ��Ȱ��ȭ
        }
    }

    private IEnumerator DisableShieldAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay - 2f); // 8�ʱ��� ��ٸ��� (2�� ������ ������ ����)

        // 2�� ������ �����̱� ����
        float blinkDuration = 2f; // ������ ���ӽð� (2��)
        float blinkInterval = 0.25f; // ������ ���� (0.5��)

        for (float t = 0; t < blinkDuration; t += blinkInterval)
        {
            shieldEffect.SetActive(!shieldEffect.activeSelf); // ������ ȿ�� 
            yield return new WaitForSeconds(blinkInterval); // 0.5�� �������� ������
        }

        // ���������� ���� ��Ȱ��ȭ
        shieldEffect.SetActive(false);
    
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");


        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;
        if (move.magnitude >= 0.1f)
        {
            rd.MovePosition(transform.position + move * walkSpeed * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            float speed = move.magnitude * walkSpeed;
            animator.SetFloat("speed", speed);

        }
        else
        {
            rd.linearVelocity = Vector3.zero;
            animator.SetFloat("speed", 0f);
        }


        //Vector3 move = new Vector3(moveX, 0, moveZ).normalized;

        //if (move.magnitude >= 0.1f)
        //{
        //    // ĳ���� ȸ�� ó��
        //    Quaternion targetRotation = Quaternion.LookRotation(move);
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        //    // ���������� �̵��ϱ� ���� velocity ���
        //    rd.linearVelocity = new Vector3(move.x * walkSpeed, rd.linearVelocity.y, move.z * walkSpeed);

        //    float speed = move.magnitude * walkSpeed;
        //    animator.SetFloat("speed", speed);
        //}
        //else
        //{
        //    // �̵��� ���� ��� �ӵ��� 0���� ����
        //    rd.linearVelocity = new Vector3(0, rd.linearVelocity.y, 0);
        //    animator.SetFloat("speed", 0f);
        //}
    }

}
