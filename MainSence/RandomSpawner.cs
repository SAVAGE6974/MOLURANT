using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;  // 복사할 프리팹
    public int numberToSpawn = 10;    // 생성할 개수
    public Vector3 spawnAreaMin;      // 스폰 범위 최소값
    public Vector3 spawnAreaMax;      // 스폰 범위 최대값

    void Start()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                Random.Range(spawnAreaMin.z, spawnAreaMax.z)
            );

            Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
        }
    }
}