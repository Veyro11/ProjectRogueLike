using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData EnemyData {  get;  set; }

    public Transform target;
    public Animator Animator { get; private set; }

    private EnemyStateMachine stateMachine;

    public SpriteRenderer attackRenderer;

    private void Awake()
    {
        EnemyData = new EnemyData();
        stateMachine = new EnemyStateMachine(this);

        Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    private void Update()
    {
        stateMachine.Update();
    }

}
