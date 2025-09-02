using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.Timeline;

public class SPBar : MonoBehaviour
{
    float _curLength;
    [SerializeField] RectTransform _Stretcher;
    [SerializeField] PlayerSO _Player;
    [SerializeField] float Test;
    Coroutine _coroutineController;
    IEnumerator AdjustHPBar(float currentHP, float targetHP)
    {
        int count = 1;
        Debug.Log(_Player.MaxHealth);
        float lengthCoefficient = 620f / _Player.MaxHealth;
        while (count <= 2.5f/Time.deltaTime)
        {
            float length = targetHP*lengthCoefficient + 10f / (0.5f*count+10f) * (currentHP-targetHP)*lengthCoefficient;
            Debug.Log(length + "," + _Player.CurHealth);
            _Stretcher.sizeDelta = new Vector2(length, 54f);
            _curLength = length;
            count++;
            yield return null;
        }
        _Stretcher.sizeDelta = new Vector2(targetHP*lengthCoefficient, 54f);
        _coroutineController = null;
    }

    public void Change()
    {
        if (_coroutineController != null) 
        { 
            StopCoroutine(_coroutineController);
            _coroutineController = StartCoroutine(AdjustHPBar(_curLength, Test));
        }
        _coroutineController = StartCoroutine(AdjustHPBar(_Player.CurHealth, Test));
    }
}