using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.Timeline;

public class HPBar : MonoBehaviour
{
    int _maxLength = 840;
    [SerializeField] RectTransform _Stretcher;
    [SerializeField] PlayerStatus _Player;
    [SerializeField] int Test;
    Coroutine _coroutineController;
    IEnumerator AdjustHPBar(int currentHP, int targetHP)
    {
        int count = 1;
        Debug.Log(_Player.maxHP);
        float lengthCoefficient = 840f / _Player.maxHP;
        while (count <= 2.5f/Time.deltaTime)
        {
            float length = targetHP*lengthCoefficient + 10f / (0.5f*count+10f) * (currentHP-targetHP)*lengthCoefficient;
            Debug.Log(length + "," + _Player.curHP);
            _Stretcher.sizeDelta = new Vector2(length, 83.3333f);
            count++;
            yield return null;
        }
        _Stretcher.sizeDelta = new Vector2(targetHP*lengthCoefficient, 83.3333f);
    }

    public void Change()
    {
        if (_coroutineController != null) 
        { StopCoroutine(_coroutineController); }
        _coroutineController = StartCoroutine(AdjustHPBar(_Player.curHP, Test));
    }
}