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
        int count = 0;
        while (count < 2/Time.deltaTime)
        {
            float length = LogicticFunction.Logistic(count, currentHP, targetHP) * 8.4f;
            Debug.Log(length + "," + _Player.curHP);
            _Stretcher.sizeDelta = new Vector2(length, 83.3333f);
            count++;
            yield return null;
        }
    }

    public void Change()
    {
        if (_coroutineController != null) { StopCoroutine(_coroutineController); }
        _coroutineController = StartCoroutine(AdjustHPBar(_Player.curHP, Test));
    }
}