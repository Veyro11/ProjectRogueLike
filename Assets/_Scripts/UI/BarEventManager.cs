using UnityEngine;

class BarEventManager : SingletonMono<BarEventManager>
{
    [SerializeField] BossBar BossBar;
    [SerializeField] HPBar HPBar;
    [SerializeField] SPBar SPBar;
    [SerializeField] PlayerStatus _player;



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
            SPBar.Change(originvalue, targetValue);
        }
        else
        {
            Debug.Log("BossBar 오브젝트 미할당");
        }
    }

    private void Update()
    {
        
    }
}