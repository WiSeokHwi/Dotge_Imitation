using UnityEngine;

public class Bullet_SRT : MonoBehaviour
{
    
    
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어와 충돌 시
        {
            CharacterController player = other.GetComponent<CharacterController>();
            if(player.isShieldActive == true)
            {
                player.isShieldActive = false;
                player.shieldEffect.SetActive(false);
                Destroy(gameObject);
            }
            else 
            {
                player.gameOver();
            }
        }
        if(other.tag == "wall")
        {
            Destroy(gameObject);
        }
    }
    
}
