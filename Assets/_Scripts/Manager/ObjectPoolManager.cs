using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    private Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();
    private Dictionary<string, GameObject> prefabReferences = new Dictionary<string, GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    // 풀링함수
    public GameObject GetFromPool(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        string prefabName = prefab.name;

        // 맞는 키값이 없으면 새로생성
        if (!poolDictionary.ContainsKey(prefabName))
        {
            poolDictionary.Add(prefabName, new Queue<GameObject>());
            prefabReferences.Add(prefabName, prefab);
        }

        Queue<GameObject> pool = poolDictionary[prefabName];

        // 풀에 재사용가능 오브젝트있으면 꺼내서 사용
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue(); // 풀에서 하나 꺼냄
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(prefab, position, rotation);

            // 생성된 오브젝트에 원본 프리팹 이름 설정
            EffectReturn effectReturn = obj.GetComponent<EffectReturn>();
            if (effectReturn != null)
            {
                effectReturn.originalPrefabName = prefabName;
            }
            return obj;
        }
    }

    public void ReturnPoolName(string prefabName, GameObject obj)
    {
        if (poolDictionary.ContainsKey(prefabName))
        {
            obj.SetActive(false);
            poolDictionary[prefabName].Enqueue(obj);
        }
        else
        {
            Destroy(obj);
        }
    }

}