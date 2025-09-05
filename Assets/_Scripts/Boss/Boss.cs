using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class Boss : MonoBehaviour
{
    public BossData EnemyData {  get;  set; }

    public Transform target;
    public SpriteRenderer enemyRenderer;
    private MaterialPropertyBlock block;

    [field: Header("Animations")]
    [field: SerializeField] public BossAnimaitionData AnimationData { get; private set; }
    public Animator Animator { get; private set; }

    public BossStateMachine stateMachine;

    public SpriteRenderer attackRenderer;
    public Collider2D attackCollider2D;

    private void Awake()
    {
        target = Player.Instance.transform;

        AnimationData.Initialize();

        EnemyData = new BossData();
        stateMachine = new BossStateMachine(this);
      
        Animator = GetComponentInChildren<Animator>();

        block = new MaterialPropertyBlock();

        
    }

    private void Start()
    {
        attackCollider2D.enabled = false;
        stateMachine.ChangeState(stateMachine.IdleState);

    }

    private void Update()
    {
        stateMachine.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        stateMachine.OnTriggerEnter2D(collision);
    }

    public void TakeDamage(float Damage)
    {
        StartCoroutine(ChangeColor());
        AudioManager.Instance.PlaySFX("Boss_Damage");
        stateMachine.Enemy.EnemyData.HP -= Damage;
        BarEventManager.Instance.BossBarCall(stateMachine.Enemy.EnemyData.HP+Damage, stateMachine.Enemy.EnemyData.HP);
    }

    public IEnumerator ChangeColor()
    {
        enemyRenderer.GetPropertyBlock(block);
        block.SetColor("_Color", new Color(2,2,2,1));
        enemyRenderer.SetPropertyBlock(block);

        yield return new WaitForSeconds(0.3f);

        // 원래 색으로 복구
        enemyRenderer.GetPropertyBlock(block);
        block.SetColor("_Color", Color.white);
        enemyRenderer.SetPropertyBlock(block);
    }
}
