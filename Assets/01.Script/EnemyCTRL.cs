using System.Collections;
using UnityEngine;

public class EnemyCTRL : MonoBehaviour
{
    public GameObject bulletPrefab; // �Ѿ� ������
    public Transform firePoint; // �Ѿ� �߻� ��ġ
    public float bulletSpeed = 20f; // �Ѿ� �ӵ�
    public float predictionTime = 0.2f; // ���� �ð�
    private Vector3 lastPosition; // ĳ������ ���� ������ ��ġ

    private Transform target;
    private CharacterController character;
    public float minDistanceForPrediction = 4f; // ������ ���� ���� �ּ� �Ÿ�


    void Start()
    {
        // �±׸� �̿��� �÷��̾� ã�� 
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        character = FindAnyObjectByType<CharacterController>();
        if (player != null)
        {
            target = player.transform;
            lastPosition = target.position; // �ʱ� ��ġ ����
            StartCoroutine(DelayedStart());
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // �÷��̾���� �Ÿ��� ����� ��� ������ ���� ����
            if (Vector3.Distance(transform.position, target.position) > minDistanceForPrediction)
            {
                // �÷��̾� �̵� ���� ����
                Vector3 predictedPosition = PredictTargetPosition(); // ���� ��ġ�� ���Ϳ� ����

                // ���� ���� ��ġ�� �ٶ󺸰� ����
                SmoothLookAt(predictedPosition);
            }
            else
            {
                // ������ ������ �������� �ʰ� �ٷ� �÷��̾� ��ġ�� ���ϰ� ����
                SmoothLookAt(target.position);
            }
        }
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(2f); // startDelay �ð� ���� ��ٸ� ��
        StartCoroutine(FireRoutine()); // �Ѿ� �߻� ��ƾ ����
    }

    IEnumerator FireRoutine()
    {
        while (true)
        {
            if (target != null && !character.isGameOver)
            {
                // �÷��̾�� ����� ��� ������ ���� ����
                if (Vector3.Distance(transform.position, target.position) > minDistanceForPrediction)
                {
                    Vector3 predictedPosition = PredictTargetPosition();
                    Shoot(predictedPosition);
                }
                else
                {
                    Shoot(target.position); // ����� �Ÿ������� �ٷ� �߻�
                }
            }

            // 0.5��~2.0�� ������ ���� �ð� ���
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        }
    }
    Vector3 PredictTargetPosition()
    {
        // ���� �̵� �ӵ��� ���
        Vector3 velocity = (target.position - lastPosition) / Time.deltaTime;
        lastPosition = target.position; // ���� ��ġ�� ����

        // ������ ��ġ = ���� ��ġ + �̵� ���� * ���� �ð�
        return target.position + velocity * predictionTime;
    }
    void SmoothLookAt(Vector3 targetPosition)
    {
        // ��ǥ ���� ���� ���
        Vector3 direction = (targetPosition - transform.position).normalized;

        // ���� ȸ�������� ��ǥ ������ ���� ȸ���� ���
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        // ȸ�� �ӵ� ���� (ȸ�� �ӵ��� �ʹ� ������ �ʵ���)
        float rotationSpeed = 5f;
        // Slerp�� �ε巴�� ȸ�� (ȸ�� �ӵ� ���� ����)
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
    void Shoot(Vector3 targetPosition)
    {        
        // �Ѿ� ���� ����
        Vector3 shootDirection = (targetPosition - firePoint.position).normalized;
        shootDirection.y = 0;
        shootDirection.Normalize();

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Rigidbody�� �Ѿ� �̵�
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = shootDirection * bulletSpeed;
    }
}
