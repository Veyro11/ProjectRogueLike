using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.Timeline;

public class SPBar : MonoBehaviour
{
    int _maxLength = 620;
    [SerializeField] RectTransform _Stretcher;
    [SerializeField] PlayerStatus _Player;
    Coroutine _coroutineController;
    IEnumerator AdjustHPBar(float currentSP, float targetSP)
    {
        int count = 1;
        Debug.Log(_Player.MaxSP);
        float lengthCoefficient = _maxLength / _Player.MaxSP;
        while (count <= 2.5f / Time.deltaTime)
        {
            float length = targetSP * lengthCoefficient + 10f / (0.5f * count + 10f) * (currentSP - targetSP) * lengthCoefficient;
            Debug.Log(length + "," + _Player.CurSP);
            _Stretcher.sizeDelta = new Vector2(length, 54f);
            count++;
            yield return null;
        }
        _Stretcher.sizeDelta = new Vector2(targetSP * lengthCoefficient, 54f);
    }

    // SP 회복시/사용시마다 호출
    public void Change(float target)
    {
        if (_coroutineController != null)
        { StopCoroutine(_coroutineController); }
        _coroutineController = StartCoroutine(AdjustHPBar(_Player.CurSP, target));
    }
}