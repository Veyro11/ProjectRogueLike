using UnityEngine;

class BarEventManager : SingletonMono<BarEventManager>
{
    [SerializeField] BossBar BossBar;
    [SerializeField] HPBar HPBar;
    [SerializeField] SPBar SPBar;

    public void BossBarCall(float targetValue)
    {
        if (BossBar != null)
        {
            BossBar.Change(targetValue);
        }
        else
        {
            Debug.Log("BossBar 오브젝트 미할당");
        }
    }

    public void HPBarCall(float targetValue)
    {
        if (HPBar != null)
        {
            HPBar.Change(targetValue);
        }
        else
        {
            Debug.Log("BossBar 오브젝트 미할당");
        }
    }
    public void SPBarCall(float targetValue)
    {
        if (SPBar != null)
        {
            SPBar.Change(targetValue);
        }
        else
        {
            Debug.Log("BossBar 오브젝트 미할당");
        }
    }
}