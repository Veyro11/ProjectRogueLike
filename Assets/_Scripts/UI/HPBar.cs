using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.Timeline;

public class HPBar : MonoBehaviour
{
    int _maxLength = 840;
    [SerializeField] RectTransform _Stretcher;
    [SerializeField] PlayerStatus _Player;
    Coroutine _coroutineController;
    IEnumerator AdjustHPBar(float currentHP, float targetHP)
    {
        int count = 1;
        Debug.Log(_Player.MaxHealth);
        float lengthCoefficient = 840f / _Player.MaxHealth;
        while (count <= 2.5f/Time.deltaTime)
        {
            float length = targetHP*lengthCoefficient + 10f / (0.5f*count+10f) * (currentHP-targetHP)*lengthCoefficient;
            Debug.Log(length + "," + _Player.CurHealth);
            _Stretcher.sizeDelta = new Vector2(length, 83.3333f);
            count++;
            yield return null;
        }
        _Stretcher.sizeDelta = new Vector2(targetHP*lengthCoefficient, 83.3333f);
    }

    // 데미지 받았을 때 마다 호출
    public void Change(float target)
    {
        if (_coroutineController != null) 
        { StopCoroutine(_coroutineController); }
        _coroutineController = StartCoroutine(AdjustHPBar(_Player.CurHealth, target));
    }
}