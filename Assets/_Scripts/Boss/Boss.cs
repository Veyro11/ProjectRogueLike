using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using Color = UnityEngine.Color;

public class Boss : MonoBehaviour
{
    public BossData EnemyData { get; set; }

    public Transform target;
    public SpriteRenderer enemyRenderer;
    private MaterialPropertyBlock block;

    [field: Header("Animations")]
    [field: SerializeField] public BossAnimaitionData AnimationData { get; private set; }
    public Animator Animator { get; private set; }

    public BossStateMachine stateMachine;

    public SpriteRenderer attackRenderer;
    public Collider2D attackCollider2D;

    [SerializeField] public GameObject rangedAttackEffect;
    private GameObject currentEffect;
    private RangedAttack ranged;

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
        BarEventManager.Instance.BossBarCall(stateMachine.Enemy.EnemyData.HP + Damage, stateMachine.Enemy.EnemyData.HP);
    }

    public IEnumerator ChangeColor()
    {
        enemyRenderer.GetPropertyBlock(block);
        block.SetColor("_Color", new Color(2, 2, 2, 1));
        enemyRenderer.SetPropertyBlock(block);

        yield return new WaitForSeconds(0.3f);

        // 원래 색으로 복구
        enemyRenderer.GetPropertyBlock(block);
        block.SetColor("_Color", Color.white);
        enemyRenderer.SetPropertyBlock(block);
    }

    public void RangedAttack()
    {
        StartCoroutine(ShowEffect());
    }

    public void RangedAttackReady()
    {
        StartCoroutine(ReadyToAttack());
    }

    public IEnumerator ShowEffect()
    {
        yield return new WaitForSeconds(0.5f);
        currentEffect = ObjectPoolManager.Instance.GetFromPool
            (
            rangedAttackEffect, transform.position +
            new Vector3(transform.localScale.x * 2, transform.position.y - 1, transform.position.z),
            Quaternion.identity
            );

        ranged = currentEffect.GetComponent<RangedAttack>();
        if (ranged != null) ranged.InitBoss(this);

        yield return new WaitForSeconds(0.5f);
        currentEffect = ObjectPoolManager.Instance.GetFromPool
             (
             rangedAttackEffect, transform.position +
             new Vector3(transform.localScale.x * 4, transform.position.y -1, transform.position.z),
             Quaternion.identity
             );

        ranged = currentEffect.GetComponent<RangedAttack>();
        if (ranged != null) ranged.InitBoss(this);

        yield return new WaitForSeconds(0.5f);

        currentEffect = ObjectPoolManager.Instance.GetFromPool
            (
            rangedAttackEffect, transform.position +
            new Vector3(transform.localScale.x * 6, transform.position.y -1, transform.position.z),
            Quaternion.identity
            );

        ranged = currentEffect.GetComponent<RangedAttack>();
        if (ranged != null) ranged.InitBoss(this);
    }

    public IEnumerator ReadyToAttack()
    {
        stateMachine.attackRenderer.color = new Color(1f, 0f, 0f, 0.3f);
        stateMachine.attackRenderer.transform.localScale = new Vector3(2f, 1f, 1f);

        // 1번째 위치
        stateMachine.attackRenderer.transform.position =
            stateMachine.Enemy.transform.position +
            new Vector3(transform.localScale.x * 2, -1f, 0f);
        yield return new WaitForSeconds(0.2f);

        // 2번째 위치
        stateMachine.attackRenderer.transform.position =
            stateMachine.Enemy.transform.position +
            new Vector3(transform.localScale.x * 4, -1f, 0f);
        yield return new WaitForSeconds(0.2f);

        // 3번째 위치
        stateMachine.attackRenderer.transform.position =
            stateMachine.Enemy.transform.position +
            new Vector3(transform.localScale.x * 6, -1f, 0f);
        
        yield return new WaitForSeconds(0.3f);
        stateMachine.attackRenderer.color = new Color(1f, 0f, 0f, 0f);
    }
}
