using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CharacterController : MonoBehaviour
{
    public float walkSpeed = 10f;
    private float rotationSpeed = 700f;
    private Rigidbody rd;
    public GameObject shieldEffect;
    public bool isShieldActive = false;
    public bool isGameOver = false;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rd = GetComponent<Rigidbody>();
        if (shieldEffect != null)
        {
            shieldEffect.SetActive(false); // 처음에는 보호막 비활성화
            isShieldActive = false;
        }
    }
    void Update()
    {
        if (isGameOver) return; 
        
        MovePlayer(); 
        
    }
    public void ShieldAtive()
    {
        
        if (shieldEffect != null)
        {
            shieldEffect.SetActive(true); // 쉴드 이펙트 활성화
            isShieldActive = true;
            StartCoroutine(DisableShieldAfterDelay(10f)); // 10초 뒤에 쉴드 비활성화
        }
        else
        {
            Debug.LogWarning("ShieldEffect is not assigned.");
        }
    }

    private IEnumerator DisableShieldAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay - 2f); // 8초까지 기다리기 (2초 전부터 깜빡임 시작)

        // 2초 전부터 깜빡이기 시작
        float blinkDuration = 2f; // 깜빡임 지속시간 (2초)
        float blinkInterval = 0.25f; // 깜빡임 간격 (0.5초)

        for (float t = 0; t < blinkDuration; t += blinkInterval)
        {
            if (!isShieldActive) break; // 쉴드가 비활성화되면 루프 탈출

            shieldEffect.SetActive(!shieldEffect.activeSelf); // 깜빡임 효과 
            yield return new WaitForSeconds(blinkInterval); // 0.5초 간격으로 깜빡임
        }

        // 최종적으로 쉴드 비활성화
        shieldEffect.SetActive(false);
        isShieldActive = false;

    }

    void MovePlayer() //이동 구현
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");


        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;
                
        if (move != Vector3.zero)
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
        //    // 캐릭터 회전 처리
        //    Quaternion targetRotation = Quaternion.LookRotation(move);
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        //    // 물리적으로 이동하기 위해 velocity 사용
        //    rd.linearVelocity = new Vector3(move.x * walkSpeed, rd.linearVelocity.y, move.z * walkSpeed);

        //    float speed = move.magnitude * walkSpeed;
        //    animator.SetFloat("speed", speed);
        //}
        //else
        //{
        //    // 이동이 없을 경우 속도를 0으로 설정
        //    rd.linearVelocity = new Vector3(0, rd.linearVelocity.y, 0);
        //    animator.SetFloat("speed", 0f);
        //}
    }
    public void gameOver()
    {
        if(!isGameOver)
        {
            isGameOver = true;
            animator.SetTrigger("Die");

            GameManager gameManager = FindAnyObjectByType<GameManager>();
            gameManager.GameOver();
        }
    }

}
