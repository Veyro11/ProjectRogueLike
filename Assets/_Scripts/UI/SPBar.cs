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
    float lengthCoefficient;

    private void Start()
    {
        _Stretcher.sizeDelta = new Vector2(0, 54f);
        lengthCoefficient = _maxLength / _Player.MaxSP;
    }
    IEnumerator AdjustSPBar(float currentSP, float targetSP)
    {
        int count = 1;
        Debug.Log(currentSP +","+targetSP+" ==??");
        while (count <= 2.5f / Time.deltaTime)
        {
            float length = (targetSP * lengthCoefficient) + 10f / (0.5f * count + 10f) * (currentSP - targetSP) * lengthCoefficient;
            Debug.Log(length + "length + " + _Player.CurSP);
            _Stretcher.sizeDelta = new Vector2(length, 54f);
            count++;
            yield return null;
        }
        _Stretcher.sizeDelta = new Vector2(targetSP * lengthCoefficient, 54f);
    }

    // SP 회복시/사용시마다 호출
    public void Change(float origin, float target)
    {
        if (_coroutineController != null)
        { StopCoroutine(_coroutineController); }
        _coroutineController = StartCoroutine(AdjustSPBar(origin, target));
    }

    public void Init()
    {
        _Stretcher.sizeDelta = new Vector2(Player.Instance.playerstat.CurSP*lengthCoefficient, 60f);
    }
}