using UnityEngine;

public class EnemyCTRL : MonoBehaviour
{
    private Transform target;
    
    void Start()
    {
        target = FindAnyObjectByType<CharacterController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
    }
}
