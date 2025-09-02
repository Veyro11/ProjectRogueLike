using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolEvent : MonoBehaviour
{
    private EffectReturn _return;

    private void Awake()
    {
        _return = GetComponentInParent<EffectReturn>();
    }

    public void OnReturn()
    {
        _return.Return();
    }
}