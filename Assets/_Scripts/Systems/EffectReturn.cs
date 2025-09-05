using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EffectReturn : MonoBehaviour
{
    public GameObject originalPrefab;
    [HideInInspector]
    public string originalPrefabName;
    public void Return()
    {
        if (!string.IsNullOrEmpty(originalPrefabName))
        {
            ObjectPoolManager.Instance.ReturnPoolName(originalPrefabName, this.gameObject);
            return;
        }
    }
}