using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject objectToDuplicate;           // 복사할 프리팹 혹은 고정된 오브젝트
    public Vector3 spawnOffset = Vector3.zero;     // 스폰 위치 오프셋
    public float spawnInterval = 1.0f;              // 스폰 간격 (초)
    public Vector3 resetPosition = Vector3.zero;   // 다음 라운드 시작 시 복사 대상 위치 초기화

    private float nextSpawnTime = 1f;
    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Update()
    {
        if (Time.time >= nextSpawnTime && NEWUI.isBuyPanelInteractable == false)
        {
            DuplicateObject();
            nextSpawnTime = Time.time + spawnInterval;
        }

        if (NEWUI.isBuyPanelInteractable)
        {
            DestroyAllSpawnedObjectsAndReset();
        }
    }

    void DuplicateObject()
    {
        if (objectToDuplicate != null)
        {
            // 항상 스포너 위치 + 오프셋에서 복제 (복사 대상 위치는 움직이지 않아야 함)
            Vector3 spawnPosition = transform.position + spawnOffset;
            GameObject newObject = Instantiate(objectToDuplicate, spawnPosition, objectToDuplicate.transform.rotation);
            spawnedObjects.Add(newObject);
        }
    }

    void DestroyAllSpawnedObjectsAndReset()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
                Destroy(obj);
        }
        spawnedObjects.Clear();

        if (objectToDuplicate != null)
        {
            // 복사 대상(프리팹 위치) 초기화
            objectToDuplicate.transform.position = resetPosition;

            if (!objectToDuplicate.activeSelf)
                objectToDuplicate.SetActive(true);
        }
    }
}