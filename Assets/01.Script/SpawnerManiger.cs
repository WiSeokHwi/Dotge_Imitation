using Unity.VisualScripting;
using UnityEngine;

public class SpawnerManiger : MonoBehaviour
{
    public GameObject Shild;
    public Vector2 spawnRangeX = new Vector2(-9f, 9f); // X�� ���� ����
    public Vector2 spawnRangeZ = new Vector2(-9f, 9f); // Z�� ���� ����
    public float spawnHeight = 1f; // Y��(����) ������
    
    void Start()
    {
        InvokeRepeating("SpawnItem",15f, Random.Range(30,80));
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnItem()
    {
        
        float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
        float randomZ = Random.Range(spawnRangeZ.x, spawnRangeZ.y);
        Vector3 spawnPoint = new Vector3(randomX, spawnHeight, randomZ);
        Instantiate(Shild, spawnPoint, Shild.transform.rotation);
        
    }

}
