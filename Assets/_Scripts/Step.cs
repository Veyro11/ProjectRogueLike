using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour
{
    void StepSFX()
    {
        AudioManager.Instance.PlaySFX("Grass" + Random.Range(1, 6));
    }
}
