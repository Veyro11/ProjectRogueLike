using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class Monster : MonoBehaviour
{
    public MonsterData MonsterData { get; set; }

    public Transform target;
    public SpriteRenderer monsterRenderer;
    private MaterialPropertyBlock block;

    [field: Header("Animations")]
    [field: SerializeField] public MonsterAnimationData AnimationData { get; private set; }
    public Animator Animator { get; private set; }

    public MonsterStateMachine stateMachine;

    public SpriteRenderer attackRenderer;
    public Collider2D attackCollider2D;

    private void Awake()
    {
        target = Player.Instance.transform;

        AnimationData.Initialize();

        MonsterData = new MonsterData();
        stateMachine = new MonsterStateMachine(this);

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

    public void TakeDamage(int Damage)
    {
        StartCoroutine(ChangeColor());
        stateMachine.Monster.MonsterData.HP -= Damage;
        Player.Instance.playerstat.AddSP(1);
        BarEventManager.Instance.SPBarCall(Player.Instance.Data.curSP-1, Player.Instance.Data.curSP);
    }

    public IEnumerator ChangeColor()
    {
        monsterRenderer.GetPropertyBlock(block);
        block.SetColor("_Color", new Color(2, 2, 2, 1));
        monsterRenderer.SetPropertyBlock(block);

        yield return new WaitForSeconds(0.3f);

        // 원래 색으로 복구
        monsterRenderer.GetPropertyBlock(block);
        block.SetColor("_Color", Color.white);
        monsterRenderer.SetPropertyBlock(block);
    }
}
