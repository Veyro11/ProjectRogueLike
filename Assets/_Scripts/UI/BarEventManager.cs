using UnityEngine;

class BarEventManager : SingletonMono<BarEventManager>
{
    [SerializeField] BossBar BossBar;
    [SerializeField] HPBar HPBar;
    [SerializeField] SPBar SPBar;
    [SerializeField] PlayerStatus _player;
    bool init = false;

    private void Start()
    {
        SetBossBar(false);
    }

    public void BossBarInitCaller(Boss boss = null)
    {
        BossBar.BossSet(boss);
    }

    public void RefreshBossBar()
    {
        BossBar.Reset();
    }

    public void BossBarCall(float originvalue, float targetValue)
    {
        if (BossBar != null)
        {
            BossBar.Change(originvalue, targetValue);
        }
        else
        {
            Debug.Log("BossBar 오브젝트 미할당");
        }
    }

    public void HPBarCall(float originvalue, float targetValue)
    {
        if (HPBar != null)
        {
            HPBar.Change(originvalue, targetValue);
        }
        else
        {
            Debug.Log("BossBar 오브젝트 미할당");
        }
    }
    public void SPBarCall(float originvalue, float targetValue)
    {
        if (SPBar != null)
        {
            Debug.Log("SP: 에니메이션 시작");
            SPBar.Change(originvalue, targetValue);
        }
        else
        {
            Debug.Log("SPBar 오브젝트 미할당");
        }
    }

    public void SetBossBar(bool State)
    {
        BossBar.gameObject.SetActive(State);
    }
}