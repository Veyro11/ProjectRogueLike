using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HPBar : MonoBehaviour
{
    public int _maxLength = 870;
    [SerializeField] RectTransform _Stretcher;
    Coroutine _coroutineController;
    float lengthCoefficient;

    private void Start()
    {
        lengthCoefficient = _maxLength / Player.Instance.playerstat.MaxHealth;
    }
    IEnumerator AdjustHPBar(float currentHP, float targetHP)
    {
        lengthCoefficient = _maxLength / Player.Instance.playerstat.MaxHealth;
        Debug.Log($"HP: {Player.Instance.playerstat.MaxHealth}");
        int count = 1;
        while (count <= 2.5f/Time.deltaTime)
        {
            float length = targetHP*lengthCoefficient + 10f / (0.5f*count+10f) *(currentHP-targetHP)*lengthCoefficient;
            _Stretcher.sizeDelta = new Vector2(length, 60f);
            count++;
            yield return null;
        }
        _Stretcher.sizeDelta = new Vector2(targetHP*lengthCoefficient, 60f);
    }

    // 데미지 받았을 때 마다 호출
    public void Change(float origin, float target)
    {
        if (_coroutineController != null) 
        { StopCoroutine(_coroutineController); }
        _coroutineController = StartCoroutine(AdjustHPBar(origin, target));
    }

    public void RefreshCoefficient()
    {
        lengthCoefficient = _maxLength / Player.Instance.playerstat.MaxHealth;
    }
}