using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.Timeline;

public class BossBar : MonoBehaviour
{
    int _maxLength = 1590;
    int _bossMaxHealth;
    [SerializeField] RectTransform _Stretcher;
    [SerializeField] Boss _boss;

    private void Start()
    {
        _bossMaxHealth = _boss.EnemyData.HP;
    }

    Coroutine _coroutineController;
    IEnumerator AdjustUIBar(float currentHP, float targetHP)
    {
        int count = 1;
        float lengthCoefficient = _maxLength / (float)_bossMaxHealth;
        while (count <= 2.5f / Time.deltaTime)
        {
            float length = targetHP * lengthCoefficient + 10f / (0.5f * count + 10f) * (currentHP - targetHP) * lengthCoefficient;
            _Stretcher.sizeDelta = new Vector2(length, 83.3333f);
            count++;
            yield return null;
        }
        _Stretcher.sizeDelta = new Vector2(targetHP * lengthCoefficient, 83.3333f);
    }

    // 데미지 받았을 때 마다 호출
    public void Change(float origin, float target)
    {
        if (_coroutineController != null)
        { StopCoroutine(_coroutineController); }
        _coroutineController = StartCoroutine(AdjustUIBar(origin, target));
    }
}