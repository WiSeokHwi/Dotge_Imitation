using System.Collections;
using UnityEngine;

public class EnemyCTRL : MonoBehaviour
{
    public GameObject bulletPrefab; // 총알 프리팹
    public Transform firePoint; // 총알 발사 위치
    public float bulletSpeed = 20f; // 총알 속도
    public float predictionTime = 0.2f; // 예측 시간
    private Vector3 lastPosition; // 캐릭터의 이전 프레임 위치

    private Transform target;
    private CharacterController character;
    public float minDistanceForPrediction = 4f; // 예측을 하지 않을 최소 거리


    void Start()
    {
        // 태그를 이용해 플레이어 찾기 
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        character = FindAnyObjectByType<CharacterController>();
        if (player != null)
        {
            target = player.transform;
            lastPosition = target.position; // 초기 위치 저장
            StartCoroutine(DelayedStart());
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // 플레이어와의 거리가 가까운 경우 예측을 하지 않음
            if (Vector3.Distance(transform.position, target.position) > minDistanceForPrediction)
            {
                // 플레이어 이동 방향 예측
                Vector3 predictedPosition = PredictTargetPosition(); // 예측 위치를 벡터에 대입

                // 적이 예측 위치를 바라보게 설정
                SmoothLookAt(predictedPosition);
            }
            else
            {
                // 가까이 있으면 예측하지 않고 바로 플레이어 위치로 향하게 설정
                SmoothLookAt(target.position);
            }
        }
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(2f); // startDelay 시간 동안 기다린 후
        StartCoroutine(FireRoutine()); // 총알 발사 루틴 시작
    }

    IEnumerator FireRoutine()
    {
        while (true)
        {
            if (target != null && !character.isGameOver)
            {
                // 플레이어와 가까운 경우 예측을 하지 않음
                if (Vector3.Distance(transform.position, target.position) > minDistanceForPrediction)
                {
                    Vector3 predictedPosition = PredictTargetPosition();
                    Shoot(predictedPosition);
                }
                else
                {
                    Shoot(target.position); // 가까운 거리에서는 바로 발사
                }
            }

            // 0.5초~2.0초 사이의 랜덤 시간 대기
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        }
    }
    Vector3 PredictTargetPosition()
    {
        // 직접 이동 속도를 계산
        Vector3 velocity = (target.position - lastPosition) / Time.deltaTime;
        lastPosition = target.position; // 현재 위치를 저장

        // 예측된 위치 = 현재 위치 + 이동 방향 * 예측 시간
        return target.position + velocity * predictionTime;
    }
    void SmoothLookAt(Vector3 targetPosition)
    {
        // 목표 방향 벡터 계산
        Vector3 direction = (targetPosition - transform.position).normalized;

        // 현재 회전값에서 목표 방향을 향해 회전값 계산
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        // 회전 속도 제한 (회전 속도가 너무 빠르지 않도록)
        float rotationSpeed = 5f;
        // Slerp로 부드럽게 회전 (회전 속도 조절 가능)
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
    void Shoot(Vector3 targetPosition)
    {        
        // 총알 방향 설정
        Vector3 shootDirection = (targetPosition - firePoint.position).normalized;
        shootDirection.y = 0;
        shootDirection.Normalize();

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Rigidbody로 총알 이동
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = shootDirection * bulletSpeed;
    }
}
