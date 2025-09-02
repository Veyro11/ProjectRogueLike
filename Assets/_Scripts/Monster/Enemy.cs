using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData EnemyData {  get;  set; }

    public Transform target;

    [field: Header("Animations")]
    [field: SerializeField] public EnemyAnimaitionData AnimationData { get; private set; }
    public Animator Animator { get; private set; }

    private EnemyStateMachine stateMachine;

    public SpriteRenderer attackRenderer;
    public Collider2D attackCollider2D;

    private void Awake()
    {
        target = Player.Instance.transform;

        AnimationData.Initialize();

        EnemyData = new EnemyData();
        stateMachine = new EnemyStateMachine(this);

        Animator = GetComponentInChildren<Animator>();
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
}
