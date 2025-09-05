using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossChaseState : BossBaseState
{
    private bool isReady = false;
    private bool isPlaying = false;
    public bool is_2M_Attack = false;
    public bool is_4M_Attack = false;
    public bool is_Ranged_Attack = false;
    public bool is_2M_AttackReady = false;
    public bool is_4M_AttackReady = false;
    public bool is_is_Ranged_AttackReady = false;
    public Vector3 currentPosition;
    public Vector3 Attack2m;
    public Vector3 Attack4m;

    private int random;

    public BossChaseState(BossStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        random = Random.Range(1, 101);



        timer = 0.7f;
        isPlaying = false;
        is_2M_Attack = false;
        is_4M_Attack = false;
        is_Ranged_Attack = false;
        is_2M_AttackReady = false;
        is_4M_AttackReady = false;
        is_is_Ranged_AttackReady = false;

        StartAnimation(stateMachine.Enemy.AnimationData.WalkParameterHash);
        currentPosition = stateMachine.attackRenderer.transform.localPosition;

        Attack2m = new Vector3(stateMachine.attackRenderer.transform.localPosition.x + 0.5f,
                               stateMachine.attackRenderer.transform.localPosition.y,
                               stateMachine.attackRenderer.transform.localPosition.z);

        Attack4m = new Vector3(stateMachine.attackRenderer.transform.localPosition.x + 1f,
                               stateMachine.attackRenderer.transform.localPosition.y,
                               stateMachine.attackRenderer.transform.localPosition.z);
    }

    public override void Exit()
    {
        isReady = false;
        StopAnimation(stateMachine.Enemy.AnimationData.AttackReadyParameterHash);
    }

    public override void HandleInput()
    {

    }

    public override void PhysicsUpdate()
    {

    }

    public override void Update()
    {
        base.Update();

        StartChasing();
        RandomReadyToAttack();

        if (!isReady) return;
        timer -= Time.deltaTime;
    }

    public void StartChasing()
    {
        BarEventManager.Instance.RefreshBossBar();
        //기본적인 추적 로직, 공격 준비 중일 땐 추적을 멈추기 위해 bool값을 사용했습니다.
        if (!isReady && stateMachine.enemyGroundChecker.IsGroundChecker())
        {
            BarEventManager.Instance.SetBossBar(true);
            Vector3 dir = (stateMachine.targetTransform.position - stateMachine.ownerTransform.position).normalized;

            stateMachine.ownerTransform.position += dir * stateMachine.Enemy.EnemyData.MoveSpeed * Time.deltaTime;
        }

        //공격 거리 탐색 후 2M터 일 경우 bool값으로 트리거 해줍니다.
        if (Vector3.Distance(stateMachine.targetTransform.position, stateMachine.ownerTransform.position) < attackDIstance_2m && !isReady)
        {
            Debug.Log("공격준비");

            isReady = true;
            is_2M_Attack = true;

            StopAnimation(stateMachine.Enemy.AnimationData.WalkParameterHash);
            StartAnimation(stateMachine.Enemy.AnimationData.AttackReadyParameterHash);
        }
        //공격 거리 탐색 후 4M터 일 경우 bool값으로 트리거 해줍니다.
        else if (Vector3.Distance(stateMachine.targetTransform.position, stateMachine.ownerTransform.position) < attackDIstance_4m && !isReady)
        {
            Debug.Log("공격준비");

            isReady = true;
            is_4M_Attack = true;

            StopAnimation(stateMachine.Enemy.AnimationData.WalkParameterHash);
            StartAnimation(stateMachine.Enemy.AnimationData.AttackReadyParameterHash);
        }

        //8M이상 멀어지면 Return상태(제자리로 돌아가기)로 변환 시켜줍니다.
        if (Vector2.Distance(stateMachine.targetTransform.position, stateMachine.ownerTransform.position) < missingDistance) return;

        stateMachine.ChangeState(stateMachine.ReturnState);
    }

    // 공격전 모션 실행하는 메서드 입니다.
    public void ReadyToAttack_2M()
    {
        if (!isReady) return;

        stateMachine.attackRenderer.color = new Color(1f, 0f, 0f, 0.3f);    //히트박스표시 사이즈 조절 통해서 범위 추가 가능

        stateMachine.attackRenderer.transform.localScale = new Vector3(2f, 1f, 1f);
        stateMachine.attackRenderer.transform.localPosition = Attack2m;

        if (timer > 0) return;

        stateMachine.attackRenderer.color = new Color(1f, 0f, 0f, 0f);
        is_2M_AttackReady = true;
        stateMachine.ChangeState(stateMachine.AttackState);
    }

    public void ReadyToAttack_4M()
    {
        if (!isReady) return;

        if (!isPlaying)
        {
            stateMachine.Enemy.RangedAttackReady();
            Debug.Log("@!@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            isPlaying = true;
        }

        //stateMachine.attackRenderer.color = new Color(1f, 0f, 0f, 0.3f);    //히트박스표시 사이즈 조절 통해서 범위 추가 가능

        //stateMachine.attackRenderer.transform.localScale = new Vector3(3f, 1f, 1f);
        //stateMachine.attackRenderer.transform.localPosition = Attack4m;

        if (timer > 0) return;

        stateMachine.attackRenderer.color = new Color(1f, 0f, 0f, 0f);
        is_4M_AttackReady = true;
        stateMachine.ChangeState(stateMachine.AttackState);
    }

    public void ReadyToAttack_Ranged()
    {
        if (timer > 0) return;

        is_is_Ranged_AttackReady = true;
        stateMachine.ChangeState(stateMachine.AttackState);
    }

    //미리 2인지 인지 골라서 보내기? 여기서 랜덤으로 뽑고
    //2m일경우 is_2M_Attack만 트루
    //4m일 경우 랜덤

    private void RandomReadyToAttack()
    {
        if (is_2M_Attack)
        {
            if (random <= 20)
            {
                ReadyToAttack_Ranged();
                return;
            }
            else if (random <= 60)
            {
                ReadyToAttack_2M();
                return;
            }
            else if (random <= 100)
            {
                ReadyToAttack_4M();
            }
        }
        else if (is_4M_Attack)
        {

            ReadyToAttack_4M();
        }
    }

}
